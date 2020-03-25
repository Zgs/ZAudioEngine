using System;
using UnityEngine;

namespace AudioEngine
{
    public class Stopping : State
    {
        private readonly AudioEngine _engine = AudioEngine.Instance;
        private const float GobalFadeoutTime = 0.3f;
        private int _state;
        private float _passedTime;
        private float _startVolume;
        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            _passedTime = 0;
            _startVolume = Channel.Volume;
            
            var list = _engine.Channels[channel.EventName];
            list.Remove(channel);
            if (list.Count < 1)
            {
                _engine.Channels.Remove(channel.EventName);
            }
            _engine.ChannelMap.Remove(channel.Id);
        }

        public override void OnStateUpdate()
        {
            Channel.Volume = Mathf.Lerp(_startVolume, 0, _passedTime / GobalFadeoutTime);
            _passedTime += Time.deltaTime;
            if (Math.Abs(Channel.Volume) < float.Epsilon)
            {
                Channel.EnterState("Stopped");
            }
        }
    }
}