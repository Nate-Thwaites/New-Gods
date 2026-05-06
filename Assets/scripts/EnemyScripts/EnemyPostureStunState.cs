using UnityEngine;
namespace Enemy
{
    public class EnemyPostureStunState : EnemyState
    {
        public EnemyPostureStunState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            enemy.postureBreakStunEnemy = true;
            enemy.playerScript.attackDamage *= 4;

        }

        public override void Exit()
        {
            base.Exit();
            enemy.enemyPosture = enemy.minEnemyPosture;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            enemy.erb.linearVelocity = new Vector2(0, 0);

            enemy.StartCoroutine(enemy.PostureBreakStun());

            if (!enemy.postureBreakStunEnemy)
            {
                enemy.playerScript.attackDamage /= 4;

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

                if (enemy.CheckForParry())
                {
                    esm.ChangeState(enemy.enemyParryState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}
