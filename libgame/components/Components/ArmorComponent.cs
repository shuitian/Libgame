using UnityEngine;
using System.Collections;
using Libgame.Characters;

namespace Libgame.Components
{

    [System.Serializable]
    public class BaseArmorList
    {
        public int length
        {
            get
            {
                return _baseArmorList.Length;
            }
        }

        public float[] _baseArmorList;

        public float this[int index]
        {
            get
            {
                return _baseArmorList[index];
            }
            set
            {
                _baseArmorList[index] = value;
            }
        }

        public BaseArmorList()
        {

        }

        public BaseArmorList(float[] _baseArmorList)
        {
            this._baseArmorList = _baseArmorList;
        } 
    }

    [System.Serializable]
    public class ArmorAddedValueList
    {
        public int length
        {
            get
            {
                return _armorAddedValueList.Length;
            }
        }

        public float[] _armorAddedValueList;

        public float this[int index]
        {
            get
            {
                return _armorAddedValueList[index];
            }
            set
            {
                _armorAddedValueList[index] = value;
            }
        }

        public ArmorAddedValueList()
        {

        }

        public ArmorAddedValueList(float[] _armorAddedValueList)
        {
            this._armorAddedValueList = _armorAddedValueList;
        } 
    }

    [System.Serializable]
    public class ArmorList
    {
        public int length
        {
            get
            {
                return Mathf.Min(baseArmorList.length, armorAddedValueList.length);
            }
        }
        public BaseArmorList baseArmorList;
        public ArmorAddedValueList armorAddedValueList;
          
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
            baseArmorList = new BaseArmorList(new float[length]);
            armorAddedValueList = new ArmorAddedValueList(new float[length]);
        }

        public ArmorList(float[] _baseArmorList, float[] _armorAddedValueList)
        {
            baseArmorList = new BaseArmorList(_baseArmorList);
            armorAddedValueList = new ArmorAddedValueList(_armorAddedValueList);
        }
    }
    /// <summary>
    /// 护甲组件
    /// </summary>
    public class ArmorComponent : CharacterComponent
    {
        public ArmorList armorList = new ArmorList();
        //{
        //    get
        //    {
                
        //        return baseArmorList + armorAddedValueList;
        //    }
        //}

        ///// <summary>
        ///// 增加护甲值
        ///// </summary>
        ///// <param name="armorAddedValue">增加的护甲值，可负</param>
        //public void AddArmor(float armorAddedValue)
        //{
        //    this.armorAddedValue += armorAddedValue;
        //}

        ///// <summary>
        ///// 设置护甲值
        ///// </summary>
        ///// <param name="baseArmor">基础护甲值</param>
        ///// <param name="armorAddedValue">增加的护甲值</param>
        //public void SetArmor(float baseArmor, float armorAddedValue)
        //{
        //    this.baseArmor = baseArmor;
        //    this.armorAddedValue = armorAddedValue;
        //}

        ///// <summary>
        ///// 最小伤害
        ///// </summary>
        //public float minDamage = 0;

        ///// <summary>
        ///// 计算护甲抵挡后的伤害,virtual
        ///// </summary>
        ///// <param name="damage">抵挡前的伤害</param>
        ///// <returns>抵挡后的伤害</returns>
        //public virtual float CalculateDamage(float damage)
        //{
        //    return Mathf.Max(minDamage, damage * (100F / (100 + armor)));
        //}
    }
}