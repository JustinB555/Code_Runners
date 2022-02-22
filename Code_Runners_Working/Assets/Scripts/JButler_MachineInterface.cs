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

namespace CodeRunner.StateMachine
{
    public interface MachineInterface
    {
        void SetInitialState<T>() where T : JButler_State;
        void SetInitialState(System.Type T);

        void ChangeState<T>() where T : JButler_State;
        void ChangeState(System.Type T);

        void StopState();

        bool IsCurrentState<T>() where T : JButler_State;
        bool IsCurrentState(System.Type T);

        void AddState<T>() where T : JButler_State, new();
        void AddState(System.Type T);

        void RemoveState<T>() where T : JButler_State;
        void RemoveState(System.Type T);

        bool ContainsState<T>() where T : JButler_State;
        bool ContainsState(System.Type T);

        void RemoveAllStates();
        string name { get; set; }
    }
}
