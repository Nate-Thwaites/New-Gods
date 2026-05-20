using UnityEngine;

namespace Enemy
{
    public abstract class EnemyState
    {
        protected EnemyScript enemy;
        protected EnemyStateMachine esm;
        protected HealthScript health;
        protected PostureScript posture;

        // base constructor
        protected EnemyState(EnemyScript enemy, EnemyStateMachine esm)
        {
            this.enemy = enemy;
            this.posture = enemy.posture;
            this.health = enemy.health;
            this.esm = esm;
        }

        // These methods are common to all states
        public virtual void Enter()
        {
            //Debug.Log("This is base.enter");
        }

        public virtual void HandleInput()
        {
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }

        public virtual void Exit()
        {
        }

    }
}
