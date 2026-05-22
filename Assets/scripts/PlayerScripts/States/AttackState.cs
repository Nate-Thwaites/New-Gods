using Enemy;
using System;
using UnityEngine;


namespace Player
{

    public enum AttackType
    {
        SwipeLeft,
        SwipeRight,
        SwipeUp,
        SwipeDown
    }

    public class AttackState : State
    {


        public AttackType attackNum;

        // constructor
        public AttackState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.rb.linearVelocity = new Vector2(0, 0);
            AttackSwitch();
           
            
        }
        
        void Attack()
        {
            player.attackTimer = 1.5f;
            player.audioSource.PlayOneShot(player.am.SFXClips[0]);


            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(player.attackPoint.position, player.attackRange, player.enemyLayer);
            foreach (Collider2D enemy in hitEnemy)
            {
                player.RandomNumForEnemyBlock(enemy.GetComponent<EnemyScript>());

                player.hitEnemy = true;
                enemy.GetComponent<EnemyScript>().attackStunEnemy = true;

                if (player.hitEnemy && enemy.GetComponent<EnemyScript>().postureBreakStunEnemy)
                {
                    enemy.GetComponent<EnemyScript>().postureBreakStunEnemy = false;
                }

                if (player.hitEnemy)
                {
                    
                    enemy.GetComponent<EnemyScript>().StartCoroutine(enemy.GetComponent<EnemyScript>().AttackDelay());
                    

                }
                

            }


        }

        public void AttackSwitch()
        {
            switch ((int)attackNum)
            {


                case 0: //Swipe Left

                    Attack();

                    player.anim.Play("attack temp", 0);
                    player.attackDamage = 10;

                    if (player.hasPowerup)
                    {
                        player.attackDamage *= 1.5f;
                    }

                    player.posture.postureDamage = 20;
                    player.attackCompleteTimer = 0.4f;
                    player.enemy.attackStunEnemy = true;
                    player.enemy.attackStunTimer = player.attackCompleteTimer + 0.1f;
                    //Debug.Log("Damage = " + player.attackDamage );

                    break;

                case 1: //Swipe Right

                    Attack();
                   
                    player.anim.Play("attack temp no2", 0);
                    player.attackDamage = 5;
                    if (player.hasPowerup)
                    {
                        player.attackDamage *= 1.5f;
                    }
                    player.posture.postureDamage = 10;
                    player.attackCompleteTimer = 0.6f;
                    player.enemy.attackStunEnemy = true;
                    player.enemy.attackStunTimer = player.attackCompleteTimer + 0.1f;
                    //Debug.Log("Damage = " + player.attackDamage);

                    break;

                case 2: // Swipe Up

                    Attack();
                   
                    player.anim.Play("Attack temp no3", 0);
                    player.attackDamage = 10;
                    if (player.hasPowerup)
                    {
                        player.attackDamage *= 1.5f;
                    }
                    player.posture.postureDamage = 20;
                    player.attackCompleteTimer = 0.6f;
                    player.enemy.attackStunEnemy = true;
                    player.enemy.attackStunTimer = player.attackCompleteTimer + 0.1f;
                    //Debug.Log("Damage = " + player.attackDamage);

                    break;

                case 3: // Swipe Down

                    Attack();
                    
                    player.anim.Play("attack temp no4", 0);
                    player.attackDamage = 20;
                    if (player.hasPowerup)
                    {
                        player.attackDamage *= 1.5f;
                    }
                    player.posture.postureDamage = 40;
                    player.attackCompleteTimer = 0.9f;
                    player.enemy.attackStunEnemy = true;
                    player.enemy.attackStunTimer = player.attackCompleteTimer + 0.1f;
                    //Debug.Log("Damage = " + player.attackDamage);

                    break;

                
            }

            
        }

     

        public override void Exit()
        {
            player.hitEnemy = false;
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            


            if (player.attackCompleteTimer <= 0)
            {
                if (player.CheckForIdle())
                {
                    sm.ChangeState(player.idleState);
                }

                if (player.CheckForRun())
                {
                    sm.ChangeState(player.walkingState);
                }

                if (player.CheckForJump())
                {
                    sm.ChangeState(player.jumpingState);
                }

                if (player.CheckForFall())
                {
                    sm.ChangeState(player.fallingState);
                }

            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}