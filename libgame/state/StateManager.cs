using System;
using System.Collections.Generic;

namespace UnityTool.Libgame
{
    public class StateMachine
    {
        //<State Machine Name, Current State Name>
        static Dictionary<string, string> currentStateNames = new Dictionary<string, string>();
        //<State Machine Name||Current State Name, Class State>
        static Dictionary<string, State> states = new Dictionary<string, State>();

        static public void ChangeState(string stateMachineName, State newState)
        {
            ChangeState(stateMachineName, newState.stateName, newState.onEnter, newState.onExcute, newState.onExit);
        }
        static public void ChangeState(string stateMachineName, string newStateName)
        {
            ChangeState(stateMachineName, newStateName, null, null, null);
        }
        static void ChangeState(string stateMachineName, string newStateName, StateHandle onEnter = null, StateHandle onExcute = null, StateHandle onExit = null)
        {
            if (currentStateNames.ContainsKey(stateMachineName))
            {
                if (!states.ContainsKey(stateMachineName + currentStateNames[stateMachineName]))
                {
                    states.Add(stateMachineName + currentStateNames[stateMachineName], new State(currentStateNames[stateMachineName]));
                }
                if (states[stateMachineName + currentStateNames[stateMachineName]] != null)
                {
                    states[stateMachineName + currentStateNames[stateMachineName]].OnExit();
                }
                if (!states.ContainsKey(stateMachineName + newStateName))
                {
                    states.Add(stateMachineName + newStateName, new State(newStateName, onEnter, onExcute, onExit));
                }
                if (states[stateMachineName + newStateName] != null)
                {
                    states[stateMachineName + newStateName].OnEnter();
                }
                currentStateNames[stateMachineName] = newStateName;
            }
            else
            {
                currentStateNames.Add(stateMachineName, newStateName);
                states.Add(stateMachineName + newStateName, new State(newStateName, onEnter, onExcute, onExit));
                states[stateMachineName + newStateName].OnEnter();
            }
        }

        static public void Update()
        {
            Dictionary<string, string> currentStateNames1 = new Dictionary<string, string>(currentStateNames);
            foreach (KeyValuePair<string, string> stateName in currentStateNames1)
            {
                if (states.ContainsKey(stateName.Key + stateName.Value))
                {
                    if (states[stateName.Key + stateName.Value] != null)
                    {
                        states[stateName.Key + stateName.Value].OnExcute();
                    }
                }
            }
        }
    }

    public delegate void StateHandle(String stateName);

    public class State
    {
        public string stateName;
        public StateHandle onEnter;
        public StateHandle onExcute;
        public StateHandle onExit;

        public State(string stateName, StateHandle onEnter = null, StateHandle onExcute = null, StateHandle onExit = null)
        {
            this.stateName = stateName;
            this.onEnter = onEnter;
            this.onExcute = onExcute;
            this.onExit = onExit;
        }

        public void OnEnter()
        {
            if (onEnter != null)
            {
                onEnter(stateName);
            }
        }

        public void OnExcute()
        {
            if (onExcute != null)
            {
                onExcute(stateName);
            }
        }

        public void OnExit()
        {
            if (onExit != null)
            {
                onExit(stateName);
            }
        }
    }
}

namespace UnityTool.Libgame.Runtime
{
    public class StateManager : UnityEngine.MonoBehaviour
    {
        void Update()
        {
            Libgame.StateMachine.Update();
        }
    }
}