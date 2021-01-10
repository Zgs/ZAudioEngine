using UnityEngine;

namespace AudioEngine
{
    public class State
    {
        protected AudioEvent AudioEvent;
        public string Name;

        public virtual void OnEnterState(AudioEvent audioEvent)
        {
            AudioEvent = audioEvent;
#if UNITY_EDITOR
            Debug.Log($"enter {Name}");
#endif
        }

        public virtual void OnStateExit()
        {
#if UNITY_EDITOR
            Debug.Log($"exit {Name}");
#endif
        }

        /// <summary>
        ///     on transfer,previous is exit and next will enter
        /// </summary>
        /// <param name="before"></param>
        /// <param name="after"></param>
        public virtual void OnStateTransfer(State before, State after)
        {
#if UNITY_EDITOR
            Debug.Log($"from {before.Name} to {after.Name}");
#endif
        }

        public virtual void OnStateUpdate()
        {
        }
    }
}