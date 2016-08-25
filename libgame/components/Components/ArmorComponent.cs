using UnityEngine;
using System.Collections;
using Libgame.Characters;
using Libgame.Damages;

namespace Libgame.Components
{
    [System.Serializable]
    public class ArmorList
    {
        public int length
        {
            get
            {
                return Mathf.Min(baseArmorList.Length, armorAddedValueList.Length);
            }
        }
        public float[] baseArmorList;
        public float[] armorAddedValueList;
          
        public float this[int index]
        {
            get
            {
                if (index < length)
                {
                    return baseArmorList[index] + armorAddedValueList[index];
                }
                else
                {
                    Debug.LogError("Get index: " + index + " from ArmorList error! The Length is " + length + "!");
                    return 0;
                }
            }
        }

        public ArmorList()
        {
        }

        public ArmorList(int length)
        {
            baseArmorList =new float[length];
            armorAddedValueList = new float[length];
        }

        public ArmorList(float[] _baseArmorList, float[] _armorAddedValueList)
        {
            baseArmorList = _baseArmorList;
            armorAddedValueList = _armorAddedValueList;
        }
    }
    /// <summary>
    /// 护甲组件
    /// </summary>
    public class ArmorComponent : CharacterComponent
    {
        /// <summary>
        /// 护甲类
        /// </summary>
        public ArmorList armorList = new ArmorList();

        /// <summary>
        /// 增加护甲值
        /// </summary>
        /// <param name="armorAddedValueList">增加的护甲值列表，可负</param>
        public void AddArmor(float[] armorAddedValueList)
        {
            for(int i = 0; i < armorAddedValueList.Length; i++)
            {
                AddArmor(i, armorAddedValueList[i]);
            }
        }

        /// <summary>
        /// 增加单个护甲值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="armorAddedValue"></param>
        public void AddArmor(int index, float armorAddedValue)
        {
            if(index < this.armorList.armorAddedValueList.Length)
            {
                this.armorList.armorAddedValueList[index] += armorAddedValue;
            }
        }

        /// <summary>
        /// 设置护甲值
        /// </summary>
        /// <param name="baseArmorList">基础护甲值列表</param>
        /// <param name="armorAddedValueList">增加的护甲值列表</param>
        public void SetArmor(float[] baseArmorList, float[] armorAddedValueList)
        {
            this.armorList.baseArmorList = baseArmorList;
            this.armorList.armorAddedValueList = armorAddedValueList;
        }

        /// <summary>
        /// 设置护甲值
        /// </summary>
        /// <param name="armorList">新的护甲值类</param>
        public void SetArmor(ArmorList armorList)
        {
            this.armorList = armorList;
        }

        /// <summary>
        /// 计算护甲抵挡后的伤害,virtual
        /// </summary>
        /// <param name="damage">抵挡前的伤害</param>
        /// <returns>抵挡后的伤害</returns>
        public virtual Damage CalculateDamage(Damage damage)
        {
            for(int i = 0; i < damage.realDamages.Length; i++)
            {
                damage.realDamages[i] *= (100F) / (100 + armorList[i]);
            }
            return damage;
        }
    }
}