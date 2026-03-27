using UnityEngine;

namespace Enemy
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("enemy attack");

            enemy.seePlayer = false;
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


            if (enemy.CheckForChase())
            {
                esm.ChangeState(enemy.enemyChaseState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}