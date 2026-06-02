using UnityEngine;


namespace Player
{
    public class PlayerAttackStunState : State
    {
        // constructor
        public PlayerAttackStunState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            player.attackStunned = true;
            player.canPressButton = false;
            player.rb.linearVelocity = new Vector2(0, 0);
            player.StartCoroutine(player.AttackStun());
            player.anim.Play("idle temp", 0);
            
            player.anim.Play("Hit", 0);
            
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

            

            if (!player.attackStunned)
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

                if (player.CheckForBlock())
                {
                    sm.ChangeState(player.blockingState);
                }

                
                sm.ChangeState(player.idleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}