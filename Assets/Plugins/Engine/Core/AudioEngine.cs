using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudioEngine
{
    public class AudioEngine : SingletonMono<AudioEngine>
    {
        [SerializeField] private GameObject _audioPrefab;
        private int _audioId;
        public ILoader Loader { get; private set; }

        [NonSerialized]
        internal readonly Dictionary<string, List<AudioEvent>> Channels = new Dictionary<string, List<AudioEvent>>();

        [NonSerialized] public readonly Dictionary<int, AudioEvent> Events = new Dictionary<int, AudioEvent>();

        private void Start()
        {
            GameObjectPool.Instance.CreatePool("Audio", _audioPrefab, 20);
            ObjectPool.Instance.CreatePool<AudioEvent>(20);
            Loader = new ResourcesLoader();
        }

        private void Update()
        {
            var keys = Events.Keys;
            foreach (var key in keys)
            {
                var channel = Events[key];
                channel.Update();
            }
        }

        public int PlaySound(string eventName, Vector3? pos)
        {
            var nextId = _audioId++;
            var channel = ObjectPool.Instance.GetNext<AudioEvent>();
            channel.Setup(eventName, pos, 1, nextId);
            channel.EnterState("Initialize");
            Events.Add(nextId, channel);
            if (Channels.ContainsKey(eventName))
            {
                Channels[eventName].Add(channel);
            }
            else
            {
                var list = new List<AudioEvent> {channel};
                Channels.Add(eventName, list);
            }

            return nextId;
        }

        public void StopSound(int id)
        {
            if (Events.TryGetValue(id, out var channel))
            {
                channel.EnterState("Stopping");
            }
        }

        public void PauseSound(int id)
        {
            if (Events.TryGetValue(id, out var channel))
            {
                channel.Pause();
            }
        }

        public void UnpauseSound(int id)
        {
            if (Events.TryGetValue(id, out var channel))
            {
                channel.UnPause();
            }
        }
    }
}