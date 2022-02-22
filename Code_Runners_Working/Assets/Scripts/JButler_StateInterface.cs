//////////////////////////////////////////////////
// Credits
// Base Credits: Shawn Kendall, Justin Murphy;
//               FSGDN Game Development C202009 00
// Creator: Justin Butler
// Created: November 9, 2020
// Description:
// Responsible for the State's Interface. Each part represents a different order of calling something.
//////////////////////////////////////////////////

namespace CodeRunner.StateMachine
{
    public interface StateInterface
    {
        void Initialize();
        void Enter();
        void Execute();
        void PhysicsExecute();
        void PostExecute();
        void Exit();
        void OnAnimatorIK(int layerIndex);
        bool isActive { get; }
        T GetMachine<T>() where T : MachineInterface;
    }
}