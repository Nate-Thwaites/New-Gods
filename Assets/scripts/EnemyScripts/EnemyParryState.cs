using UnityEngine;

namespace Enemy
{
    public class EnemyParryState : EnemyState
    {
        public EnemyParryState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            enemy.parryEnemy = true;

        }

        public override void Exit()
        {
            base.Exit();
            enemy.parryEnemy = false;

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

