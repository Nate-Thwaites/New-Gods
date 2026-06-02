using UnityEngine;


namespace Player
{
    public class JumpingState : State
    {
        // constructor
        public JumpingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }
        public float initVelocity;
        public override void Enter()
        {
            base.Enter();

            player.anim.Play("jump");

            player.rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);


            initVelocity = player.rb.linearVelocity.x;
            
            

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


            if (player.jumpAction.WasReleasedThisFrame())
            {
                player.rb.AddForce(Vector2.down * player.jumpForce * 0.5f, ForceMode2D.Impulse);
            }


            if (player.moveInput.x > 0)
            {
                
                if (player.rb.linearVelocity.x > 0)
                {
                    player.rb.linearVelocity = new Vector2(initVelocity, player.rb.linearVelocity.y);
                    

                }
                else
                {
                    player.rb.linearVelocity = new Vector2(initVelocity * 0.5f, player.rb.linearVelocity.y);
                    player.jumpDirChange = true;
                    

                }
                player.transform.localScale = new Vector3(player.facingDir, 1f, 1f);
            }

            if (player.moveInput.x < 0)
            {
                player.transform.localScale = new Vector3(player.facingDir, 1f, 1f);
                if (player.rb.linearVelocity.x < 0)
                {
                    player.rb.linearVelocity = new Vector2(initVelocity, player.rb.linearVelocity.y);

                }
                else 
                {
                    player.rb.linearVelocity = new Vector2(initVelocity * 0.5f, player.rb.linearVelocity.y);
                    player.jumpDirChange = true;

                    player.transform.localScale = new Vector3(player.facingDir, 1f, 1f);

                }


            }

            if ( player.rb.linearVelocity.x < 0.1f && player.rb.linearVelocity.x > -0.1f)
            {
                if (player.facingDir > 0)
                {
                    player.rb.linearVelocity = new Vector2(initVelocity + 3f, player.rb.linearVelocity.y);
                    player.jumpDirChange = true;

                }

                if (player.facingDir < 0)
                {
                    player.rb.linearVelocity = new Vector2(initVelocity - 3f, player.rb.linearVelocity.y);
                    player.jumpDirChange = true;
                   
                }
            }




            if (player.posture.posture > player.posture.minPosture)
            {
                player.posture.posture = player.posture.posture - Time.deltaTime;
            }









            if (player.CheckForFall())
            {
                sm.ChangeState(player.fallingState);
            }

            if (player.CheckForAttackStun())
            {
                sm.ChangeState(player.attackStunState);
            }






        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            
        }
    }
}