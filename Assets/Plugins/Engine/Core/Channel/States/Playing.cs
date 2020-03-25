namespace AudioEngine
{
    public class Playing : State
    {
        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            Play();
        }

        public override void OnStateUpdate()
        {
            Channel.ApplyChannelParameters();
            if (!Channel.AudioSource.isPlaying)
            {
                Channel.EnterState("Stopping");
            }

            if (Channel.ShouldBeVirtual())
            {
                Channel.EnterState("Virtualizing");
            }
        }

        private void Play()
        {
            //we start to play the sound
            Channel.ApplyChannelParameters();
            Channel.AudioSource.Play();
        }
    }
}