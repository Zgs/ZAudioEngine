using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AudioEngine
{
    public interface ILoader
    {
        T LoadSync<T>(string assetName) where T : Object;
        T LoadSync<T>(string bundleName, string assetName) where T : Object;
        Task<T> LoadAsync<T>(string assetName) where T : Object;
        Task<T> LoadAsync<T>(string bundleName, string assetName) where T : Object;
        
        ResourceRequest LoadAsyncWithCallBack<T>(string assetName) where T : Object;

        ResourceRequest LoadAsyncWithCallBack<T>(string bundleName, string assetName) where T : Object;
    }
}