
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{


    public class PlayerScript : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody2D rb;
        [HideInInspector]
        public Animator anim;
        public float jumpForce = 10f;
        public float moveDir;

        public GameObject itemText;
        public TMPro.TextMeshProUGUI stateText;

        public bool isGrounded;
        public LayerMask floor;


        public bool jumpDirChange;

        public ControlManager control;

        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyLayer;
        

        // variables holding the different player states
        public IdleState idleState;
        public WalkingState walkingState;
        public JumpingState jumpingState;
        public FallingState fallingState;
        public AttackState attackState;

        public InputAction moveAction;
        public InputAction jumpAction;
        public InputAction attackAction;
        public InputBinding moveLeft;

        public StateMachine sm;


        LayerMask mask;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            control = FindFirstObjectByType<ControlManager>();

            moveAction = InputSystem.actions.FindAction("Move");
            jumpAction = InputSystem.actions.FindAction("Jump");
            attackAction = InputSystem.actions.FindAction("Attack");
            //anim = GetComponent<Animator>();

            mask = LayerMask.GetMask("itemLayer");

            // add new states here
            idleState = new IdleState(this, sm);
            walkingState = new WalkingState(this, sm);
            jumpingState = new JumpingState(this, sm);
            fallingState = new FallingState(this, sm);
            attackState = new AttackState(this,sm);

            // initialise the statemachine with the default state
            sm.Init(idleState);



        }


        // Update is called once per frame
        public void Update()
        {
            GroundCheck();



            stateText.text = "State: " + sm.CurrentState;

            if ((sm.CurrentState == null))
            {
                print("null");
                return;
            }


            moveDir = moveAction.ReadValue<Vector2>().x;



            sm.CurrentState.LogicUpdate();
            


            //Debug.Log("x=" + moveAction.ReadValue<Vector2>().x);
            //Debug.Log("y=" + moveAction.ReadValue<Vector2>().y);


        }



        void FixedUpdate()
        {
            if ((sm.CurrentState == null))
            {
                print("physics update null"); return;
            }

            sm.CurrentState.PhysicsUpdate();


            //DoRayTest();


        }


        public void OnMove(InputValue value)
        {
            print("move");
        }


        public bool CheckForRun()
        {

            if (Mathf.Abs(moveDir) > 0 && isGrounded)
            {

                return true;
            }

            return false;
        }

        public bool CheckForIdle()
        {
            if (moveDir == 0 && isGrounded)
            {

                return true;
            }

            return false;
        }

        public bool CheckForJump()
        {
            if (jumpAction.WasPressedThisFrame() && isGrounded)
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
            if(attackAction.WasPressedThisFrame())
            {
                return true;
            }

            return false;
        }

        public void GroundCheck()
        {
            Vector3 ofs1 = new Vector3 (0,0,0);
            Vector3 ofs2 = new Vector3(-0.5f, 0, 0);
            Vector3 ofs3 = new Vector3(0.5f, 0, 0);

            bool hit1 = Physics2D.Raycast(transform.position + ofs1, Vector2.down, 0.55f, LayerMask.GetMask("floor"));
            bool hit2 = Physics2D.Raycast(transform.position + ofs2, Vector2.down, 0.55f, LayerMask.GetMask("floor"));
            bool hit3 = Physics2D.Raycast(transform.position + ofs3, Vector2.down, 0.55f, LayerMask.GetMask("floor"));

            print("h1= " + hit1 + "  h2=" + hit2 + "  h3=" + hit3);


            if (hit1 || hit2 || hit3)
            {
                Debug.DrawRay(transform.position, Vector2.down * 0.55f, Color.green);


                isGrounded = true;
            }

            else
            {
                Debug.DrawRay(transform.position, Vector2.down * 0.55f, Color.red);

                isGrounded = false;
                
            }
        }

        void DoRayTest()
        {
            bool hit = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, LayerMask.GetMask("floor"));




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