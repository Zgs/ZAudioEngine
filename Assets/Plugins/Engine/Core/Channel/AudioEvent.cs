using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace AudioEngine
{
    public sealed class AudioEvent : StateMachine
    {
        public AudioSource AudioSource;
        public int Id;
        private bool _pause;
        private AudioEngine _engine = AudioEngine.Instance;

        public AudioEvent()
        {
            AddState("Initialize", new Initialize());
            AddState("Empty", new Empty());
            AddState("Load", new Load());
            AddState("ToPlay", new ToPlay());
            AddState("Playing", new Playing());
            AddState("Virtualizing", new Virtualizing());
            AddState("Virtual", new Virtual());
            AddState("Stopping", new Stopping());
            AddState("Stopped", new Stopped());

            EnterState("Empty");
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
        /// <param name="id">id of channel</param>
        public void Setup(string eventName, Vector3? pos, float volume, int id)
        {
            EventName = eventName;
            Volume = volume;
            Position = pos;
            Id = id;
        }

        public override void EnterState(string stateName)
        {
            if (!States.ContainsKey(stateName))
            {
                return;
            }

            var previousState = CurentState;
            var state = States[stateName];
            if (previousState != null)
            {
                previousState.OnStateExit();
                previousState.OnStateTransfer(previousState, state);
                state.OnStateTransfer(previousState, state);
            }

            CurentState = state;
            state.OnEnterState(this);
        }

        public void Reset()
        {
            GameObjectPool.Instance.ReturnToPool(AudioSource.gameObject);
            AudioSource.clip = null;
            AudioSource.loop = false;
            AudioSource = null;
            EnterState("Empty");
        }

        public void Update()
        {
            if (_pause)
            {
                return;
            }

            CurentState?.OnStateUpdate();
        }

        public bool ShouldBeVirtual()
        {
            return false;
        }

        public void ApplyChannelParameters()
        {
        }

        public void Pause()
        {
            AudioSource.Pause();
            _pause = true;
        }

        public void UnPause()
        {
            AudioSource.UnPause();
            _pause = false;
        }
    }
}