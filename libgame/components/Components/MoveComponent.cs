using UnityEngine;
using System.Collections;

namespace Libgame.Components
{
    /// <summary>
    /// 移动组件
    /// </summary>
    public class MoveComponent : CharacterComponent
    {

        public GameObject moveObject;
        // Update is called once per frame
        void Update()
        {
            Move();
        }

        /// <summary>
        /// 是否可以移动，玩家由触摸控制，怪物和NPC由AI控制
        /// </summary>
        protected bool canMove = false;

        /// <summary>
        /// 设置是否可以移动
        /// </summary>
        /// <param name="p_canMove">是否可以移动</param>
        public void SetCanMove(bool p_canMove)
        {
            this.canMove = p_canMove;
        }

        /// <summary>
        /// 是否可以移动，virtual
        /// </summary>
        /// <returns>是返回真，反之返回假</returns>
        public virtual bool CanMove()
        {
            return canMove;
        }

        /// <summary>
        /// 移动，virtual
        /// </summary>
        public virtual void Move()
        {
            if (CanMove() && moveObject && moveDirect != Vector3.zero)
                moveObject.transform.position += moveDirect.normalized * moveSpeed * Time.deltaTime;
        }

#region 移动方向
        /// <summary>
        /// 移动方向，动态改变
        /// </summary>
        [SerializeField]
        protected Vector3 moveDirect = Vector3.zero;

        /// <summary>
        /// 获取移动方向
        /// </summary>
        /// <returns>移动方向</returns>
        public Vector3 GetMoveDirect()
        {
            return moveDirect;
        }

        /// <summary>
        /// 改变移动方向
        /// </summary>
        /// <param name="p_newMoveDirect">新的移动方向</param>
        public void ChangeMoveDirect(Vector3 p_newMoveDirect)
        {
            moveDirect = p_newMoveDirect;
        }
#endregion

#region 移动速度
        /// <summary>
        /// 最大移动速度
        /// </summary>
        static public float maxMoveSpeed = 1000;

        /// <summary>
        /// 最小移动速度
        /// </summary>
        static public float minMoveSpeed = 0;

        /// <summary>
        /// 基础移动速度
        /// </summary>
        static public float baseMoveSpeed = 100;

        /// <summary>
        /// 基础移动速度增加值，私有
        /// </summary>
        [SerializeField]
        private float _moveSpeedAddedValue = 0;

        /// <summary>
        /// 基础移动速度增加值
        /// </summary>
        public float moveSpeedAddedValue
        {
            get { return _moveSpeedAddedValue; }
            private set { _moveSpeedAddedValue = value; }
        }
        /// <summary>
        /// 移动速度系数，私有
        /// </summary>
        [SerializeField]
        private float _moveSpeedAddedRate = 1;

        /// <summary>
        /// 移动速度系数
        /// </summary>
        public float moveSpeedAddedRate
        {
            get { return _moveSpeedAddedRate; }
            private set { _moveSpeedAddedRate = value; }
        }

        /// <summary>
        /// 移动速度，私有
        /// </summary>
        private float _moveSpeed
        {
            get
            {
                return (baseMoveSpeed + moveSpeedAddedValue) * moveSpeedAddedRate;
            }
        }

        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed
        {
            get
            {
                return UnityEngine.Mathf.Min(maxMoveSpeed, UnityEngine.Mathf.Max(minMoveSpeed, _moveSpeed));
            }
        }
        /// <summary>
        /// 增加移动速度
        /// </summary>
        /// <param name="p_moveSpeedAddedValue">增加的基础移动速度增加值</param>
        /// <param name="p_moveSpeedAddedRate">增加的移动速度增加百分比</param>
        /// <returns>设置是否成功</returns>
        public bool AddMoveSpeed(float p_moveSpeedAddedValue, float p_moveSpeedAddedRate)
        {
            return SetMoveSpeed(this.moveSpeedAddedValue + p_moveSpeedAddedValue, this.moveSpeedAddedRate + p_moveSpeedAddedRate);
        }

        /// <summary>
        /// 设置移动速度
        /// </summary>
        /// <param name="p_moveSpeedAddedValue">新的基础移动速度增加值</param>
        /// <param name="p_moveSpeedAddedRate">新的移动速度增加百分比</param>
        /// <returns>设置是否成功</returns>
        public bool SetMoveSpeed(float p_moveSpeedAddedValue, float p_moveSpeedAddedRate)
        {
            this.moveSpeedAddedValue = p_moveSpeedAddedValue;
            this.moveSpeedAddedRate = p_moveSpeedAddedRate;
            return true;
        }
#endregion

        /// <summary>
        /// 显示组件的基本信息，override
        /// </summary>
        [ContextMenu("ShowDetail")]
        public override void ShowDetail()
        {
            MonoBehaviour.print(
                "[" + gameObject + "] => "
                + "canMove: " + canMove + ", moveDirect: " + moveDirect + ", moveSpeed: " + moveSpeed
                + ", [static]baseMoveSpeed: " + baseMoveSpeed + ", moveSpeedAddedValue: " + moveSpeedAddedValue + ", moveSpeedAddedRate: " + moveSpeedAddedRate
                + ", [static]maxMoveSpeed: " + maxMoveSpeed + ", [static]minMoveSpeed: " + minMoveSpeed);
        }
    }
}