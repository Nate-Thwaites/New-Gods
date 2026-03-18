using UnityEngine;


namespace Player
{

    public enum AttackType
    {
        SwipeLeft,
        SwipeUp
    }

    public class AttackState : State
    {



        public AttackType attackNum;// = AttackType.SwipeUp;

        // constructor
        public AttackState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

        
            
            
            /*
            switch(player.attackNum)
            {
                case 1:

                    Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(player.attackPoint.position, player.attackRange, player.enemyLayer);


                    foreach (Collider2D enemy in hitEnemy)
                    {
                        Debug.Log("hit enemy");
                    }

                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4: 
                    break;
            }
            */
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

            if(player.CheckForIdle())
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

