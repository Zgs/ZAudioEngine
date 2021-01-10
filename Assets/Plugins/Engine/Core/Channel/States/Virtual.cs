namespace AudioEngine
{
    public class Virtual : State
    {
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            if (!AudioEvent.ShouldBeVirtual())
            {
                AudioEvent.EnterState("ToPlay");
            }

            if (!AudioEvent.AudioSource.isPlaying)
            {
                AudioEvent.EnterState("Stopping");
            }
        }
    }
}