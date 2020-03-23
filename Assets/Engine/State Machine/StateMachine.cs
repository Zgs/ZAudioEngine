using System.Collections.Generic;

namespace AudioEngine
{
    public abstract class StateMachine
    {
        public State CurentState;
        protected Dictionary<string, State> States = new Dictionary<string, State>();

        public void AddState(State state)
        {
            var stateName = typeof(State).Name;
            if (States.ContainsKey(stateName))
            {
                return;
            }
            States.Add(typeof(State).Name, state);
        }

        public abstract void EnterState(string stateName);
    }
}