using UnityEngine;
using System.Collections;
using System.Threading;

namespace Libgame
{
    /// <summary>
    /// 人物
    /// </summary>
    [RequireComponent(typeof(MoveComponent))]
    public class Character : MonoBehaviour
    {

        /// <summary>
        /// 人物ID
        /// </summary>
        public int id;

        /// <summary>
        /// 人物姓名
        /// </summary>
        public string name;

        /// <summary>
        /// 人物介绍
        /// </summary>
        public string introduction;



        /// <summary>
        /// 人物所挂载的移动组件，私有
        /// </summary>
        private MoveComponent _moveComponent;

        /// <summary>
        /// 人物所挂载的移动组件
        /// </summary>
        public MoveComponent moveComponent
        {
            get
            {
                if (_moveComponent == null)
                {
                    _moveComponent = GetComponent<MoveComponent>();
                    if (_moveComponent == null)
                    {
                        _moveComponent = gameObject.AddComponent<MoveComponent>();
                    }
                }
                return _moveComponent;
            }
        }

        protected void OnDestroy()
        {
            Die(this);
        }
        #region 生死大事
        /// <summary>
        /// 是否已经死亡
        /// </summary>
        [SerializeField]
        protected bool isDead;

        /// <summary>
        /// 出生，virtual
        /// </summary>
        public virtual void Birth()
        {
            isDead = false;
        }

        /// <summary>
        /// 重生，virtual
        /// </summary>
        public virtual void Rebirth()
        {
            isDead = false;
        }

        /// <summary>
        /// 杀死一个人物，virtual
        /// </summary>
        /// <param name="p_dead">被害者</param>
        public virtual void Kill(Character p_dead)
        {

        }

        /// <summary>
        /// 死亡，virtual
        /// </summary>
        /// <param name="p_killer">杀手</param>
        /// <returns>是否死亡</returns>
        public virtual bool Die(Character p_killer)
        {
            if (p_killer == null)
            {
                p_killer = this;
            }
            if (isDead == false)
            {
                isDead = true;
                p_killer.Kill(this);
            }
            return isDead;
        }
        #endregion
    }
}