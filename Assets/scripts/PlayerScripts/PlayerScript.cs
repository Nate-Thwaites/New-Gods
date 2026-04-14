using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{

    

    public class PlayerScript : MonoBehaviour
    {
        #region variables

        #region core variables

        [HideInInspector]
        public Rigidbody2D rb;
        [Header("Core variables")]
        public Animator anim;
        [Space(10)]

        #endregion core variables


        #region UI variables

        [Header("UI variables")]
        public GameObject itemText;
        public TMPro.TextMeshProUGUI stateText;
        [Space(10)]

        #endregion UI variables


        #region Jump and Movement variables

        [Header("Jump and Movement variables")]
        public float jumpForce = 13f;
        public float moveDir;
        public bool isGrounded;
        public LayerMask floor;
        public bool jumpDirChange;
        [Space(10)]

        #endregion Jump and Movement variables

        #region Attack variables

        [Header("Attack variables")]
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyLayer;
        public int maxAttackNum = 3;
        public float attackTimer = 2f;
        public float attackCompleteTimer = 0.5f;
        [Space(10)]

        #endregion Attack variables

        #region Block variables
        [Header("Block variables")]
        public bool isBlocking;
        [Space(10)]
        #endregion Block variables 

        #region StateMachine variables

        [Header("StateMachine variables")]
        public IdleState idleState;
        public WalkingState walkingState;
        public JumpingState jumpingState;
        public FallingState fallingState;
        public AttackState attackState;
        public BlockingState blockingState;
        public StateMachine sm;
        [Space(10)]

        #endregion StateMachine variables

        #region input variables

        [Header("Input variables")]
        public InputAction moveAction;
        public InputAction jumpAction;
        public InputAction attackAction;
        public InputAction blockAction;
        [Space(10)]

        #endregion input variables




        #region Item Detection variables

        [Header("Item Detection variables")]
        LayerMask itemMask;


        #endregion Item Detection variables

        #endregion variables


        #region unity methods
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            

            moveAction = InputSystem.actions.FindAction("Move");
            jumpAction = InputSystem.actions.FindAction("Jump");
            attackAction = InputSystem.actions.FindAction("Attack");
            blockAction = InputSystem.actions.FindAction("Block");
            //anim = GetComponent<Animator>();

            itemMask = LayerMask.GetMask("itemLayer");

            // add new states here
            idleState = new IdleState(this, sm);
            walkingState = new WalkingState(this, sm);
            jumpingState = new JumpingState(this, sm);
            fallingState = new FallingState(this, sm);
            attackState = new AttackState(this, sm);
            blockingState = new BlockingState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(idleState);



        }


        // Update is called once per frame
        public void Update()
        {
            GroundCheck();

            if (blockAction.WasPressedThisFrame())
            {
                isBlocking = true;
            }

            else if (blockAction.WasReleasedThisFrame())
            {
                isBlocking = false;
            }

            stateText.text = "State: " + sm.CurrentState;

            if ((sm.CurrentState == null))
            {
                
                return;
            }


            moveDir = moveAction.ReadValue<Vector2>().x;

            attackTimer -= Time.deltaTime;
            attackCompleteTimer -= Time.deltaTime;

            
            sm.CurrentState.LogicUpdate();
        }



        void FixedUpdate()
        {
            if ((sm.CurrentState == null))
            {
                print("physics update null");
                
                return;
            }

            sm.CurrentState.PhysicsUpdate();


            //DoRayTest();


        }
        #endregion unity methods


        #region state checks


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
            if(attackAction.WasPressedThisFrame() && attackCompleteTimer <= 0)
            {
                if (attackTimer >= 0)
                {
                    attackState.attackNum++;
                }

                if((int)attackState.attackNum > maxAttackNum || attackTimer < 0)
                {
                   
                    attackState.attackNum = 0;
                }

                
                return true;
            }

            return false;
        }

        public bool CheckForBlock()
        {
            if(isBlocking)
            {

                /*
                 if(attack lands and block is pressed within a certain time frame)
                {
                    bool parry = true;
                }
                 */
                return true;
            }

            return false;
        }



        #endregion state checks

        #region attack debug temp
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(attackPoint.position, attackRange);
        }
        #endregion attack debug temp

        #region Raycast detection 
        public void GroundCheck()
        {
            Vector3 ofs1 = new Vector3 (0,0,0);
            Vector3 ofs2 = new Vector3(-0.5f, 0, 0);
            Vector3 ofs3 = new Vector3(0.5f, 0, 0);

            bool hit1 = Physics2D.Raycast(transform.position + ofs1, Vector2.down, 0.55f, LayerMask.GetMask("floor"));
            bool hit2 = Physics2D.Raycast(transform.position + ofs2, Vector2.down, 0.55f, LayerMask.GetMask("floor"));
            bool hit3 = Physics2D.Raycast(transform.position + ofs3, Vector2.down, 0.55f, LayerMask.GetMask("floor"));



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




       
        public void ItemDetection()
        {
           

            float dist = 2;


            Vector3 offset = new Vector3(1, 0, 0);

            bool itemHit = Physics2D.Raycast(transform.position - offset, Vector2.right, dist, itemMask);

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

        #endregion Raycast detection
    }
}