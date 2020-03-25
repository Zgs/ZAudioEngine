namespace AudioEngine
{
    public class Stopped : State
    {
        private Channel _channel;

        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            
            // do some reset work and release all resources the channel used
            Channel.AudioSource.Stop();
            Channel.Reset();
            ObjectPool.Instance.ReturnToPool(Channel);
        }
    }
}