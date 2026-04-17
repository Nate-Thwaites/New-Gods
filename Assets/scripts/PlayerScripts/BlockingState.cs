using UnityEngine;
using Enemy;

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
            

            if (player.parryTimer > 0 )
            {
                Debug.Log("Parried attack");
                player.blockingAndParrying.playerParry = true;
            }

        }

        public override void Exit()
        {
            base.Exit();
            player.blockingAndParrying.playerParry = false;
            player.blockingAndParrying.isBlocking = false;
            player.parryTimer = 0.18f;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            if (player.blockingAndParrying.hitPlayer && player.blockingAndParrying.isBlocking)
            {
                Debug.Log("Blocked attack");
                player.blockingAndParrying.hitPlayer = false;
                player.blockingAndParrying.playerParry = false;
                player.blockingAndParrying.isBlocking = false;
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
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}
