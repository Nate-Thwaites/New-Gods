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
        public TMPro.TextMeshProUGUI stateText;

        public bool isGrounded;
        public LayerMask floor;

        
        
        

        // variables holding the different player states
        public IdleState idleState;
        public WalkingState walkingState;
        public JumpingState jumpingState;
        public FallingState fallingState;


        public StateMachine sm;


        LayerMask mask;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            //anim = GetComponent<Animator>();

            mask = LayerMask.GetMask("itemLayer");

            // add new states here
            idleState = new IdleState(this, sm);
            walkingState = new WalkingState(this, sm);
            jumpingState = new JumpingState(this, sm);
            fallingState = new FallingState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(idleState);

            print("layername=" + mask.value);
        }


        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.LogicUpdate();
           // print(sm.GetState().ToString());
           stateText.text = "State: " + sm.CurrentState;
        }



        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();


            DoRayTest();


        }



        public bool CheckForRun()
        {

            if (Input.GetKey("a") || Input.GetKey("d") && isGrounded)
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
            if (Input.GetKey (KeyCode.Space) && isGrounded)
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

        public bool CheckForAttack()
        {
            if(Input.GetKey("e"))
            {
                return true;
            }

            return false;
        }

        void DoRayTest()
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

            float dist = 2;


            Vector3 offset = new Vector3(1, 0, 0);

            bool itemHit = Physics2D.Raycast(transform.position - offset, Vector2.right, dist, mask);

            if (itemHit)
            {
                Debug.DrawRay(transform.position - offset, Vector2.right * dist, Color.green);
                itemText.SetActive(true);
            }

            else
            {
                Debug.DrawRay(transform.position - offset, Vector2.right * dist, Color.red);
                itemText.SetActive(false);
            }





        }




    }
}