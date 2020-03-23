using System.Collections.Generic;

namespace AudioEngine
{
    public abstract class StateMachine
    {
        public State CurentState;
        protected readonly Dictionary<string, State> States = new Dictionary<string, State>();

        public void AddState(string stateName, State state)
        {
            if (States.ContainsKey(stateName))
            {
                return;
            }
            state.Name = stateName;
            States.Add(stateName, state);
        }

        public abstract void EnterState(string stateName);
    }
}