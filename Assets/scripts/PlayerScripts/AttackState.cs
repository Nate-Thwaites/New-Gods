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
            player.attackCompleteTimer = 1f;

            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(player.attackPoint.position, player.attackRange, player.enemyLayer);
            foreach (Collider2D enemy in hitEnemy)
            {
                player.RandomNumForEnemyBlock();

                player.hitEnemy = true;

              

                if (!player.enemyScript.blockEnemy && !player.enemyScript.parryEnemy)
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



                    break;

                case 1: //Swipe Right

                    Attack();

                    player.anim.Play("attack temp no2", 0);


                    break;

                case 2: // Swipe Up

                    Attack();

                    player.anim.Play("Attack temp no3", 0);

                    break;

                case 3: // Swipe Down

                    Attack();

                    player.anim.Play("attack temp no4", 0);

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

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}