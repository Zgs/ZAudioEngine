using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameObjectPool
{
    public Dictionary<string, Pool> PoolDict = new Dictionary<string, Pool>();

    public void CreatePool(string poolName, GameObject poolObj, int initSize, int maxSize)
    {
        if (PoolDict.ContainsKey(poolName))
        {
            Debug.LogWarning($"PoolName {poolName} is already exist!!!");
            return;
        }

        var pool = new Pool(poolName, poolObj, initSize, maxSize);
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

    public void ReturnToPool(GameObject gameObject)
    {
        var poolName = gameObject.GetComponent<PoolTag>().PoolName;
        var pool = PoolDict[poolName];
        pool.ReturnToQueue(gameObject);
    }
}

public class Pool
{
    public string PoolName;
    public GameObject PoolObj;
    public int InitSize;
    public int MaxSize;
    private Queue<GameObject> _queue;

    public Pool(string poolName, GameObject poolObj, int initSize, int maxSize)
    {
        PoolName = poolName;
        var poolTag = poolObj.AddComponent<PoolTag>();
        poolTag.PoolName = poolName;
        PoolObj = poolObj;
        InitSize = initSize;
        MaxSize = Math.Max(initSize, maxSize);
        _queue = new Queue<GameObject>(MaxSize);
        for (var i = 0; i < initSize; i++)
        {
            var gameObject = Object.Instantiate(poolObj);
            gameObject.SetActive(false);
            _queue.Enqueue(gameObject);
        }
    }

    public GameObject GetNextItem()
    {
        return null;
    }

    public void ReturnToQueue(GameObject item)
    {
        _queue.Enqueue(item);
    }
}

public class PoolTag : MonoBehaviour
{
    public string PoolName;
}