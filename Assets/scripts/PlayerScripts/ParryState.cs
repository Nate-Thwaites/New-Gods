using UnityEngine;


namespace Player
{
    public class ParryState : State
    {
        // constructor
        public ParryState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("parried");
            player.playerPostureBar = player.playerPostureBar + 0;



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

            //Debug.Log(player.moveAction.ReadValue<float>());

            base.LogicUpdate();

            if(!player.CheckForParry())
            {
                player.StartCoroutine(player.LeaveParry());

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