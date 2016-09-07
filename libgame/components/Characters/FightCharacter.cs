using UnityEngine;
using System.Collections;
using System.Collections.Generic ;
using Libgame.Components;
using Libgame.Bridge;
using Libgame.Damages;

namespace Libgame.Characters
{
    /// <summary>
    /// 战斗人物
    /// </summary>
    [RequireComponent(typeof(HpDieBridge))]
    [RequireComponent(typeof(BaseNoArmorComponent))]
    [RequireComponent(typeof(BaseAttackComponent))]
    public class FightCharacter : Character
    {

        /// <summary>
        /// 生命值组件，私有
        /// </summary>
        protected HpComponent _hpComponent;

        /// <summary>
        /// 生命值组件
        /// </summary>
        public HpComponent hpComponent
        {
            get
            {
                if (_hpComponent==null)
                {
                    _hpComponent = GetComponent<HpComponent>();
                }
                if (_hpComponent == null)
                {
                    _hpComponent = gameObject.AddComponent<HpComponent>();
                }
                return _hpComponent;
            }
        }

        /// <summary>
        /// 护甲值组件，私有
        /// </summary>
        protected BaseNoArmorComponent _armorComponent;

        /// <summary>
        /// 护甲值组件
        /// </summary>
        public BaseNoArmorComponent armorComponent
        {
            get
            {
                if (_armorComponent == null)
                {
                    _armorComponent = GetComponent<BaseNoArmorComponent>();
                }
                if (_armorComponent == null)
                {
                    _armorComponent = gameObject.AddComponent<BaseNoArmorComponent>();
                }
                return _armorComponent;
            }
        }

        /// <summary>
        /// 攻击组件，私有
        /// </summary>
        protected BaseAttackComponent _attackComponent;

        /// <summary>
        /// 攻击组件
        /// </summary>
        public BaseAttackComponent attackComponent
        {
            get
            {
                if (_attackComponent == null)
                {
                    _attackComponent = GetComponent<BaseAttackComponent>();
                }
                if (_attackComponent == null)
                {
                    _attackComponent = gameObject.AddComponent<BaseAttackComponent>();
                }
                return _attackComponent;
            }
        }

        /// <summary>
        /// 受到伤害
        /// </summary>
        /// <param name="damage">受到的伤害</param>
        public virtual void TakeDamages(Damage damage)
        {
            BaseNoArmorComponent armor = GetComponent<BaseNoArmorComponent>();
            Damage newDamage = armor.CalculateDamage(damage);
            this.hpComponent.LosePoint(newDamage.source, newDamage.realTotalDamage);
        }
        ///// <summary>
        ///// TODO
        /////     身上所穿得装备
        ///// </summary>
        //public Dictionary<ItemType.EquipmentTypeEnum, Equipment> equipmentList;

        //protected void Awake()
        //{
        //    base.Awake();
        //    hpComponent = GetComponent<HpComponent>();
        //    elementIntensityAndDefenceComponent = GetComponent<ElementIntensityAndDefenceComponent>();
        //    equipmentList = new Dictionary<ItemType.EquipmentTypeEnum, Equipment>();
        //}

        ///// <summary>
        ///// 收到原子技能的伤害
        ///// </summary>
        ///// <param name="skill">原子技能</param>
        //public void GetSkillDamaged(AtomicSkill skill)
        //{
        //    if (!hpComponent || !skill)
        //    {
        //        return;
        //    }
        //    GetDamaged(skill.skillAttribute.damage);
        //}

        ///// <summary>
        ///// 受到技能的伤害
        ///// </summary>
        ///// <param name="p_skillDamage">受到的技能</param>
        //public void GetDamaged(Damage.SkillDamage p_skillDamage)
        //{
        //    if (!hpComponent || p_skillDamage == null) 
        //    {
        //        return;
        //    }
        //    hpComponent.LoseHp(Damage.CalculateDamage(this, p_skillDamage));
        //}

        ///// <summary>
        ///// 释放技能
        ///// </summary>
        ///// <param name="skill">被释放的技能</param>
        //public void ReleaseSkill(Skill skill)
        //{
        //    if (!skill)
        //    {
        //        return;
        //    }
        //    skill.owner = this;
        //    skill.SetDamageByIntensity(elementIntensityAndDefenceComponent);
        //    skill.Release();
        //}
    }

    /// <summary>
    /// 双曲线护甲战斗人物
    /// </summary>
    [RequireComponent(typeof(HyperbolaArmorComponent))]
    public class HArmorFightCharacter : FightCharacter
    {
    }

    /// <summary>
    /// 线性护甲战斗人物
    /// </summary>
    [RequireComponent(typeof(LinearArmorComponent))]
    public class LArmorFightCharacter : FightCharacter
    {
    }
}