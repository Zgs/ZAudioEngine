namespace AudioEngine
{
    public class Initialize : State
    {
        public override void OnEnterState(AudioEvent audioEvent)
        {
            //todo do some initialize work
            base.OnEnterState(audioEvent);
            AudioEvent.Loaded = false;
            AudioEvent.EnterState("ToPlay");
        }
    }
}