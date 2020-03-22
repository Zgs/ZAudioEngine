using UnityEngine;

namespace AudioEngine
{
    public class SingletonMono<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    var gameObj = new GameObject(typeof(T).Name);
                    _instance = gameObj.AddComponent<T>();
                }

                return _instance;
            }
        }
    }
}