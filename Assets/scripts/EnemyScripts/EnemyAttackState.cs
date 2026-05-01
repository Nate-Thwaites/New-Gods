using Player;
using UnityEngine;



namespace Enemy
{
    public enum EnemyAttackType
    {
        SwipeLeft,
        SwipeRight,
        SwipeUp,
        SwipeDown
    }


    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackType enemyAttackNum;

        public EnemyAttackState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            //EnemyAttack();
            EnemyAttackSwitch();
            
            enemy.enemyMoveDir = 0;

            if (enemy.playerScript.hitPlayer && !enemy.playerScript.isBlocking)
            {
                enemy.playerScript.playerHealth -= 10;
            }

            enemy.seePlayer = false;
        }

        void EnemyAttack()
        {
            enemy.enemyAttackTimer = 1.5f;
            enemy.enemyAttackCompleteTimer = 1f;
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemy.enemyAttackPoint.position, enemy.enemyAttackRange, enemy.playerLayer);
            foreach (Collider2D player in hitPlayer)
            {
                enemy.playerScript.hitPlayer = true;

                
                
                
                
            }

                     
            
        }

        public void EnemyAttackSwitch()
        {
            switch ((int)enemyAttackNum)
            {


                case 0: //Swipe Left

                    EnemyAttack();
                    
                    enemy.anim.Play("enemy attack 1", 0);



                    break;

                case 1: //Swipe Right

                    EnemyAttack();
                    
                    enemy.anim.Play("enemy attack 2", 0);


                    break;

                case 2: // Swipe Up

                    EnemyAttack();
                   
                    enemy.anim.Play("enemy attack 3", 0);

                    break;

                case 3: // Swipe Down

                    EnemyAttack();
                    
                    enemy.anim.Play("enemy attack 4", 0);

                    break;


            }
        }

        public override void Exit()
        {
            base.Exit();
            enemy.playerScript.hitPlayer = false;

        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

          

            if (enemy.CheckForIdle() || enemy.enemyAttackCompleteTimer >= 0 && enemy.enemyAttackCompleteTimer <= 0.6)
            {
                esm.ChangeState(enemy.enemyIdleState);
            }


            if (enemy.CheckForChase())
            {
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