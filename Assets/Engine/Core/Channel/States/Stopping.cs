using System;
using UnityEngine;

namespace AudioEngine
{
    public class Stopping : State
    {
        private readonly float _gobalFadeoutTime = 0.3f;
        private int _state;
        private float _passedTime;
        private float _startVolume;
        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            _passedTime = 0;
            _startVolume = Channel.Volume;
        }

        public override void OnStateUpdate()
        {
            Channel.Volume = Mathf.Lerp(_startVolume, 0, _passedTime / _gobalFadeoutTime);
            _passedTime += Time.deltaTime;
            if (Math.Abs(Channel.Volume) < float.Epsilon)
            {
                Channel.EnterState("Stopped");
            }
        }
        
    }
}