using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AudioEngine
{
    public class GameObjectPool : SingletonMono<GameObjectPool>
    {
        public Dictionary<string, Pool> PoolDict = new Dictionary<string, Pool>();

        public void CreatePool(string poolName, GameObject poolObj, int initSize)
        {
            if (PoolDict.ContainsKey(poolName))
            {
                Debug.LogWarning($"PoolName {poolName} is already exist!!!");
                return;
            }

            var pool = new Pool(poolName, poolObj, initSize, gameObject);
            PoolDict.Add(poolName, pool);
        }

        public GameObject GetNext(string poolName)
        {
            if (!PoolDict.ContainsKey(poolName))
            {
                Debug.LogWarning($"PoolName {poolName} is not exist!!!");
                return null;
            }

            var pool = PoolDict[poolName];
            return pool.GetNextItem();
        }

        public void ReturnToPool(GameObject gameObj)
        {
            var poolName = gameObj.GetComponent<PoolTag>().PoolName;
            var pool = PoolDict[poolName];
            pool.ReturnToQueue(gameObj);
        }

        public class Pool
        {
            private int _availableCount;
            private readonly string _poolName;
            private readonly GameObject _poolObj;
            private readonly GameObject _poolObjRoot;
            private readonly Queue<GameObject> _queue;

            public Pool(string poolName, GameObject poolObj, int initSize, GameObject rootObj)
            {
                _poolObjRoot = new GameObject(poolName);
                _poolObjRoot.transform.SetParent(rootObj.transform);
                _poolName = poolName;
                _poolObj = poolObj;
                _queue = new Queue<GameObject>();
                for (var i = 0; i < initSize; i++)
                {
                    var gameObject = Object.Instantiate(poolObj, _poolObjRoot.transform, true);
                    gameObject.SetActive(false);
                    gameObject.transform.localPosition = Vector3.zero;
                    _queue.Enqueue(gameObject);
                }

                _availableCount = initSize;
            }

            public GameObject GetNextItem()
            {
                GameObject ret;
                if (_availableCount > 0)
                {
                    ret = _queue.Dequeue();
                    _availableCount -= 1;
                    ret.SetActive(true);
                }
                else
                {
                    ret = Instantiate(_poolObj);
                }

                if (ret)
                {
                    ret.SetActive(true);
                    var tag = ret.GetComponent<PoolTag>();
                    if (tag == null)
                    {
                        tag = ret.AddComponent<PoolTag>();
                    }

                    tag.PoolName = _poolName;
                    ret.transform.SetParent(null);
                }

                return ret;
            }

            public void ReturnToQueue(GameObject item)
            {
                _queue.Enqueue(item);
                item.transform.SetParent(_poolObjRoot.transform);
                item.transform.localPosition = Vector3.zero;
                item.SetActive(false);
                _availableCount += 1;
            }
        }

        public class PoolTag : MonoBehaviour
        {
            public string PoolName;
        }
    }
}