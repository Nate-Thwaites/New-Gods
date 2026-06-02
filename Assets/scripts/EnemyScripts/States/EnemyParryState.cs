using Player;
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
            PlayerScript playerScript = enemy.player.GetComponent<PlayerScript>();
            base.Enter();
            //Debug.Log("parry");
            enemy.parryEnemy = true;
            playerScript.posture.posture += 10;
            enemy.StartCoroutine(enemy.LeaveEnemyParry());
            playerScript.parryStunned = true;
        }

        public override void Exit()
        {
            base.Exit();
            enemy.parryEnemy = false;
            //Debug.Log("leave parry");
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();


            


            if (!enemy.parryEnemy)
            {
                if (enemy.CheckForChase())
                {
                    esm.ChangeState(enemy.enemyChaseState);
                }

                if (enemy.CheckForAttack())
                {
                    esm.ChangeState(enemy.enemyAttackState);
                }

               



            }
            if (enemy.CheckForPostureStun())
            {
                esm.ChangeState(enemy.enemyPostureStunState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}

