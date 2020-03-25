using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace AudioEngine
{
    public class Channel : StateMachine
    {
        public AudioSource AudioSource;
        public int Id;
        private bool _pause;
        private VirtualConfig _config;
        private AudioEngine _engine = AudioEngine.Instance;

        public Channel()
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
            //todo load event config
            if (pos == null)
            {
                _config = new VirtualConfig
                {
                    MaxWithinRaiduo = 0,
                    MaxPlaybackCount = 10,
                    CheckType = VirtualCheckType.PlaybackCount,
                    PlayType = PlayType.OneShot,
                    SoundType = SoundType.Effect
                };
            }
            else
            {
                _config = new VirtualConfig
                {
                    MaxWithinRaiduo = 5,
                    MaxPlaybackCount = 10,
                    CheckType = VirtualCheckType.MaxWithinRadius,
                    PlayType = PlayType.OneShot,
                    SoundType = SoundType.Effect
                };
            }
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
            switch (_config.CheckType)
            {
                case VirtualCheckType.MaxWithinRadius:
                    return MaxRadiusCheck();
                case VirtualCheckType.PlaybackCount:
                    return PlaybackCountCheck();
                case VirtualCheckType.Both:
                    return MaxRadiusCheck() || PlaybackCountCheck();
            }

            return false;
        }

        private bool MaxRadiusCheck()
        {
            if (Position == null)
            {
                return _engine.Channels[EventName].Count > 2;
            }

            var list = _engine.Channels[EventName];
            var withinCount = 0;
            for (var i = 0; i < list.Count; i++)
            {
                var channel = list[i];
                if (channel.Id == Id)
                {
                    continue;
                }

                Debug.Assert(channel.Position != null, "channel.Position != null");
                var dis = Vector3.SqrMagnitude(Position.Value - channel.Position.Value);
                if (dis > _config.MaxWithinRaiduo * _config.MaxWithinRaiduo)
                {
                    withinCount += 1;
                }
            }

            return withinCount > 2;
        }

        private bool PlaybackCountCheck()
        {
            return _engine.Channels[EventName].Count > _config.MaxPlaybackCount;
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