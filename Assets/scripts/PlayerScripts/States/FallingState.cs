using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


namespace Player
{
    public class FallingState : State
    {
        public float initVelocity;
        public FallingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            player.anim.Play("fall");

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


            if (player.playerPosture > player.minPlayerPosture)
            {
                player.playerPosture = player.playerPosture - Time.deltaTime;
            }

            if (player.moveDir > 0)
            {
                player.transform.localScale = new Vector3(player.moveDir, 1f, 1f);
                if (player.rb.linearVelocity.x > 0)
                {
                    player.rb.linearVelocity = new Vector2(initVelocity, player.rb.linearVelocity.y);
                    
                }
                else
                {
                    player.rb.linearVelocity = new Vector2(initVelocity * 0.5f, player.rb.linearVelocity.y);
                    player.jumpDirChange = true;
                    

                }

            }

            if(player.moveDir < 0)
            {
                player.transform.localScale = new Vector3(player.moveDir, 1f, 1f);
                if (player.rb.linearVelocity.x < 0)
                {
                    player.rb.linearVelocity = new Vector2(initVelocity, player.rb.linearVelocity.y);
                    
                }
                else
                {
                    player.rb.linearVelocity = new Vector2(initVelocity * 0.5f, player.rb.linearVelocity.y);
                    player.jumpDirChange = true;
                    
                }


            }

            if (player.rb.linearVelocity.x < 0.1f && player.rb.linearVelocity.x > -0.1f)
            {
                if (player.moveDir > 0)
                {
                   
                    player.rb.linearVelocity = new Vector2(initVelocity + 3f, player.rb.linearVelocity.y);
                    player.jumpDirChange = true;
                    
                }

                if (player.moveDir < 0)
                {
                   
                    player.rb.linearVelocity = new Vector2(initVelocity - 3f, player.rb.linearVelocity.y);
                    player.jumpDirChange = true;
                    
                }
            }

            float dist = 1.5f;
            Vector3 offset = new Vector3(0.75f, 0, 0);

            bool wallHit = Physics2D.Raycast(player.transform.position - offset, Vector2.right, dist, player.floor);

            if (wallHit)
            {
                Debug.DrawRay(player.transform.position - offset, Vector2.right * dist, Color.green);
                player.rb.linearVelocity = new Vector2(0,player.rb.linearVelocity.y);


            }

            base.LogicUpdate();

           

            if (player.isGrounded) 
            {
                if (player.CheckForRun())
                {
                    sm.ChangeState(player.walkingState);

                }

                if (player.CheckForIdle())
                {
                    sm.ChangeState(player.idleState);


                }
            }
            
            

            

            
        

    }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

          
        }
    }
}