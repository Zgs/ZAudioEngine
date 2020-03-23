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
            Channel.AudioSource.clip = null;
            Channel.AudioSource.transform.position = Channel.Position.Value;
            Channel.AudioSource.volume = Channel.Volume;
            Channel.Loaded = true;
            Channel.EnterState("ToPlay");
        }
    }
}