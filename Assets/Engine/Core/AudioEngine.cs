using System.Collections.Generic;
using UnityEngine;

namespace AudioEngine
{
    public class AudioEngine : SingletonMono<AudioEngine>
    {
        private readonly Dictionary<int, Channel> _channelMap = new Dictionary<int, Channel>();
        [SerializeField] private GameObject _audioPrefab;
        private int _channelId;
        public ILoader Loader;

        private void Start()
        {
            GameObjectPool.Instance.CreatePool("Audio", _audioPrefab, 20);
            ObjectPool.Instance.CreatePool<Channel>(20);
            Loader = new ResourcesLoader();
        }

        private void Update()
        {
            var keys = _channelMap.Keys;
            foreach (var key in keys)
            {
                var channel = _channelMap[key];
                channel.Update();
            }

            // for (var i = 0; i < _stopChannel.Count; i++)
            // {
            //     var id = _stopChannel[i];
            //     _channelMap.Remove(id);
            // }
            // _stopChannel.Clear();
        }

        public int PlaySound(string eventName, Vector3? pos, float volume = 0.5f)
        {
            var nextId = _channelId++;
            var channel = ObjectPool.Instance.GetNext<Channel>();
            channel.Setup(eventName, pos, volume);
            channel.EnterState("Initialize");
            _channelMap.Add(nextId, channel);
            return nextId;
        }

        public void StopSound(int id)
        {
            var channel = _channelMap[id];
            channel.EnterState("Stopping");
            _channelMap.Remove(id);
        }

        public void PauseSound(int id)
        {
        }
    }
}