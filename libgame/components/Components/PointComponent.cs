using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Libgame.Characters;
using UnityEngine;

namespace Libgame.Components
{
    public class PointComponent: CharacterComponent
    {
        void OnEnable()
        {
            StartCoroutine(RecoverPointPerSecond());
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }

        private float _point;
        /// <summary>
        /// 当前Point
        /// </summary>
        public float point
        {
            get
            {
                return _point;
            }
            protected set
            {
                if(_point!= value)
                {
                    float _value = _point;
                    _point = value;
                    if (onPointChange != null)
                    {
                        onPointChange(_value, value);
                    }
                }
            }
        }

        #region 变化回调
        Action<float, float> onPointChange;

        public void AttachPointChangeCallBack(Action<float, float> onPointChange)
        {
            this.onPointChange += onPointChange;
        }

        public void DetachPointChangeCallBack(Action<float, float> onPointChange)
        {
            this.onPointChange -= onPointChange;
        }

        public void ClearPointChangeCallBack()
        {
            this.onPointChange = null;
        }
        #endregion

        #region 0值回调
        Action<Character, float, float> onPointLE0;

        public void AttachPointLE0CallBack(Action<Character, float, float> onPointLE0)
        {
            this.onPointLE0 += onPointLE0;
        }

        public void DetachPointLE0CallBack(Action<Character, float, float> onPointLE0)
        {
            this.onPointLE0 -= onPointLE0;
        }

        public void ClearPointLE0CallBack()
        {
            this.onPointLE0 = null;
        }
        #endregion
        /// <summary>
        /// 无条件扣除相应Point
        /// </summary>
        /// <param name="p_pointLost">Point扣除量</param>
        public virtual void LosePoint(Character source, float p_pointLost)
        {
            if (p_pointLost < 0)
            {
                p_pointLost = 0;
            }
            point -= p_pointLost;
            if (point <= 0)
            {
                if (onPointLE0 != null)
                {
                    onPointLE0(source, p_pointLost, point);
                }
            }
        }

