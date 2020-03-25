namespace AudioEngine
{
    public class ToPlay : State
    {
        public override void OnEnterState(Channel channel)
        {
            base.OnEnterState(channel);
            if (Channel.ShouldBeVirtual())
            {
                if (Channel.OnShot)
                {
                    Channel.EnterState("Stopping");
                }
                else
                {
                    Channel.EnterState("Virtual");
                }
                return;
            }

            if (!Channel.Loaded)
            {
                Channel.EnterState("Load");
                return;
            }

            Channel.EnterState("Playing");
        }
    }
}