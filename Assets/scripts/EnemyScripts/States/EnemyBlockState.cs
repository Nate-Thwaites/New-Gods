using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyBlockState : EnemyState
    {
        public EnemyBlockState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }
        public override void Enter()
        {
            PlayerScript playerScript = enemy.player.GetComponent<PlayerScript>();
            base.Enter();
            //Debug.Log("block");
            enemy.blockEnemy = true;
            enemy.posture.posture += playerScript.posture.postureDamage;
            Debug.Log("posture damage: " + playerScript.posture.postureDamage);
            enemy.StartCoroutine(enemy.LeaveEnemyBlock());

        }

        public override void Exit()
        {
            base.Exit();
            enemy.blockEnemy = false;
            //Debug.Log("leave block");
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();




            if (!enemy.blockEnemy)
            {
                if (enemy.CheckForChase())
                {
                    esm.ChangeState(enemy.enemyChaseState);
                }

                if (enemy.CheckForAttack())
                {
                    esm.ChangeState(enemy.enemyAttackState);
                }

                if(enemy.CheckForPostureStun())
                {
                    esm.ChangeState(enemy.enemyPostureStunState);
                }   
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}