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
            Debug.Log("Enemy Attack Stunned");
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

            enemy.StartCoroutine(enemy.AttackStun());

            if (!enemy.attackStunEnemy)
            {
                //Debug.Log("Check For State");
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