using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using System;
using JetBrains.Annotations;

namespace Player
{


    public class PlayerScript : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Animator anim;
        public float jumpForce = 10f;

        public GameObject itemText;


        public bool isGrounded;
        public LayerMask floor;

        
        
        

        // variables holding the different player states
        public IdleState idleState;
        public WalkingState walkingState;
        public JumpingState jumpingState;
        public FallingState fallingState;


        public StateMachine sm;





        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            //anim = GetComponent<Animator>();

            

            // add new states here
            idleState = new IdleState(this, sm);
            walkingState = new WalkingState(this, sm);
            jumpingState = new JumpingState(this, sm);
            fallingState = new FallingState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(idleState);
        }


        // Update is called once per frame
        public void Update()
        {
            bool hit = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, LayerMask.GetMask("floor"));

            //print(sm.CurrentState);

            

            if (hit)
            {
                isGrounded = true;
                Debug.DrawRay(transform.position, Vector2.down * 0.55f, Color.green);
            }

            else
            {
                isGrounded = false;
                Debug.DrawRay(transform.position, Vector2.down * 0.55f, Color.red);
            }


            bool itemHitLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.8f, LayerMask.GetMask("item"));

            if (itemHitLeft)
            {
                Debug.DrawRay(transform.position, Vector2.left * 0.8f, Color.green);
                itemText.SetActive(true);
            }

            else
            {
                Debug.DrawRay(transform.position, Vector2.left * 0.8f, Color.red);
                itemText.SetActive(false);
            }

            bool itemHitRight = Physics2D.Raycast(transform.position, Vector2.right, 0.8f, LayerMask.GetMask("item"));

            if (itemHitRight)
            {
                Debug.DrawRay(transform.position, Vector2.right * 0.8f, Color.green);
                itemText.SetActive(true);
            }

            else
            {
                Debug.DrawRay(transform.position, Vector2.right * 0.8f, Color.red);
                itemText.SetActive(false);
            }


            sm.CurrentState.LogicUpdate();
        }



        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }

      

        public bool CheckForRun()
        {

            if (Input.GetKeyDown("a") || Input.GetKeyDown("d") && isGrounded)
            {

                return true;
            }

            return false;
        }

        public bool CheckForIdle()
        {
            if (Input.GetKey("a") == false && Input.GetKey("d") == false && isGrounded)
            {

                return true;
            }

            return false;
        }

        public bool CheckForJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
               return true;
            }

            return false;
        } 

        public bool CheckForFall()
        {
            if(rb.linearVelocity.y < 0 && !isGrounded)
            {
                return true;
            }

            return false;
        }


       
        
    }
}