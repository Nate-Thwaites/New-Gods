using Player;
using UnityEngine;



namespace Enemy
{
    public enum EnemyAttackType
    {
        SwipeLeft,
        SwipeRight,
        SwipeUp,
        
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
            EnemyAttackSwitch();

            enemy.blockOrParryChance = 0;
            enemy.enemyMoveDir = 0;

            

            enemy.seePlayer = false;
        }

        void EnemyAttack()
        {
            enemy.enemyAttackTimer = 1.5f;
            enemy = enemy.GetComponent<EnemyScript>();

            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemy.enemyAttackPoint.position, enemy.enemyAttackRange, enemy.playerLayer);
            foreach (Collider2D player in hitPlayer)
            {
                
                enemy.hitPlayer = true;
                
                if(enemy.hitPlayer)
                {
                    enemy.playerScript.health.health -= enemy.enemyDamage; 
                }
                
            }

                     
            
        }

        public void EnemyAttackSwitch()
        {
            switch ((int)enemyAttackNum)
            {
                case 0:

                    EnemyAttack();

                    //enemy.anim.Play("enemy attack 1", 0);
                    enemy.enemyAttackCompleteTimer = 0.5f;
                    enemy.enemyDamage = 10;

                    break;

                case 1: //Swipe Left

                    
                    EnemyAttack();
                    
                    //enemy.anim.Play("enemy attack 1", 0);
                    enemy.enemyAttackCompleteTimer = 0.5f;
                    enemy.enemyDamage = 10;

                    break;

                case 2: //Swipe Right

                    EnemyAttack();
                    
                    //enemy.anim.Play("enemy attack 2", 0);
                    enemy.enemyAttackCompleteTimer = 0.5f;
                    enemy.enemyDamage = 10;

                    break;

                case 3: // Swipe Up

                    EnemyAttack();
                   
                    //enemy.anim.Play("enemy attack 3", 0);
                    enemy.enemyAttackCompleteTimer = 0.5f;
                    enemy.enemyDamage = 20;
                    break;

                


            }
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

            if(enemy.CheckForParry())
            {
                esm.ChangeState(enemy.enemyParryState);
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