using UnityEngine;
using System.Collections;

namespace Libgame.Components
{
    /// <summary>
    /// 为一些不需要移动的人物所设
    /// </summary>
    public class NeverMoveComponent : MoveComponent
    {

        /// <summary>
        /// 判断是否可以移动,override
        /// </summary>
        /// <returns>恒为假</returns>
        public override bool CanMove()
        {
            return false;
        }
    }
}
