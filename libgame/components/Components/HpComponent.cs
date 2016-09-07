using UnityEngine;
using System.Collections;
using System;
using Libgame.Characters;

namespace Libgame.Components
{
    /// <summary>
    /// 生命值组件
    /// </summary>
    public class HpComponent : PointComponent
    {

        /// <summary>
        /// 当前生命值
        /// </summary>
        public float hp
        {
            get { return point; }
            set { point = hp; }
        }

#region MAX_HP
        /// <summary>
        /// 基础最大生命值
        /// </summary>
        public float baseMaxHp
        {
            get { return baseMaxPoint; }
            set { baseMaxPoint = value; }
        }

        /// <summary>
        /// 基础最大生命值增加值
        /// </summary>
        public float maxHpAddedValue
        {
            get { return maxPointAddedValue; }
            private set { maxPointAddedValue = value; }
        }

        /// <summary>
        /// 最大生命值百分比
        /// </summary>
        public float maxHpRate
        {
            get { return maxPointRate; }
            private set { maxPointRate = value; }
        }

        /// <summary>
        /// 最大生命值
        /// </summary>
        public float maxHp
        {
            get
            {
                return maxPoint;
            }
        }

        /// <summary>
        /// 最小最大生命值
        /// </summary>
        public float minMaxHp
        {
            get { return minMaxPoint; }
            set { minMaxPoint = value; }
        }

        /// <summary>
        /// 最大最大生命值
        /// </summary>
        public float maxMaxHp
        {
            get { return maxMaxPoint; }
            set { maxMaxPoint = value; }
        }
#endregion

#region HP_RECOVER
        /// <summary>
        /// 基础每秒生命恢复值
        /// </summary>
        public float baseHpRecover
        {
            get { return basePointRecover; }
            set { basePointRecover = value; }
        }

        /// <summary>
        /// 基础每秒生命恢复值增加量
        /// </summary>
        public float hpRecoverAddedValue
        {
            get { return pointRecoverAddedValue; }
            private set { pointRecoverAddedValue = value; }
        }

        /// <summary>
        /// 每秒生命恢复增加百分比
        /// </summary>
        public float hpRecoverRate
        {
            get { return pointRecoverRate; }
            private set { pointRecoverRate = value; }
        }

        /// <summary>
        /// 当前每秒生命恢复值,私有
        /// </summary>
        public float _hpRecover
        {
            get
            {
                return _pointRecover;
            }
        }

        /// <summary>
        /// 当前每秒生命恢复值
        /// </summary>
        public float hpRecover
        {
            get
            {
                return pointRecover;
            }
        }
        #endregion

        /// <summary>
        /// 显示组件的基本信息，override
        /// </summary>
        [ContextMenu("ShowDetail")]
        public override void ShowDetail()
        {
            base.ShowDetail();
        }
    }
}
