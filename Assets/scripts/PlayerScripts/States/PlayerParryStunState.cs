using UnityEngine;


namespace Player
{
    public class PlayerParryStunState : State
    {
        // constructor
        public PlayerParryStunState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.rb.AddForce(new Vector2(player.facingDir * -20, 0));
            player.canPressButton = false;
            player.StartCoroutine(player.ParryStun());


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
            if (!player.parryStunned)
            {
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

                if (player.CheckForAttackStun())
                {
                    sm.ChangeState(player.attackStunState);
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }

}
