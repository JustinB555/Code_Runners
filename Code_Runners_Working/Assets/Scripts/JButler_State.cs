//////////////////////////////////////////////////
// Credits
// Base Credits: Shawn Kendall, Justin Murphy;
//               FSGDN Game Development C202009 00
// Creator: Justin Butler
// Created: November 9, 2020
// Description:
// Responsible for all AI enemy behaviours and interactions with the player. This State Machine is setup 
// in such a way that it can be used for more things.
//////////////////////////////////////////////////

// VERBOSE or Debugger goes here
//#define CODERUNNER_STATEMACHINE_VERBOSE

namespace CodeRunner.StateMachine
{
    [System.Serializable]
    public abstract class JButler_State : StateInterface
    {
        public virtual void Execute() { }
        public virtual void PhysicsExecute() { }
        public virtual void PostExecute() { }

        public virtual void OnAnimatorIK(int layerIndex) { }

        public virtual void Initialize()
        {
#if (CODERUNNER_STATEMACHINE_VERBOSE)
            UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
#endif // CODERUNNER_STATEMACHINE_VERBOSE
        }

        public virtual void Enter()
        {
#if (CODERUNNER_STATEMACHINE_VERBOSE)
            UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
#endif // CODERUNNER_STATEMACHINE_VERBOSE
        }

        public virtual void Exit()
        {
#if (CODERUNNER_STATEMACHINE_VERBOSE)
            UnityEngine.Debug.Log(machine.name + "." + GetType().Name + "::" + System.Reflection.MethodBase.GetCurrentMethod().Name + "()");
#endif // CODERUNNER_STATEMACHINE_VERBOSE
        }

        public T GetMachine<T>() where T : MachineInterface
        {
            try
            {
                return (T)machine;
            }
            catch (System.InvalidCastException e)
            {
                if (typeof(T) == typeof(JButler_MachineState) || typeof(T).IsSubclassOf(typeof(JButler_MachineState)))
                {
                    throw new System.Exception(machine.name + ".GetMachine() cannot return the type you requested!\tYour machine is derived from JButler_MachineBehaviour not JButler_MachineState!" + e.Message);
                }
                else if (typeof(T) == typeof(JButler_MachineBehaviour) || typeof(T).IsSubclassOf(typeof(JButler_MachineBehaviour)))
                {
                    throw new System.Exception(machine.name + ".GetMachine() cannot return the type you requested!\tYour machine is derived from JButler_MachineState not JButler_MachineBehaviour!" + e.Message);
                }
                else
                {
                    throw new System.Exception(machine.name + ".GetMachine() cannot return the type you requested!\n" + e.Message);
                }
            }
        }

        internal MachineInterface machine { get; set; }

        public bool isActive { get { return machine.IsCurrentState(GetType()); } }
    }
}
