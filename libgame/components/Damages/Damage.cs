using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libgame.Characters;

namespace Libgame.Damages
{
    public class Damage
    {
        public Damage()
        {
            isDamaged = false;
        }

        public Damage(float[] originalDamages, float[] realDamages = null, Character source = null, Character target = null, bool isDamaged = false)
        {
            this.source = source;
            this.target = target;
            this.originalDamages = originalDamages;
            if (realDamages == null || realDamages.Length == 0)
            {
                this.realDamages = this.originalDamages;
            }
            else
            {
                this.realDamages = realDamages;
            }
            this.isDamaged = isDamaged;
        }

        /// <summary>
        /// 伤害来源
        /// </summary>
        public Character source;

        /// <summary>
        /// 原始的伤害数组
        /// </summary>
        public float[] originalDamages;
    
        /// <summary>
        /// 实际的伤害数组
        /// </summary>
        public float[] realDamages;

        /// <summary>
        /// 伤害目标
        /// </summary>
        public Character target;

        /// <summary>
        /// 是否已经造成了伤害
        /// </summary>
        public bool isDamaged;

        /// <summary>
        /// 实际的总伤害
        /// </summary>
        public float realTotalDamage;
    }
}
