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

            if (enemy.enemyPosture > enemy.minEnemyPosture)
            {
                enemy.enemyPosture = enemy.enemyPosture - 3 * Time.deltaTime;
            }

            if (enemy.CheckForChase())
            {
                esm.ChangeState(enemy.enemyChaseState);
            }

            if (enemy.CheckForAttack())
            {
                esm.ChangeState(enemy.enemyAttackState);
            }

            if (enemy.CheckForParryStun())
            {
                esm.ChangeState(enemy.enemyParryStunState);
            }

            if (enemy.CheckForBlock())
            {
                esm.ChangeState(enemy.enemyBlockState);
            }

            if(enemy.CheckForParry())
            {
                esm.ChangeState(enemy.enemyParryState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}