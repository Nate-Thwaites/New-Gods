using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        public EnemyState CurrentState { get; private set; }
        public EnemyState LastState { get; private set; }
        //private int lastChangeFrame = -1;
        public void Init(EnemyState startingState)
        {
            CurrentState = startingState;
            LastState = null;
            startingState.Enter();
        }

        public void ChangeState(EnemyState newState)
        {
            //Debug.Log("Changing state to " + newState);
            CurrentState.Exit();

            LastState = CurrentState;
            CurrentState = newState;
            newState.Enter();

           /* if (lastChangeFrame == Time.frameCount)
            {
                return;
            }

            lastChangeFrame = Time.frameCount;

            if (CurrentState != null)
            {
                CurrentState.Exit();
            }*/

        }

        public void PhysicsUpdate()
        {

        }

        public void LogicUpdate()
        {

        }

        public EnemyState GetState()
        {
            return CurrentState;
        }

    }
}