        /// <summary>
        /// 试着扣除相应Point
        /// </summary>
        /// <param name="p_pointLost">Point扣除量</param>
        /// <returns>扣除成功，返回真，否则返回假</returns>
        public virtual bool TryToLosePoint(Character source, float p_pointLost)
        {
            if (p_pointLost < 0)
            {
                p_pointLost = 0;
            }
            if (point > p_pointLost)
            {
                LosePoint(source, p_pointLost);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获得Point
        /// </summary>
        /// <param name="p_pointObtained">Point获得量</param>
        /// <returns>实际增加的Point</returns>
        public virtual float AddPoint(Character source, float p_pointObtained)
        {
            if (p_pointObtained < 0)
            {
                p_pointObtained = 0;
            }
            float pointTemp = point;
            point += p_pointObtained;
            if (point > maxPoint)
            {
                point = maxPoint;
            }
            return point - pointTemp;
        }

        /// <summary>
        /// Point清0
        /// </summary>
        public void ClearPoint()
        {
            this.point = 0;
        }

        /// <summary>
        /// 设置Point
        /// </summary>
        public void SetPoint(float point)
        {
            this.point = point;
        }

        #region MAX_POINT
        /// <summary>
        /// 基础最大Point
        /// </summary>
        protected float baseMaxPoint = 100;


        /// <summary>
        /// 基础最大Point增加值，私有
        /// </summary>
        [SerializeField]
        private float _maxPointAddedValue = 0;

        /// <summary>
        /// 基础最大Point增加值
        /// </summary>
        protected float maxPointAddedValue
        {
            get { return _maxPointAddedValue; }
            set { _maxPointAddedValue = value; }
        }

        /// <summary>
        /// 最大Point百分比，私有
        /// </summary>
        [SerializeField]
        private float _maxPointRate = 1;

        /// <summary>
        /// 最大Point百分比
        /// </summary>
        protected float maxPointRate
        {
            get { return _maxPointRate; }
            set { _maxPointRate = value; }
        }

        /// <summary>
        /// 最大Point,私有
        /// </summary>
        private float _maxPoint
        {
            get
            {
                return (baseMaxPoint + maxPointAddedValue) * maxPointRate;
            }
        }
        /// <summary>
        /// 最大Point
        /// </summary>
        protected float maxPoint
        {
            get
            {
                return UnityEngine.Mathf.Min(maxMaxPoint, UnityEngine.Mathf.Max(minMaxPoint, _maxPoint));
            }
        }

        /// <summary>
        /// 最小最大Point
        /// </summary>
        protected float minMaxPoint = 1;

        /// <summary>
        /// 最大最大Point
        /// </summary>
        protected float maxMaxPoint = 10000;

        /// <summary>
        /// 重设Point为最大Point
        /// </summary>
        public void ResetPoint()
        {
            point = maxPoint;
        }

        /// <summary>
        /// 增加最大Point
        /// </summary>
        /// <param name="p_maxPointAddedValue">增加的基础最大Point增加值</param>
        /// <param name="p_maxPointRate">增加的最大Point百分比</param>
        /// <returns></returns>
        public bool AddMaxPoint(float p_maxPointAddedValue, float p_maxPointRate)
        {
            return ChangeMaxPoint(this.maxPointAddedValue + p_maxPointAddedValue, this.maxPointRate + p_maxPointRate);
        }

        /// <summary>
        /// 改变最大Point
        /// </summary>
        /// <param name="p_maxPointAddedValue">新的基础最大Point增加值</param>
        /// <param name="p_maxPointRate">新的最大Point百分比</param>
        /// <returns>改变是否成功</returns>
        public bool ChangeMaxPoint(float p_maxPointAddedValue, float p_maxPointRate)
        {
            this.maxPointAddedValue = p_maxPointAddedValue;
            this.maxPointRate = p_maxPointRate;
            return true;
        }

        /// <summary>
        /// 设置最大Point
        /// </summary>
        /// <param name="p_baseMaxPoint">新的基础最大Point</param>
        /// <param name="p_maxPointAddedValue">新的基础最大Point增加值</param>
        /// <param name="p_maxPointRate">新的最大Point百分比</param>
        /// <returns>改变是否成功</returns>
        public  bool SetMaxPoint(float p_baseMaxPoint, float p_maxPointAddedValue, float p_maxPointRate)
        {
            this.baseMaxPoint = p_baseMaxPoint;
            this.maxPointAddedValue = p_maxPointAddedValue;
            this.maxPointRate = p_maxPointRate;
            return true;
        }
        #endregion

        #region POINT_RECOVER
        /// <summary>
        /// 基础每秒Point恢复值
        /// </summary>
        protected float basePointRecover = 0;

        /// <summary>
        /// 基础每秒Point恢复值增加量，私有
        /// </summary>
        [SerializeField]
        private float _pointRecoverAddedValue = 0;

        /// <summary>
        /// 基础每秒Point恢复值增加量
        /// </summary>
        protected float pointRecoverAddedValue
        {
            get { return _pointRecoverAddedValue; }
            set { _pointRecoverAddedValue = value; }
        }

        /// <summary>
        /// 每秒Point恢复增加百分比，私有
        /// </summary>
        [SerializeField]
        private float _pointRecoverRate = 1;

        /// <summary>
        /// 每秒Point恢复增加百分比
        /// </summary>
        protected float pointRecoverRate
        {
            get { return _pointRecoverRate; }
            set { _pointRecoverRate = value; }
        }

        /// <summary>
        /// 当前每秒Point恢复值,私有
        /// </summary>
        protected float _pointRecover
        {
            get
            {
                return (basePointRecover + pointRecoverAddedValue) * pointRecoverRate;
            }
        }

        /// <summary>
        /// 当前每秒Point恢复值
        /// </summary>
        protected float pointRecover
        {
            get
            {
                return _pointRecover;
            }
        }

        /// <summary>
        /// 增加每秒Point恢复值
        /// </summary>
        /// <param name="p_pointRecoverAddedValue">增加的每秒Point恢复增加值</param>
        /// <param name="p_pointRecoverRate">增加的每秒Point恢复百分比</param>
        /// <returns>是否增加成功</returns>
        public bool AddPointRecover(float p_pointRecoverAddedValue, float p_pointRecoverRate)
        {
            return ChangePointRecover(this.pointRecoverAddedValue + p_pointRecoverAddedValue, this.pointRecoverRate + p_pointRecoverRate);
        }

        /// <summary>
        /// 改变每秒Point恢复值
        /// </summary>
        /// <param name="p_pointRecoverAddedValue">新的基础每秒Point恢复增加值</param>
        /// <param name="p_pointRecoverRate">新的每秒Point恢复增加百分比</param>
        /// <returns>是否改变成功</returns>
        public bool ChangePointRecover(float p_pointRecoverAddedValue, float p_pointRecoverRate)
        {
            this.pointRecoverAddedValue = p_pointRecoverAddedValue;
            this.pointRecoverRate = p_pointRecoverRate;
            return true;
        }

        /// <summary>
        /// 设置每秒Point恢复
        /// </summary>
        /// <param name="p_basePointRecover">新的基础每秒Point恢复</param>
        /// <param name="p_pointRecoverAddedValue">新的基础每秒Point恢复增加值</param>
        /// <param name="p_pointRecoverRate">新的每秒Point恢复增加百分比</param>
        /// <returns></returns>
        public bool SetPointRecover(float p_basePointRecover, float p_pointRecoverAddedValue, float p_pointRecoverRate)
        {
            this.basePointRecover = p_basePointRecover;
            this.pointRecoverAddedValue = p_pointRecoverAddedValue;
            this.pointRecoverRate = p_pointRecoverRate;
            return true;
        }

        public virtual bool CanRecover()
        {
            return true;
        }

        /// <summary>
        /// 协程，Point恢复
        /// </summary>
        /// <returns></returns>
        IEnumerator RecoverPointPerSecond()
        {
            while (true)
            {
                if (CanRecover())
                {
                    if (pointRecover > 0)
                    {
                        AddPoint(null, pointRecover * Time.deltaTime);
                    }
                    else if (pointRecover < 0)
                    {
                        //如果修改Point回复为负数，因为Point恢复而死亡，算自杀
                        LosePoint(null, -pointRecover * Time.deltaTime);
                    }
                }
                yield return 0;
            }
        }

        /// <summary>
        /// 暂停Point恢复协程
        /// </summary>
        public void PausePointRecover()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// 重新开始Point恢复协程
        /// </summary>
        public void ResumePointRecover()
        {
            PausePointRecover();
            StartCoroutine(RecoverPointPerSecond());
        }
        #endregion

        /// <summary>
        /// 显示组件的基本信息，override
        /// </summary>
        public override void ShowDetail()
        {
            MonoBehaviour.print(
                "[" + this + "] => "
                + "point: " + point
                + ", maxPoint: " + maxPoint + ", baseMaxPoint: " + baseMaxPoint + ", maxPointAddedValue: " + maxPointAddedValue + ", maxPointRate: " + maxPointRate
                + ", minMaxPoint: " + minMaxPoint + ", maxMaxPoint: " + maxMaxPoint + ",\n"
                + "pointRecover: " + pointRecover + ", basePointRecover: " + basePointRecover + ", pointRecoverAddedValue: " + pointRecoverAddedValue + ", pointRecoverRate: " + pointRecoverRate);
        }
    }
}
