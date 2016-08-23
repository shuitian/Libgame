using UnityEngine;

namespace Libgame
{
    /// <summary>
    /// 基础组件，所以组件的父类
    /// </summary>
    public class BaseComponent : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 显示组件的基本信息，virtual
        /// </summary>
        public virtual void ShowDetail()
        {
        }
    }
}