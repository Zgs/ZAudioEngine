using UnityEngine;

namespace AudioEngine
{
    public class State
    {
        protected Channel Channel;
        public string Name;

        public virtual void OnEnterState(Channel channel)
        {
            Channel = channel;
            Debug.Log($"enter {Name}");
        }

        public virtual void OnStateExit()
        {
            Debug.Log($"exit {Name}");
        }

        /// <summary>
        ///     on transfer,previous is exit and next will enter
        /// </summary>
        /// <param name="before"></param>
        /// <param name="after"></param>
        public virtual void OnStateTransfer(State before, State after)
        {
            Debug.Log($"from {before.Name} to {after.Name}");
        }

        public virtual void OnStateUpdate()
        {
        }
    }
}