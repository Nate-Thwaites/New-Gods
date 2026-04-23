using UnityEngine;
using System.Collections;
using System;

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
            player.parryTimer = 2.18f;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            if (player.hitPlayer && player.isBlocking)
            {
                Debug.Log("Blocked attack");
                player.playerPostureBar = player.playerPostureBar + 10;
                //player.blockingAndParrying.hitPlayer = false;
                //player.blockingAndParrying.playerParry = false;
                player.isBlocking = false;
            }

            
            

            

            player.parryTimer -= Time.deltaTime;
            
            base.LogicUpdate();
            if (!player.CheckForBlock())
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

                if (player.CheckForAttack())
                {
                    sm.ChangeState(player.attackState);
                }

                if (player.CheckForParry())
                {
                    sm.ChangeState(player.parryState);
                }
            }
        }

        

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
        
       

    }
}
