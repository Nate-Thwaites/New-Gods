using UnityEngine;
namespace Enemy
{
    public class EnemyPostureStunState : EnemyState
    {
        public EnemyPostureStunState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }
        bool attackLanded;
        public override void Enter()
        {
            base.Enter();
            

        }

        public override void Exit()
        {
            base.Exit();
            enemy.posture.posture = enemy.posture.minPosture;
            enemy.playerScript.attackDamage /= 4;
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

            if (enemy.playerScript.hitEnemy)
            {
                attackLanded = true;
                
                enemy.erb.AddForce(5 * enemy.transform.right * 2f, ForceMode2D.Impulse);

                enemy.playerScript.attackDamage *= 4;

                enemy.postureBreakStunEnemy = false;
            }
            

            if (!enemy.postureBreakStunEnemy)
            {

                enemy.StartCoroutine(enemy.LeavePostureStun());

                if (enemy.leavePostureStunEnemy)
                {

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
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}
