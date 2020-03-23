using UnityEngine;

namespace AudioEngine
{
    public class ToPlay : State
    {
        private string _previous;

        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            switch (_previous)
            {
                case "Initialize":
                    Channel.EnterState("Load");
                    break;
                case "Load":
                    Channel.EnterState("Playing");
                    break;
                case "":
                case null:
                    Debug.LogWarning("something wrong");
                    break;
            }
        }

        public override void OnStateTransfer(State before, State after)
        {
            _previous = after == this ? before.Name : string.Empty;
        }
    }
}