namespace AudioEngine
{
    public class Virtual : State
    {
        public override void OnStateUpdate()
        {
            base.OnStateUpdate();
            if (!Channel.ShouldBeVirtual())
            {
                Channel.EnterState("ToPlay");
            }

            if (!Channel.AudioSource.isPlaying)
            {
                Channel.EnterState("Stopping");
            }
        }
    }
}