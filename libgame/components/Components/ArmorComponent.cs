using UnityEngine;
using System.Collections;
using Libgame.Characters;

namespace Libgame.Components
{
    /// <summary>
    /// 护甲组件
    /// </summary>
    public class ArmorComponent : CharacterComponent
    {
        /// <summary>
        /// 基础护甲值,private
        /// </summary>
        [SerializeField]
        protected float _baseArmor;

        /// <summary>
        /// 基础护甲值
        /// </summary>
        public float baseArmor
        {
            get { return _baseArmor; }
            private set { _baseArmor = value; }
        }

        /// <summary>
        /// 护甲增加值,private
        /// </summary>
        [SerializeField]
        protected float _armorAddedValue;

        /// <summary>
        /// 护甲增加值
        /// </summary>
        public float armorAddedValue
        {
            get { return _armorAddedValue; }
            private set { _armorAddedValue = value; }
        }

        /// <summary>
        /// 护甲值
        /// </summary>
        public float armor
        {
            get
            {
                return baseArmor + armorAddedValue;
            }
        }

        /// <summary>
        /// 增加护甲值
        /// </summary>
        /// <param name="armorAddedValue">增加的护甲值，可负</param>
        public void AddArmor(float armorAddedValue)
        {
            this.armorAddedValue += armorAddedValue;
        }

        /// <summary>
        /// 设置护甲值
        /// </summary>
        /// <param name="baseArmor">基础护甲值</param>
        /// <param name="armorAddedValue">增加的护甲值</param>
        public void SetArmor(float baseArmor, float armorAddedValue)
        {
            this.baseArmor = baseArmor;
            this.armorAddedValue = armorAddedValue;
        }

        /// <summary>
        /// 最小伤害
        /// </summary>
        public float minDamage = 0;

        /// <summary>
        /// 计算护甲抵挡后的伤害,virtual
        /// </summary>
        /// <param name="damage">抵挡前的伤害</param>
        /// <returns>抵挡后的伤害</returns>
        public virtual float CalculateDamage(float damage)
        {
            return Mathf.Max(minDamage, damage * (100F / (100 + armor)));
        }
    }
}