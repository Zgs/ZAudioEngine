using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioEngine
{
    public class AudioEngine : MonoBehaviour
    {
        private static AudioEngine _instance;

        public static AudioEngine Instance
        {
            get
            {
                _instance = FindObjectOfType<AudioEngine>();
                if (_instance == null)
                {
                    var gameObj = new GameObject("Audio Engine");
                    _instance = gameObj.AddComponent<AudioEngine>();
                }

                return _instance;
            }
        }

        private Dictionary<int, Channel> _channelMap = new Dictionary<int, Channel>();
        private List<int> _stopChannel = new List<int>();
        private int _channelId;

        private void Update()
        {
            var keys = _channelMap.Keys;
            foreach (var key in keys)
            {
                var channel = _channelMap[key];
                if (!channel.IsPlaying)
                {
                    _stopChannel.Add(key);
                }
            }

            for (var i = 0; i < _stopChannel.Count; i++)
            {
                var id = _stopChannel[i];
                _channelMap.Remove(id);
            }
        }

        private void LateUpdate()
        {
            _stopChannel.Clear();
        }

        public int PlaySound(string eventName, Vector3 pos, float volume)
        {
            var nextId = _channelId++;
            var channel = new Channel();
            channel.Setup(eventName, pos, volume);
            _channelMap.Add(nextId, channel);
            return nextId;
        }
    }
}