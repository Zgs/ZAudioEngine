using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudioEngine
{
    public class AudioEngine : SingletonMono<AudioEngine>
    {
        [SerializeField] private GameObject _audioPrefab;
        private int _channelId;
        public ILoader Loader { get; private set; }

        [NonSerialized]
        internal readonly Dictionary<string, List<Channel>> Channels = new Dictionary<string, List<Channel>>();

        [NonSerialized] public readonly Dictionary<int, Channel> ChannelMap = new Dictionary<int, Channel>();

        private void Start()
        {
            GameObjectPool.Instance.CreatePool("Audio", _audioPrefab, 20);
            ObjectPool.Instance.CreatePool<Channel>(20);
            Loader = new ResourcesLoader();
        }

        private void Update()
        {
            var keys = ChannelMap.Keys;
            foreach (var key in keys)
            {
                var channel = ChannelMap[key];
                channel.Update();
            }
        }

        public int PlaySound(string eventName, Vector3? pos)
        {
            var nextId = _channelId++;
            var channel = ObjectPool.Instance.GetNext<Channel>();
            channel.Setup(eventName, pos, 1, nextId);
            channel.EnterState("Initialize");
            ChannelMap.Add(nextId, channel);
            if (Channels.ContainsKey(eventName))
            {
                Channels[eventName].Add(channel);
            }
            else
            {
                var list = new List<Channel> {channel};
                Channels.Add(eventName, list);
            }

            return nextId;
        }

        public void StopSound(int id)
        {
            if (ChannelMap.TryGetValue(id, out var channel))
            {
                channel.EnterState("Stopping");
            }
        }

        public void PauseSound(int id)
        {
            if (ChannelMap.TryGetValue(id, out var channel))
            {
                channel.Pause();
            }
        }

        public void UnpauseSound(int id)
        {
            if (ChannelMap.TryGetValue(id, out var channel))
            {
                channel.UnPause();
            }
        }
    }
}