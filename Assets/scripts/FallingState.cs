using UnityEngine;
using System;

namespace Player
{
    public class FallingState : State
    {
        // constructor
        public FallingState(PlayerScript player, StateMachine sm) : base(player, sm)
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
                player.rb.AddForce (Vector2.right * 5);
                


            }

            if (Input.GetKey("a"))
            {
                
                player.rb.AddForce(Vector2.left * 5);
                
            }

            if (player.CheckForRun())
            {
                sm.ChangeState(player.walkingState);
            }

            if (player.CheckForIdle())
            {
                sm.ChangeState(player.idleState);
            }

            Mathf.Clamp(player.rb.linearVelocity.x, -5, 5);
        

    }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}