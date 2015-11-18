using System.Collections;
using UnityEngine;

namespace UnityTool.Libgame
{
    public class ObjectPool
    {
        static public ArrayList objects = new ArrayList();

        static public GameObject[] Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, int number)
        {
            GameObject[] objs = new GameObject[number];
            for (int i = 0; i < number; i++)
            {
                objs[i] = ObjectPool.Instantiate(prefab, position, rotation);
            }
            return objs;
        }

        static public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject obj = ObjectPool.Instantiate(prefab, position, rotation);
            if (parent != null)
            {
                obj.transform.SetParent(parent);
            }
            return obj;
        }

        static public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject obj = findObject(prefab);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }

        static public GameObject Instantiate(GameObject prefab)
        {
            GameObject obj = findObject(prefab);
            obj.SetActive(true);
            return obj;
        }

        static GameObject findObject(GameObject prefab)
        {
            Objects objs = null;
            foreach (Objects objectPoolTemp in objects)
            {
                if (objectPoolTemp.checkPrefab(prefab))
                {
                    objs = objectPoolTemp;
                    break;
                }
            }
            if (objs == null)
            {
                objs = new Objects();
                objs.setPrefab(prefab);
                objects.Add(objs);
            }
            GameObject obj = ((Objects)objs).getNextObjectInPool();
            return obj;
        }

        static public void Destroy(GameObject objectToDestroy)
        {
            if (objectToDestroy.activeSelf)
            {
                objectToDestroy.SetActive(false);
            }
        }

        static public void Refresh()
        {
            foreach (Objects objs in objects)
            {
                objs.clear();
            }
        }
    }


    [System.Serializable]
    class Objects
    {
        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private int size;
        [SerializeField]
        private ArrayList objects;
        protected int index = 0;

        public Objects()
        {
            init();
        }

        public void setPrefab(GameObject prefab)
        {
            this.prefab = prefab;
        }

        public bool checkPrefab(GameObject prefab)
        {
            if (prefab == this.prefab)
            {
                return true;
            }
            return false;
        }

        public void init()
        {
            size = 0;
            objects = new ArrayList();
        }

        public GameObject getNextObjectInPool()
        {
            GameObject obj = null;
            for (int i = 0; i < objects.Count; i++)
            {
                obj = objects[index % objects.Count] as GameObject;
                if (!obj)//when MonoBehaviour.Destort() was called
                {
                    obj = MonoBehaviour.Instantiate(prefab) as GameObject;
                    objects[index % objects.Count] = obj;
                    obj.SetActive(false);
                    obj.name = prefab.name + "_" + index;
                    index = (index + 1) % objects.Count;
                    return obj;
                }
                if (!obj.activeSelf)
                {
                    break;
                }
                index = (index + 1) % objects.Count;
            }
            if (objects.Count == 0 || obj.activeSelf)
            {
                //no object is available
                obj = MonoBehaviour.Instantiate(prefab) as GameObject;
                size++;
                obj.SetActive(false);
                obj.name = prefab.name + "_" + objects.Count;
                objects.Add(obj);
                index = 0;
                return obj;
            }
            index = (index + 1) % objects.Count;
            return obj;
        }

        public void clear()
        {
            foreach (GameObject obj in objects)
            {
                MonoBehaviour.Destroy(obj);
            }
            objects = new ArrayList();
            index = 0;
        }
    }
}