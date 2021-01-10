namespace AudioEngine
{
    public class ToPlay : State
    {
        public override void OnEnterState(AudioEvent audioEvent)
        {
            base.OnEnterState(audioEvent);
            if (AudioEvent.ShouldBeVirtual())
            {
                if (AudioEvent.OnShot)
                {
                    AudioEvent.EnterState("Stopping");
                }
                else
                {
                    AudioEvent.EnterState("Virtual");
                }
                return;
            }

            if (!AudioEvent.Loaded)
            {
                AudioEvent.EnterState("Load");
                return;
            }

            AudioEvent.EnterState("Playing");
        }
    }
}