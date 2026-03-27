using UnityEngine;

namespace Enemy
{
    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            enemy.erb.linearVelocity = new Vector2(0, 0);
            Debug.Log("enemy idle");
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
            
            
            if (enemy.CheckForChase())
            {
                esm.ChangeState(enemy.enemyChaseState);
            }

            if (enemy.CheckForAttack())
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