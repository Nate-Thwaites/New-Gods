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
        [Space(10)]
        public Animator anim;

        #endregion core variables


        #region UI variables

        [Header("UI variables")]
        [Space(10)]
        public GameObject itemText;
        public TMPro.TextMeshProUGUI stateText;

        #endregion UI variables


        #region Jump and Movement variables

        [Header("Jump and Movement variables")]
        [Space(10)]
        public float jumpForce = 13f;
        public float moveDir;
        public bool isGrounded;
        public LayerMask floor;
        public bool jumpDirChange;

        #endregion Jump and Movement variables

        #region Attack variables

        [Header("Attack variables")]
        [Space(10)]
        public Transform attackPoint;
        public float attackRange = 0.5f;
        public LayerMask enemyLayer;
        public int maxAttackNum = 3;
        public float attackTimer = 2f;

        #endregion Attack variables

        #region StateMachine variables

        [Header("StateMachine variables")]
        [Space(10)]
        public IdleState idleState;
        public WalkingState walkingState;
        public JumpingState jumpingState;
        public FallingState fallingState;
        public AttackState attackState;
        public StateMachine sm;

        #endregion StateMachine variables

        #region input variables

        [Header("Input variables")]
        [Space(10)]
        public InputAction moveAction;
        public InputAction jumpAction;
        public InputAction attackAction;

        #endregion input variables




        #region Item Detection variables

        [Header("Item Detection variables")]
        [Space(10)]
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
            //anim = GetComponent<Animator>();

            itemMask = LayerMask.GetMask("itemLayer");

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
                
                return;
            }


            moveDir = moveAction.ReadValue<Vector2>().x;

            attackTimer -= Time.deltaTime;

            
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
            if(attackAction.WasPressedThisFrame())
            {
                if (attackTimer >= 0)
                {
                    print("attack case = " + attackState.attackNum);

                    attackState.attackNum++;

                    /*AttackState.case 1;*/

                }

                if((int)attackState.attackNum > maxAttackNum || attackTimer < 0)
                {
                    print("attack case = " + attackState.attackNum);

                    attackState.attackNum = 0;
                }

                
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