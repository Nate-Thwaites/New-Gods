using UnityEngine;

namespace Enemy
{
    public class EnemyChaseState : EnemyState
    {
        public EnemyChaseState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("chase");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(enemy.CheckForIdle())
            {
                esm.ChangeState(enemy.enemyIdleState);
            }

            if(enemy.CheckForAttack())
            {
                esm.ChangeState(enemy.enemyAttackState);
            }


        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}
