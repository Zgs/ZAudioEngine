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
        public override void OnEnterState(AudioEvent audioEvent)
        {
            base.OnEnterState(audioEvent);
            _passedTime = 0;
            _startVolume = AudioEvent.Volume;
            
            var list = _engine.Channels[audioEvent.EventName];
            list.Remove(audioEvent);
            if (list.Count < 1)
            {
                _engine.Channels.Remove(audioEvent.EventName);
            }
            _engine.Events.Remove(audioEvent.Id);
        }

        public override void OnStateUpdate()
        {
            AudioEvent.Volume = Mathf.Lerp(_startVolume, 0, _passedTime / GobalFadeoutTime);
            _passedTime += Time.deltaTime;
            if (Math.Abs(AudioEvent.Volume) < float.Epsilon)
            {
                AudioEvent.EnterState("Stopped");
            }
        }
    }
}