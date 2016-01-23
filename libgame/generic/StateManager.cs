using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTool.Libgame
{
    public class StateMachine
    {
        //<State Machine Name, Current State Name>
        static Dictionary<string, string> currentStateNames = new Dictionary<string, string>();
        //<State Machine Name||Current State Name, Class State>
        static Dictionary<string, State> states = new Dictionary<string, State>();

        public static bool IsCurrentState(string stateMachineName, string stateName)
        {
            if (currentStateNames.ContainsKey(stateMachineName) && currentStateNames[stateMachineName] == stateName)
            {
                return true;
            }
            return false;
        }

        static public void Clear()
        {
            //<State Machine Name, Current State Name>
            currentStateNames = new Dictionary<string, string>();
            //<State Machine Name||Current State Name, Class State>
            states = new Dictionary<string, State>();
        }

        public static string GetCurrentStateName(string stateMachineName)
        {
            if (currentStateNames.ContainsKey(stateMachineName))
            {
                return currentStateNames[stateMachineName];
            }
            else
            {
                return null;
            }
        }

        public static void Init()
        {
            if (Runtime.StateManager.Instance() == null)
            {
                GameObject obj = new GameObject("StateManager");
                obj.AddComponent< Runtime.StateManager> ();
            }
        }
        static public void RegediStateCallBack(string stateMachineName, string stateName, StateCallBackType type, StateHandle handle)
        {
            Init();
            if (!states.ContainsKey(stateMachineName + stateName))
            {
                states.Add(stateMachineName + stateName, new State(stateName));
            }
            if(type == StateCallBackType.TypeOnEnter)
            {
                states[stateMachineName + stateName].onEnter += handle;
            }
            else if (type == StateCallBackType.TypeOnExcute)
            {
                states[stateMachineName + stateName].onExcute += handle;

            }
            else if (type == StateCallBackType.TypeOnExit)
            {
                states[stateMachineName + stateName].onExit += handle;
            }
        }

        static public void UnregediStateCallBack(string stateMachineName, string stateName, StateCallBackType type, StateHandle handle)
        {
            if (!states.ContainsKey(stateMachineName + stateName))
            {
                return;
            }
            if (type == StateCallBackType.TypeOnEnter)
            {
                states[stateMachineName + stateName].onEnter += handle;
            }
            else if (type == StateCallBackType.TypeOnExcute)
            {
                states[stateMachineName + stateName].onExcute += handle;

            }
            else if (type == StateCallBackType.TypeOnExit)
            {
                states[stateMachineName + stateName].onExit += handle;
            }
        }

        static public void ChangeState(string stateMachineName, string newStateName)
        {
            Init();
            _ChangeState(stateMachineName, newStateName, null, null, null);
        }
        static void _ChangeState(string stateMachineName, string newStateName, StateHandle onEnter = null, StateHandle onExcute = null, StateHandle onExit = null)
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
                currentStateNames[stateMachineName] = newStateName;
            }
            else
            {
                currentStateNames.Add(stateMachineName, newStateName);
            }
            if (!states.ContainsKey(stateMachineName + newStateName))
            {
                states.Add(stateMachineName + newStateName, new State(newStateName));
            }
            if (states[stateMachineName + newStateName] != null)
            {
                states[stateMachineName + newStateName].OnEnter();
            }
        }

        static public void Update()
        {
            Init();
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

    public enum StateCallBackType
    {
        TypeOnEnter,
        TypeOnExcute,
        TypeOnExit,
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
        static StateManager _stateManager;
        void Awake()
        {
            _stateManager = this;
        }

        void OnDestroy()
        {
            Libgame.StateMachine.Clear();
        }
        static public StateManager Instance()
        {
            if (_stateManager && _stateManager.enabled)
            {
                return _stateManager;
            }
            else
            {
                return null;
            }
        }

        
        void Update()
        {
            if (Instance() == this)
            {
                Libgame.StateMachine.Update();
            }
        }
    }
}