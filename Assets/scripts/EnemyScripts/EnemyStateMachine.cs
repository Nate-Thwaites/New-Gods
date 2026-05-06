using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        public EnemyState CurrentState { get; private set; }
        public EnemyState LastState { get; private set; }
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
