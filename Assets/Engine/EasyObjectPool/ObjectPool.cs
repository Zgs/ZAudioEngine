﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public static ObjectPool _instance;

    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ObjectPool();
            }

            return _instance;
        }
    }

    private Dictionary<string, Queue<object>> _poolDict = new Dictionary<string, Queue<object>>();

    public void CreatePool<T>(int initSize) where T : new()
    {
        string poolName = typeof(T).Name;
        if (_poolDict.ContainsKey(poolName))
        {
            Debug.Log("something wrong, pool is already exist");
            return;
        }

        Queue<object> queue = new Queue<object>();
        for (int i = 0; i < initSize; i++)
        {
            queue.Enqueue(new T());
        }
        _poolDict.Add(poolName, queue);
    }

    public T GetNext<T>() where T : new()
    {
        //string poolName = typeof(T).Name;
        // Pool pool = _poolDict[poolName];
        return new T();
    }

    public void ReturnToPool<T>(T obj)
    {
        //string poolName = typeof(T).Name;
        //Pool pool = _poolDict[poolName];
        //pool.ReturnToPool(obj);
    }
}