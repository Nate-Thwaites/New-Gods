using Enemy;
using System.Security.Cryptography;
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
            //Attack();
            AttackSwitch();
           
            
        }
        
        void Attack()
        {
            player.attackTimer = 1.5f;
            

            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(player.attackPoint.position, player.attackRange, player.enemyLayer);
            foreach (Collider2D enemy in hitEnemy)
            {
                player.RandomNumForEnemyBlock();

                player.hitEnemy = true;

                if(player.hitEnemy && player.enemyScript.postureBreakStunEnemy)
                {
                    player.enemyScript.postureBreakStunEnemy = false;
                }

                if (player.hitEnemy)
                {
                    player.StartCoroutine(player.AttackDelay());
                    //player.enemyScript.enemyHealth -= 10;
                }
                /*if (!player.enemyScript.blockEnemy)
                {
                    player.enemyScript.enemyHealth -= 10;
                }*/

                Debug.Log("enemy health: " + player.enemyScript.enemyHealth);
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
                    player.postureDamage = 20;
                    player.attackCompleteTimer = 0.5f;


                    break;

                case 1: //Swipe Right

                    Attack();

                    player.anim.Play("attack temp no2", 0);
                    player.attackDamage = 5;
                    player.postureDamage = 10;
                    player.attackCompleteTimer = 0.7f;

                    break;

                case 2: // Swipe Up

                    Attack();

                    player.anim.Play("Attack temp no3", 0);
                    player.attackDamage = 10;
                    player.postureDamage = 20;
                    player.attackCompleteTimer = 0.7f;

                    break;

                case 3: // Swipe Down

                    Attack();

                    player.anim.Play("attack temp no4", 0);
                    player.attackDamage = 20;
                    player.postureDamage = 40;
                    player.attackCompleteTimer = 1f;

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