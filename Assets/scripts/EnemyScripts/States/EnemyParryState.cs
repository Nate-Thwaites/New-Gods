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
            //Debug.Log("parry");
            enemy.parryEnemy = true;
            enemy.posture.posture += 10;
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

           
            enemy.StartCoroutine(enemy.LeaveEnemyParry());


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
            
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}

