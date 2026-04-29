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
            Debug.Log("stun");
            player.rb.AddForce(5 * player.transform.right * 0.5f, ForceMode2D.Impulse);



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

            player.StartCoroutine(player.PostureStun());


            base.LogicUpdate();

            if (player.playerPostureBar == player.minPlayerPostureBar)
            {

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
            }



        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}