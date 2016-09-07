using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libgame.Damages;
using UnityEngine;
using Libgame.Characters;

namespace Libgame.Components
{
    /// <summary>
    /// 攻击力
    /// </summary>
    [System.Serializable]
    public class Attacks
    {
        /// <summary>
        /// 攻击力数值数组
        /// </summary>
        [SerializeField]
        protected float[] attacks;

        public Attacks() { }

        public Attacks(float[] attacks)
        {
            this.attacks = attacks;
        }

        public float[] GetAttacks()
        {
            return attacks;
        }
    }

    /// <summary>
    /// 攻击速度
    /// </summary>
    [System.Serializable]
    public class AttackSpeed
    {
        public float baseAttackInterval = 1.5F;
        /// <summary>
        /// 时间修正数值
        /// </summary>
        public float timeModifiedValue = 1000F;

        /// <summary>
        /// 攻击间隔
        /// </summary>
        public float attackInterval
        {
            get
            {
                return (baseAttackInterval * timeModifiedValue) / (timeModifiedValue + attackSpeed);
            }
        }
        
        /// <summary>
        /// 攻击速度，private
        /// </summary>
        protected float _attackSpeed = 100F;

        /// <summary>
        /// 攻击速度
        /// </summary>
        public float attackSpeed
        {
            get
            {
                return _attackSpeed;
            }
            set
            {
                _attackSpeed = Mathf.Min(Mathf.Max(value, minAttackSpeed), maxAttackSpeed);
            }
        }

        /// <summary>
        /// 最小攻击速度
        /// </summary>
        public float minAttackSpeed = 0;

        /// <summary>
        /// 最大攻击速度
        /// </summary>
        public float maxAttackSpeed = 5000;

        /// <summary>
        /// 最后一次攻击时间
        /// </summary>
        float lastAttackTime;

        /// <summary>
        /// 是否可以攻击
        /// </summary>
        /// <returns>是否可以攻击</returns>
        public bool CanAttack()
        {
            if (Time.time - lastAttackTime > attackInterval)
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 攻击之后，更新最后一次攻击时间
        /// </summary>
        public void UpdateLastAttackTime()
        {
            lastAttackTime = Time.time;
        }
    }

    /// <summary>
    /// 攻击距离
    /// </summary>
    [System.Serializable]
    public class AttackDistance
    {
        /// <summary>
        /// 攻击最远距离
        /// </summary>
        public float maxDistance = 500;

        /// <summary>
        /// 攻击最近距离
        /// </summary>
        public float minDistance = 0;

        /// <summary>
        /// 检查是否在攻击距离内
        /// </summary>
        /// <param name="self"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool CheckInDistance(Vector3 self, Vector3 target)
        {
            float dis = Vector3.Distance(self, target);
            if(dis>minDistance && dis < maxDistance)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 基础攻击组件
    /// </summary>
    public class BaseAttackComponent : CharacterComponent
    {
        /// <summary>
        /// 攻击力
        /// </summary>
        public Attacks attacks;

        /// <summary>
        /// 攻击速度
        /// </summary>
        public AttackSpeed attackSpeed;

        /// <summary>
        /// 攻击距离
        /// </summary>
        public AttackDistance attackDistance;

        /// <summary>
        /// 获取伤害数据结果
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="target">目标</param>
        /// <returns></returns>
        public Damage GetAttackDamage(Character source, Character target)
        {
            Damage damage = new Damage(attacks.GetAttacks(), null, source, target);
            return damage;
        }

        /// <summary>
        /// 攻击
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="target">目标</param>
        public virtual void Attack(Character source, Character target)
        {
            Damage damage = GetAttackDamage(source, target);
            if(source is FightCharacter)
            {
                ((FightCharacter)source).TakeDamages(damage);
            }
        }
    }
}
