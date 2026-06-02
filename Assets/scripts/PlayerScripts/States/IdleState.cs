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
            player.rb.linearVelocity = new Vector2 (0, 0);
            player.anim.Play("idle temp");
            player.StartCoroutine(player.PostureDecrease());



        }

        public override void Exit()
        {
            base.Exit();
            player.delayPostureDecrease = false;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            if (player.delayPostureDecrease)
            {
                if (player.posture.posture > player.posture.minPosture)
                {
                    player.posture.posture = player.posture.posture - 2 * Time.deltaTime;
                    Debug.Log("Posture decrea");
                }
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

            if (player.CheckForAttackStun())
            {
                sm.ChangeState(player.attackStunState);
            }

            if (player.CheckForPostureStun())
            {
                sm.ChangeState(player.postureStunState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            
        }
    }
}