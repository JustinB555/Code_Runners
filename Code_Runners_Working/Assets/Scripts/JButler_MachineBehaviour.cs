﻿//////////////////////////////////////////////////
// Credits
// Base Credits: Shawn Kendall, Justin Murphy;
//               FSGDN Game Development C202009 00
// Creator: Justin Butler
// Created: November 9, 2020
// Description:
// Responsible for all AI enemy behaviours and interactions with the player. This State Machine is setup 
// in such a way that it can be used for more things.
//////////////////////////////////////////////////

//#define CODERUNNER_MBEHAVIOUR_VERBOSE

namespace CodeRunner.StateMachine
{
    public abstract class JButler_MachineBehaviour : UnityEngine.MonoBehaviour, MachineInterface
    {
        /// <summary>
        /// REQUIRES IMPL
        /// </summary>
        public abstract void AddStates();

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            AddStates();

            currentState = initialState;
            if (null == currentState)
            {
                throw new System.Exception("\n" + name + ".nextState is null on Initialize()!\tDid you forget to call SetInitialState()?\n");
            }

            foreach (System.Collections.Generic.KeyValuePair<System.Type, JButler_State> pair in states)
            {
                pair.Value.Initialize();
            }

            onEnter = true;
            onExit = false;
        }

        public virtual void Update()
        {
            if (onExit)
            {
                currentState.Exit();
#if (CODERUNNER_MBEHAVIOUR_VERBOSE)
                UnityEngine.Debug.Log("<color=yellow>CurrentState.Exit()</color> = " + currentState);
#endif //CODERUNNER_MBEHAVIOUR_VERBOSE

                currentState = nextState;
#if (CODERUNNER_MBEHAVIOUR_VERBOSE)
                UnityEngine.Debug.Log("<color=orange>(CurrentState = nextState)</color> =" + currentState);
#endif //CODERUNNER_MBEHAVIOUR_VERBOSE
                nextState = null;

                onEnter = true;
                onExit = false;
            }

            if (onEnter)
            {
                currentState.Enter();
#if (CODERUNNER_MBEHAVIOUR_VERBOSE)
                UnityEngine.Debug.Log("<color=blue>CurrentState.Enter()</color> = " + currentState);
#endif //CODERUNNER_MBEHAVIOUR_VERBOSE

                onEnter = false;
            }

            try
            {
                currentState.Execute();
            }
            catch (System.NullReferenceException e)
            {
                if (null == initialState)
                {
                    throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
                }
                else
                {
                    throw new System.Exception("\n" + name + ".currentState is null calling Exectue()!\tDid you change state to a valid state?\n" + e.Message);
                }
            }
        }

        public virtual void FixedUpdate()
        {
            if (!(onEnter && onExit))
            {
                try
                {
                    currentState.PhysicsExecute();
                }
                catch (System.NullReferenceException e)
                {
                    if (null == initialState)
                    {
                        throw new System.Exception("\n" + name + ".currentState is null calling Execute()!\tDid you set initail state?\n" + e.Message);
                    }
                    else
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Exevute()!\tDid you change state to a valid state?\n" + e.Message);
                    }
                }
            }
        }

        public virtual void LateUpdate()
        {
            if (!(onEnter && onExit))
            {
                try
                {
                    currentState.PostExecute();
                }
                catch (System.NullReferenceException e)
                {
                    if (null == initialState)
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
                    }
                    else
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Execute()tDidyou change state to a valid state?\n" + e.Message);
                    }
                }
            }
        }

        public void OnAnimatorIK(int layerIndex)
        {
            if (!(onEnter && onExit))
            {
                try
                {
                    currentState.OnAnimatorIK(layerIndex);
                }
                catch (System.NullReferenceException e)
                {
                    if (null == initialState)
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you set initial state?\n" + e.Message);
                    }
                    else
                    {
                        throw new System.Exception("\n" + name + ".currentState is null when calling Execute()!\tDid you change state to a valid state?\n" + e.Message);
                    }
                }
            }
        }

        public void SetInitialState<T>() where T : JButler_State { initialState = states[typeof(T)]; }
        public void SetInitialState(System.Type T) { initialState = states[T]; }

        public void StopState() { nextState = null; }

        public void ChangeState<T>() where T : JButler_State { ChangeState(typeof(T)); }
        public void ChangeState(System.Type T)
        {
            if (null != nextState)
            {
                throw new System.Exception(name + " is already changing states, you must wait to call ChangeState()!\n");
            }

            try
            {
                nextState = states[T];
#if (CODERUNNER_MBEHAVIOUR_VERBOSE)
                UnityEngine.Debug.Log("<color=green>NextState</color> = " + nextState);
#endif //CODERUNNER_MBEHAVIOUR_VERBOSE
            }
            catch (System.Collections.Generic.KeyNotFoundException e)
            {
                throw new System.Exception("\n" + name + ".ChangeState() cannot find the state in the machine!\tDid you add the state you are trying to change to?\n" + e.Message);
            }

            onExit = true;
        }

        public bool IsCurrentState<T>() where T : JButler_State { return (currentState.GetType() == typeof(T)) ? true : false; }
        public bool IsCurrentState(System.Type T) { return (currentState.GetType() == T) ? true : false; }

        public void AddState<T>() where T : JButler_State, new()
        {
            if (!ContainsState<T>())
            {
                JButler_State item = new T();
                item.machine = this;

                states.Add(typeof(T), item);
            }
        }

        public void AddState(System.Type T)
        {
            if (!ContainsState(T))
            {
                JButler_State item = (JButler_State)System.Activator.CreateInstance(T);
                item.machine = this;

                states.Add(T, item);
            }
        }

        public void RemoveState<T>() where T : JButler_State { states.Remove(typeof(T)); }
        public void RemoveState(System.Type T) { states.Remove(T); }

        public bool ContainsState<T>() where T : JButler_State { return states.ContainsKey(typeof(T)); }
        public bool ContainsState(System.Type T) { return states.ContainsKey(T); }

        public void RemoveAllStates() { states.Clear(); }

        protected JButler_State currentState { get; set; }
        protected JButler_State nextState { get; set; }
        protected JButler_State initialState { get; set; }

        protected bool onEnter { get; set; }
        protected bool onExit { get; set; }

        protected System.Collections.Generic.Dictionary<System.Type, JButler_State> states = new System.Collections.Generic.Dictionary<System.Type, JButler_State>();
    }
}
