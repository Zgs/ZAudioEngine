namespace AudioEngine
{
    public class Virtualizing : State
    {
        
        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            if (!Channel.ShouldBeVirtual())
            {
                Channel.EnterState("Playing");
            }
        }
    }
}