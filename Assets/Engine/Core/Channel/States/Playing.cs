using UnityEngine;

namespace AudioEngine
{
    public class Playing : State
    {
        private string _previous;

        public override void OnStateTransfer(State before, State after)
        {
            base.OnStateTransfer(before, after);
            _previous = after == this ? before.Name : string.Empty;
        }

        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            switch (_previous)
            {
                case "Virtualizing":
                    Play();
                    break;
                default:
                    Channel.EnterState("Virtualizing");
                    break;
            }
        }

        public override void OnStateUpdate()
        {
            if (!Channel.IsPlaying)
            {
                Channel.EnterState("Stopping");
            }
        }

        private void Play()
        {
            //we start to play the sound
        }
    }
}