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

            Debug.Log("Blocking");





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

            if (player.CheckForAttack())
            {
                sm.ChangeState(player.attackState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}
