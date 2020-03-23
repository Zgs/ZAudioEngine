using UnityEngine;

namespace AudioEngine
{
    public class Channel : StateMachine
    {
        public bool IsPlaying;
        public AudioSource AudioSource;

        public Channel()
        {
            AddState(new Initialize());
            AddState(new Load());
            AddState(new ToPlay());
            AddState(new Playing());
            AddState(new Virtualizing());
            AddState(new Virtual());
            AddState(new Stopping());
            AddState(new Stopped());
        }

        public string EventName { get; set; }
        public float Volume { get; set; }
        public Vector3? Position { get; set; }
        public bool OnShot { get; set; }
        public bool Loaded { get; set; }

        /// <summary>
        /// setup the base information for this channel
        /// </summary>
        /// <param name="eventName">once event name is set, it can load the clip information</param>
        /// <param name="pos">position the 3D information</param>
        /// <param name="volume">the volume of this event</param>
        public void Setup(string eventName, Vector3? pos, float volume)
        {
            EventName = eventName;
            Volume = volume;
            Position = pos;
        }

        public override void EnterState(string stateName)
        {
            if (!States.ContainsKey(stateName))
            {
                return;
            }

            var previousState = CurentState;
            var state = States[stateName];

            previousState.OnStateExit();
            previousState.OnStateTransfer(previousState, state);
            state.OnStateTransfer(previousState, state);
            state.OnEnterState(this);

            CurentState = state;
        }

        public void Update()
        {
            CurentState?.OnStateUpdate();
        }

        public bool ShouldBeVirtual()
        {
            return false;
        }
    }
}