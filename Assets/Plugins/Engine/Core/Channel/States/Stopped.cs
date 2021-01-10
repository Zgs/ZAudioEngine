namespace AudioEngine
{
    public class Stopped : State
    {
        private AudioEvent _audioEvent;

        public override void OnEnterState(AudioEvent audioEvent)
        {
            base.OnEnterState(audioEvent);
            
            // do some reset work and release all resources the channel used
            AudioEvent.AudioSource.Stop();
            AudioEvent.Reset();
            ObjectPool.Instance.ReturnToPool(AudioEvent);
        }
    }
}