using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libgame.Components
{
    public class SingleResourceComponent : PointComponent
    {
        public int resourceId;
        public string resourceName;
    }

    public class SingleNoRecoverResourceComponent : SingleResourceComponent
    {
        public override bool CanRecover()
        {
            return false;
        }
    }

    public class ResourceComponent : CharacterComponent
    {
        Dictionary<int, SingleResourceComponent> singleResourceComponentDic;
        SingleResourceComponent[] singleResourceComponents;
        void Awake()
        {
            singleResourceComponentDic = new Dictionary<int, SingleResourceComponent>();
            singleResourceComponents = GetComponents<SingleResourceComponent>();
            foreach(SingleResourceComponent resourceComponent in singleResourceComponents)
            {
                if (!singleResourceComponentDic.ContainsKey(resourceComponent.resourceId))
                {
                    singleResourceComponentDic.Add(resourceComponent.resourceId, resourceComponent);
                }
            }
        }

        public bool ContainResource(int resourceId)
        {
            return singleResourceComponentDic.ContainsKey(resourceId);
        }

        public SingleResourceComponent GetSingleResourceComponentById(int id)
        {
            if (ContainResource(id))
            {
                return singleResourceComponentDic[id];
            }
            return null;
        }
    }
}
