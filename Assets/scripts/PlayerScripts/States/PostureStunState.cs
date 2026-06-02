using Player;
using UnityEngine;

namespace Player
{
    public class PostureStunState : State
    {
        public PostureStunState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            player.rb.AddForce(-15 * player.transform.right * player.facingDir, ForceMode2D.Impulse);

            player.StartCoroutine(player.PostureStun());


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

            if (player.posture.posture <= player.posture.minPosture)
            {
                if (player.isGrounded)
                {
                    player.rb.linearVelocity = Vector2.zero;
                }

                if (player.CheckForRun())
                {
                    sm.ChangeState(player.walkingState);
                }

                if (player.CheckForIdle())
                {
                    sm.ChangeState(player.idleState);
                }

                if (player.CheckForFall())
                {
                    sm.ChangeState(player.fallingState);
                }

                if (player.CheckForBlock())
                {
                    sm.ChangeState(player.blockingState);
                }   
            }



        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}