using UnityEngine;

namespace AudioEngine
{
    public class Load : State
    {
        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            //now it is sync when in real project it should be an async way
            Channel = channel;
            Channel.AudioSource = GameObjectPool.Instance.GetNext("Audio").GetComponent<AudioSource>();
            Channel.AudioSource.spatialBlend = Channel.Position == null ? 0f : 1f;
            Channel.AudioSource.transform.position = Channel.Position ?? Vector3.zero;
            Channel.AudioSource.volume = Channel.Volume;
            Channel.AudioSource.playOnAwake = false;

            var request = AudioEngine.Instance.Loader.LoadAsyncWithCallBack<AudioClip>(Channel.EventName);
            request.completed += operation =>
            {
                var clip = request.asset as AudioClip;
                Channel.AudioSource.clip = clip;
                Channel.Loaded = true;
                Channel.EnterState("ToPlay");
            };
        }
    }
}