using Player;
using UnityEngine;



namespace Enemy
{
    


    public class EnemyAttackState : EnemyState
    {
       

        public EnemyAttackState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            enemy.anim.Play("enemy attack 1", 0);
            enemy.enemyAttackCompleteTimer = 0.8f;
            enemy.enemyDamage = 10;

            enemy.blockOrParryChance = 0;
            enemy.erb.linearVelocity = Vector2.zero;


            enemy.seePlayer = false;

            enemy.StartCoroutine(enemy.LeaveAttack());
        }

      

       

        public override void Exit()
        {
            base.Exit();
            enemy.hitPlayer = false;
            
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (enemy.leaveAttack)
            {
                if (enemy.CheckForAttackStun())
                {
                    esm.ChangeState(enemy.enemyAttackStunState);
                }

                if (enemy.CheckForIdle() || enemy.enemyAttackCompleteTimer >= 0 && enemy.enemyAttackCompleteTimer <= 0.6)
                {
                    enemy.anim.Play("enemy leave attack", 0);

                    esm.ChangeState(enemy.enemyIdleState);
                }


                if (enemy.CheckForChase())
                {
                    enemy.anim.Play("enemy leave attack", 0);
                    esm.ChangeState(enemy.enemyChaseState);
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

                if (enemy.CheckForPostureStun())
                {
                    esm.ChangeState(enemy.enemyPostureStunState);
                }

                if(enemy.CheckForAttackStun())
                {
                    esm.ChangeState(enemy.enemyAttackStunState);
                }

                enemy.leaveAttack = false;
            } 
           
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}