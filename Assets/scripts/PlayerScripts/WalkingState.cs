using UnityEngine;
using System;

namespace Player
{
    public class WalkingState : State
    {
        // constructor
        public WalkingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();




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

            if (Input.GetKey("d"))
            {
                player.rb.linearVelocity = new Vector2(10, player.rb.linearVelocity.y);


            }

            if (Input.GetKey("a"))
            {
                player.rb.linearVelocity = new Vector2(-10, player.rb.linearVelocity.y);

                
            }


            if (player.CheckForIdle())
            {
                sm.ChangeState(player.idleState);

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