using UnityEngine;


namespace Player
{
    public class JumpingState : State
    {
        // constructor
        public JumpingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            
            player.rb.AddForce(Vector2.up * player.jumpForce,ForceMode2D.Impulse);
            

            
            
            

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
                player.rb.AddForce(Vector2.down * player.jumpForce);
            }

            if (Input.GetKey("d"))
            {
                player.rb.linearVelocity = new Vector2(5, player.rb.linearVelocityY);


            }

            if (Input.GetKey("a"))
            {
                player.rb.linearVelocity = new Vector2(-5, player.rb.linearVelocityY);


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