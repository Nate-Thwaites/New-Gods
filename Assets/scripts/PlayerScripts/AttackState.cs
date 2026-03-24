using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Timeline.Actions;
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


            Attack();
            AttackSwitch();
            
            
            
        }
        
        void Attack()
        {
            player.attackTimer = 1.5f;

            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(player.attackPoint.position, player.attackRange, player.enemyLayer);
            foreach (Collider2D enemy in hitEnemy)
            {
                Debug.Log("hit enemy");
            }
        }

        public void AttackSwitch()
        {
            switch ((int)attackNum)
            {


                case 0:

                    Attack();

                    player.anim.Play("attack temp", 0);



                    break;

                case 1:

                    Attack();

                    player.anim.Play("attack temp no2", 0);


                    break;

                case 2:
                    Attack();

                    player.anim.Play("Attack temp no3", 0);

                    break;

                case 3:
                    Attack();

                    player.anim.Play("attack temp no4", 0);

                    break;
            }

            
        }

     

        public override void Exit()
        {
            
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            /*if attack and timer >= 0

                THEN

                go to next case
                


                if timer < 0

                THEN

                case = 0
                
                
            */

            

            



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


/*        public void Test()
        {
            if( attackNum == AttackType.SwipeUp )
            {
                goto Attack0;
            }
            //if (attackNum == 1)
            {
                goto Attack1;
            }




        Attack0:
            //do stuff

            goto Attack2;

            return;


        Attack1:


        Attack2:
            //goto Attack0;
            return;


        }*/



    }
}

