namespace AudioEngine
{
    public class Playing : State
    {
        public override void OnEnterState(AudioEvent audioEvent)
        {
            base.OnEnterState(audioEvent);
            Play();
        }

        public override void OnStateUpdate()
        {
            AudioEvent.ApplyChannelParameters();
            if (!AudioEvent.AudioSource.isPlaying)
            {
                AudioEvent.EnterState("Stopping");
            }

            if (AudioEvent.ShouldBeVirtual())
            {
                AudioEvent.EnterState("Virtualizing");
            }
        }

        private void Play()
        {
            //we start to play the sound
            AudioEvent.ApplyChannelParameters();
            AudioEvent.AudioSource.Play();
        }
    }
}