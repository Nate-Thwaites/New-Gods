using UnityEngine;

namespace Enemy
{
    public class EnemyAttackStunState : EnemyState
    {
        public EnemyAttackStunState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            enemy.erb.linearVelocity = new Vector2(0, 0);
           
            enemy.stunned = true;
            enemy.attackPlayer = true;
            enemy.StartCoroutine(enemy.AttackStun());
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

            

            if (!enemy.stunned)
            {
                if (enemy.CheckForAttack())
                {
                    esm.ChangeState(enemy.enemyAttackState);
                }

                if (enemy.CheckForChase())
                {
                    esm.ChangeState(enemy.enemyChaseState);
                }

            }

            if(enemy.CheckForBlock())
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