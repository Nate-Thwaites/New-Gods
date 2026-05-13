using UnityEngine;


namespace Player
{
    public class IdleState : State
    {
        // constructor
        public IdleState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.moveDir = 0;
            player.anim.Play("idle temp");

            

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

            if (player.playerPosture > player.minPlayerPosture)
            { 
                player.playerPosture = player.playerPosture -3 * Time.deltaTime;
            }


         


            base.LogicUpdate();

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

            if (player.CheckForBlock())
            {
                sm.ChangeState(player.blockingState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            
        }
    }
}