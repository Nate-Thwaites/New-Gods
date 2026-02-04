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

            if(Input.GetKeyUp(KeyCode.Space))
            {
                player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, 0);
                player.rb.AddForce(Vector2.down * player.jumpForce * 0.3f, ForceMode2D.Impulse);
            }

            if (Input.GetKey("d"))
            {
                //initVelocity -= 2;

                if (player.rb.linearVelocity.x > 0)
                {
                    player.rb.linearVelocity = new Vector2(initVelocity, player.rb.linearVelocity.y);
                }
                else
                {
                    player.rb.linearVelocity = new Vector2(initVelocity * 0.5f, player.rb.linearVelocity.y);

                }

            }

            if (Input.GetKey("a"))
            {
                if (player.rb.linearVelocity.x < 0)
                {
                    player.rb.linearVelocity = new Vector2(initVelocity, player.rb.linearVelocity.y);
                }
                else
                {
                    player.rb.linearVelocity = new Vector2(initVelocity * 0.5f, player.rb.linearVelocity.y);

                }


            }

            if (player.rb.linearVelocity.x == 0)
            {
                if (Input.GetKey("d"))
                {
                    player.rb.linearVelocity = new Vector2(initVelocity + 3f, player.rb.linearVelocity.y);
                }

                if (Input.GetKey("a"))
                {
                    player.rb.linearVelocity = new Vector2(initVelocity - 3f, player.rb.linearVelocity.y);
                }
            }

            /*
                if GetKeyUp(space) and isjumping
                then!
                fall

            

             */








            if (player.CheckForFall())
            {
                sm.ChangeState(player.fallingState);
            }




            
        }

        
    }
}