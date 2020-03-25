namespace AudioEngine
{
    public class Initialize : State
    {
        public override void OnEnterState(Channel channel)
        {
            //todo do some initialize work
            base.OnEnterState(channel);
            Channel.Loaded = false;
            Channel.EnterState("ToPlay");
        }
    }
}