using UnityEngine;

namespace Player
{
    public class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }
        public State LastState { get; private set; }
        private int lastChangeFrame = -1;
        public void Init(State startingState)
        {
            CurrentState = startingState;
            LastState = null;
            startingState.Enter();
        }

        public void ChangeState(State newState)
        {
            //Debug.Log("Changing state to " + newState);
            CurrentState.Exit();

            LastState = CurrentState;
            CurrentState = newState;
            newState.Enter();

            if(lastChangeFrame == Time.frameCount)
            {
                return;
            }

            lastChangeFrame = Time.frameCount;

            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
        }

        public void PhysicsUpdate()
        {

        }

        public void LogicUpdate()
        {

        }

        public State GetState()
        {
            return CurrentState;
        }

    }
}
