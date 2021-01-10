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

        public override void OnEnterState(AudioEvent audioEvent)
        {
            base.OnEnterState(audioEvent);
            _passedTime = 0;
            _startVolume = AudioEvent.Volume;
        }

        public override void OnStateUpdate()
        {
            AudioEvent.Volume = Mathf.Lerp(_startVolume, 0, _passedTime / _gobalFadeoutTime);
            _passedTime += Time.deltaTime;
            if (Math.Abs(AudioEvent.Volume) < float.Epsilon)
            {
                AudioEvent.EnterState("Virtual");
            }

            if (!AudioEvent.ShouldBeVirtual())
            {
                AudioEvent.EnterState("Playing");
            }
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _fadeout = false;
        }
    }
}