using System;
using UnityEngine;

namespace AudioEngine
{
    public class Virtualizing : State
    {
        private bool _fadeout;
        private float _passedTime;
        private float _startVolume;
        private readonly float _gobalFadeoutTime = 0.3f;

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
                Channel.EnterState("Virtual");
            }

            if (!Channel.ShouldBeVirtual())
            {
                Channel.EnterState("Playing");
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _fadeout = false;
        }
    }
}