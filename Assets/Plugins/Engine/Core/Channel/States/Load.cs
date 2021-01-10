using UnityEngine;

namespace AudioEngine
{
    public class Load : State
    {
        public override void OnEnterState(AudioEvent audioEvent)
        {
            base.OnEnterState(audioEvent);
            //now it is sync when in real project it should be an async way
            AudioEvent = audioEvent;
            AudioEvent.AudioSource = GameObjectPool.Instance.GetNext("Audio").GetComponent<AudioSource>();
            AudioEvent.AudioSource.spatialBlend = AudioEvent.Position == null ? 0f : 1f;
            AudioEvent.AudioSource.transform.position = AudioEvent.Position ?? Vector3.zero;
            AudioEvent.AudioSource.volume = AudioEvent.Volume;
            AudioEvent.AudioSource.playOnAwake = false;

            var request = AudioEngine.Instance.Loader.LoadAsyncWithCallBack<AudioClip>(AudioEvent.EventName);
            request.completed += operation =>
            {
                var clip = request.asset as AudioClip;
                AudioEvent.AudioSource.clip = clip;
                AudioEvent.Loaded = true;
                AudioEvent.EnterState("ToPlay");
            };
        }
    }
}