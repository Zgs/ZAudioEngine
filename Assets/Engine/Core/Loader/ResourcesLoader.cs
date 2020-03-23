using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace AudioEngine
{
    public class ResourcesLoader : ILoader
    {
        public T LoadSync<T>(string assetName) where T : Object
        {
            return Resources.Load<T>(assetName);
        }

        public T LoadSync<T>(string bundleName, string assetName) where T : Object
        {
            string path = Path.Combine(bundleName, assetName);
            return LoadSync<T>(path);
        }

        public Task<T> LoadAsync<T>(string assetName) where T : Object
        {
            return null;
        }

        public async Task<T> LoadAsync<T>(string bundleName, string assetName) where T : Object
        {
            string path = Path.Combine(bundleName, assetName);
            return await LoadAsync<T>(path);
        }

        public ResourceRequest LoadAsyncWithCallBack<T>(string assetName) where T : Object
        {
            return Resources.LoadAsync<T>(assetName);
        }

        public ResourceRequest LoadAsyncWithCallBack<T>(string bundleName, string assetName) where T : Object
        {
            string path = Path.Combine(bundleName, assetName);
            return LoadAsyncWithCallBack<T>(path);
        }
    }
}