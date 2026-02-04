using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;

namespace Player
{
    public class FallingState : State
    {
        // constructor
        public FallingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }
        JumpingState jumpingState;
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

           
            
            if (player.CheckForRun())
            {
                sm.ChangeState(player.walkingState);
                
            }

            if (player.CheckForIdle())
            {
                sm.ChangeState(player.idleState);
                

            }

            
            {

            }

            
        

    }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}