namespace AudioEngine
{
    public class Stopped : State
    {
        private Channel _channel;

        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            // do some reset work and release all resources the channel used
            Channel.AudioSource.clip = null;
            Channel.AudioSource.loop = false;
            Channel.AudioSource.Stop();
            GameObjectPool.Instance.ReturnToPool(Channel.AudioSource.gameObject);
            Channel.AudioSource = null;
            Channel.EnterState("Empty");
        }
    }
}