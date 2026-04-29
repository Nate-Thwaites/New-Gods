using UnityEngine;


namespace Player
{
    public class BlockingState : State
    {
       
        // constructor
        public BlockingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {  
        }

        public override void Enter()
        {
            base.Enter();


            

        }

        public override void Exit()
        {
            base.Exit();
            
            player.isBlocking = false;
            player.parryTimer = 0.18f;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            if (player.parryTimer > 0 && player.hitPlayer)
            {
                Debug.Log("Parried attack");
                player.playerParry = true;
                player.enemyScript.parryStunEnemy = true;
                if (player.playerParry)
                {
                    player.playerPostureBar = player.playerPostureBar + 0;
                }
            }

            if (player.hitPlayer && player.isBlocking)
            {
                Debug.Log("Blocked attack");
                //player.blockingAndParrying.hitPlayer = false;
                //player.blockingAndParrying.playerParry = false;
                player.isBlocking = false;

                if(!player.playerParry)
                {
                    player.playerPostureBar = player.playerPostureBar + 100;
                }
            }

            




            player.parryTimer -= Time.deltaTime;
            
            base.LogicUpdate();
            if (!player.CheckForBlock())
            {
                player.StartCoroutine(player.LeaveParry());

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

                if (player.CheckForAttack())
                {
                    sm.ChangeState(player.attackState);
                }

                if (player.CheckForPostureStun())
                {
                    sm.ChangeState(player.postureStunState);
                }

            }
        }

        

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
        
       

    }
}
