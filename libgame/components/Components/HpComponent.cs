﻿using UnityEngine;
using System.Collections;
using System;
using Libgame.Characters;

namespace Libgame.Components
{
    /// <summary>
    /// 生命值组件
    /// </summary>
    public class HpComponent : CharacterComponent
    {
        void OnEnable()
        {
            StartCoroutine(RecoverHpPerSecond());
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// 当前生命值
        /// </summary>
        public float hp;

        Action<Character, float, float> onHpLE0;

        public void AttachHpLE0CallBack(Action<Character, float, float> onHpLE0)
        {
            this.onHpLE0 += onHpLE0;
        }

        public void DetachHpLE0CallBack(Action<Character, float, float> onHpLE0)
        {
            this.onHpLE0 -= onHpLE0;
        }

        public void ClearHpLE0CallBack()
        {
            this.onHpLE0 = null;
        }
        
        /// <summary>
        /// 无条件扣除相应生命值
        /// </summary>
        /// <param name="p_hpLost">生命值扣除量</param>
        public virtual void LoseHp(Character source, float p_hpLost)
        {
            if (p_hpLost < 0)
            {
                p_hpLost = 0;
            }
            hp -= p_hpLost;
            if (hp <= 0)
            {
                if (onHpLE0 != null)
                {
                    onHpLE0(source, p_hpLost, hp);
                }
            }
        }

        /// <summary>
        /// 试着扣除相应生命值
        /// </summary>
        /// <param name="p_hpLost">生命值扣除量</param>
        /// <returns>扣除成功，返回真，否则返回假</returns>
        public virtual bool TryToLoseHp(Character source, float p_hpLost)
        {
            if (p_hpLost < 0)
            {
                p_hpLost = 0;
            }
            if (hp > p_hpLost)
            {
                LoseHp(source, p_hpLost);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得生命值
        /// </summary>
        /// <param name="p_hpObtained">生命值获得量</param>
        /// <returns>实际增加的生命值</returns>
        public virtual float AddHp(Character source, float p_hpObtained)
        {
            if (p_hpObtained < 0)
            {
                p_hpObtained = 0;
            }
            float hpTemp = hp;
            hp += p_hpObtained;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
            return hp - hpTemp;
        }

        /// <summary>
        /// 生命值清0
        /// </summary>
        public void ClearHp()
        {
            this.hp = 0;
        }

#region MAX_HP
        /// <summary>
        /// 基础最大生命值
        /// </summary>
        static public float baseMaxHp = 100;


        /// <summary>
        /// 基础最大生命值增加值，私有
        /// </summary>
        [SerializeField]
        private float _maxHpAddedValue = 0;

        /// <summary>
        /// 基础最大生命值增加值
        /// </summary>
        public float maxHpAddedValue
        {
            get { return _maxHpAddedValue; }
            private set { _maxHpAddedValue = value; }
        }

        /// <summary>
        /// 最大生命值百分比，私有
        /// </summary>
        [SerializeField]
        private float _maxHpRate = 1;

        /// <summary>
        /// 最大生命值百分比
        /// </summary>
        public float maxHpRate
        {
            get { return _maxHpRate; }
            private set { _maxHpRate = value; }
        }

        /// <summary>
        /// 最大生命值,私有
        /// </summary>
        private float _maxHp
        {
            get
            {
                return (baseMaxHp + maxHpAddedValue) * maxHpRate;
            }
        }
        /// <summary>
        /// 最大生命值
        /// </summary>
        public float maxHp
        {
            get
            {
                return UnityEngine.Mathf.Min(maxMaxHp, UnityEngine.Mathf.Max(minMaxHp, _maxHp));
            }
        }

        /// <summary>
        /// 最小最大生命值
        /// </summary>
        static public float minMaxHp = 1;

        /// <summary>
        /// 最大最大生命值
        /// </summary>
        static public float maxMaxHp = 10000;

        /// <summary>
        /// 重设生命值为最大生命值
        /// </summary>
        public void ResetHp()
        {
            hp = maxHp;
        }

        /// <summary>
        /// 增加最大生命值
        /// </summary>
        /// <param name="p_maxHpAddedValue">增加的基础最大生命值增加值</param>
        /// <param name="p_maxHpRate">增加的最大生命值百分比</param>
        /// <returns></returns>
        public bool AddMaxHp(float p_maxHpAddedValue, float p_maxHpRate)
        {
            return ChangeMaxHp(this.maxHpAddedValue + p_maxHpAddedValue, this.maxHpRate + p_maxHpRate);
        }

        /// <summary>
        /// 改变最大生命值
        /// </summary>
        /// <param name="p_maxHpAddedValue">新的基础最大生命值增加值</param>
        /// <param name="p_maxHpRate">新的最大生命值百分比</param>
        /// <returns>改变是否成功</returns>
        public bool ChangeMaxHp(float p_maxHpAddedValue, float p_maxHpRate)
        {
            this.maxHpAddedValue = p_maxHpAddedValue;
            this.maxHpRate = p_maxHpRate;
            return true;
        }
#endregion
#region HP_RECOVER

        /// <summary>
        /// 基础每秒生命恢复值
        /// </summary>
        static public float baseHpRecover = 0;

        /// <summary>
        /// 基础每秒生命恢复值增加量，私有
        /// </summary>
        [SerializeField]
        private float _hpRecoverAddedValue = 0;

        /// <summary>
        /// 基础每秒生命恢复值增加量
        /// </summary>
        public float hpRecoverAddedValue
        {
            get { return _hpRecoverAddedValue; }
            private set { _hpRecoverAddedValue = value; }
        }

        /// <summary>
        /// 每秒生命恢复增加百分比，私有
        /// </summary>
        [SerializeField]
        private float _hpRecoverRate = 1;

        /// <summary>
        /// 每秒生命恢复增加百分比
        /// </summary>
        public float hpRecoverRate
        {
            get { return _hpRecoverRate; }
            private set { _hpRecoverRate = value; }
        }

        /// <summary>
        /// 当前每秒生命恢复值,私有
        /// </summary>
        public float _hpRecover
        {
            get
            {
                return (baseHpRecover + hpRecoverAddedValue) * hpRecoverRate;
            }
        }

        /// <summary>
        /// 当前每秒生命恢复值
        /// </summary>
        public float hpRecover
        {
            get
            {
                return _hpRecover;
            }
        }

        /// <summary>
        /// 增加每秒生命恢复值
        /// </summary>
        /// <param name="p_hpRecoverAddedValue">增加的每秒生命恢复增加值</param>
        /// <param name="p_hpRecoverRate">增加的每秒生命恢复百分比</param>
        /// <returns>是否增加成功</returns>
        public bool AddHpRecover(float p_hpRecoverAddedValue, float p_hpRecoverRate)
        {
            return ChangeHpRecover(this.hpRecoverAddedValue + p_hpRecoverAddedValue, this.hpRecoverRate + p_hpRecoverRate);
        }

        /// <summary>
        /// 改变每秒生命恢复值
        /// </summary>
        /// <param name="p_hpRecoverAddedValue">新的基础每秒生命恢复增加值</param>
        /// <param name="p_hpRecoverRate">新的每秒生命恢复增加百分比</param>
        /// <returns>是否改变成功</returns>
        public bool ChangeHpRecover(float p_hpRecoverAddedValue, float p_hpRecoverRate)
        {
            this.hpRecoverAddedValue = p_hpRecoverAddedValue;
            this.hpRecoverRate = p_hpRecoverRate;
            return true;
        }

        /// <summary>
        /// 协程，生命恢复
        /// </summary>
        /// <returns></returns>
        IEnumerator RecoverHpPerSecond()
        {
            while (true)
            {
                if (hpRecover > 0)
                {
                    AddHp(null, hpRecover * Time.deltaTime);
                }
                else if (hpRecover < 0)
                {
                    //TODO
                    //涉及到BUFF的话，如果2个不同的角色，都在同一个角色身上添加了扣血BUFF
                    //被添加的角色死亡，算谁的
                    //TODO
                    //在修改恢复或者生命值的时候，记录来源。
                    LoseHp(null, -hpRecover * Time.deltaTime);
                }
                yield return 0;
            }
        }

        /// <summary>
        /// 暂停生命恢复协程
        /// </summary>
        public void PauseHpRecover()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// 重新开始生命恢复协程
        /// </summary>
        public void ResumeHpRecover()
        {
            PauseHpRecover();
            StartCoroutine(RecoverHpPerSecond());
        }
        #endregion

        /// <summary>
        /// 显示组件的基本信息，override
        /// </summary>
        [ContextMenu("ShowDetail")]
        public override void ShowDetail()
        {
            MonoBehaviour.print(
                "[" + gameObject + "] => "
                + "hp: " + hp
                + ", maxHp: " + maxHp + ", [static]baseMaxHp: " + baseMaxHp + ", maxHpAddedValue: " + maxHpAddedValue + ", maxHpRate: " + maxHpRate
                + ", [static]minMaxHp: " + minMaxHp + ", [static]maxMaxHp: " + maxMaxHp + ",\n"
                + "hpRecover: " + hpRecover + ", [static]baseHpRecover: " + baseHpRecover + ", hpRecoverAddedValue: " + hpRecoverAddedValue + ", hpRecoverRate: " + hpRecoverRate);
        }
    }
}
