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
            ApplyChannelParameters();
            if (!Channel.IsPlaying)
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
            ApplyChannelParameters();
        }

        private void ApplyChannelParameters()
        {
        }
    }
}