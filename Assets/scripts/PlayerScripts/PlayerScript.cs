using Enemy;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        public EnemyScript enemyScript;
        [Space(10)]
        
        #endregion core variables


        #region UI variables

        [Header("UI variables")]
        public GameObject itemText;
        public TMPro.TextMeshProUGUI stateText;
        public PlayerHealthBar playerHealthBar;
        public PlayerPostureBar playerPostureBar;


        public TMPro.TextMeshProUGUI postureText;
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
        public int attackDamage = 10;
        public bool hitEnemy;
        [Space(10)]

        #endregion Attack variables

        #region Block variables

        [Header("Block variables")]

        //public BlockingAndParrying blockingAndParrying;

        public bool hitPlayer = false;
        public bool isBlocking;
        public bool playerParry;
        public bool stunEnemy;


        //public bool isBlocking;
        public float parryTimer = 0.18f;
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
        public PostureStunState postureStunState;
        public StateMachine sm;
        [Space(10)]

        #endregion StateMachine variables

        #region input variables

        [Header("Input variables")]
        public InputAction moveAction;
        public InputAction jumpAction;
        public InputAction attackAction;
        public InputAction blockAction;
        public InputAction healAction;
        public InputAction interactAction;
        [Space(10)]

        #endregion input variables

        #region health variables

        [Header("Health variables")]

        public int maxPlayerHealth = 100;

        public int minPlayerHealth = 0;

        public int playerHealth;


        [Space(10)]

        #endregion health variables

        #region rng variables

        [Header("RNG variables")]

        public RandomNumberGenerator rng;

        [Space(10)]

        #endregion rng variables

        #region posture variables

        [Header("Posture variables")]

        public float playerPosture;
        public int maxPlayerPosture = 100;
        public int minPlayerPosture = 0;
        [Space(10)]
        #endregion posture variables

        #region Item Detection variables

        [Header("Item Detection variables")]
        LayerMask itemMask;
        public bool hasHealItem;
        public GameObject HealItem;

        #endregion Item Detection variables

        #endregion variables


        #region unity methods
        void Start()
        {
            playerHealthBar.SetMaxHealth(maxPlayerHealth);
            playerPostureBar.SetMaxPosture(maxPlayerPosture);

            hasHealItem = false;
            playerPosture = minPlayerPosture;

            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            anim = GetComponent<Animator>();

            playerHealth = maxPlayerHealth;

            moveAction = InputSystem.actions.FindAction("Move");
            jumpAction = InputSystem.actions.FindAction("Jump");
            attackAction = InputSystem.actions.FindAction("Attack");
            blockAction = InputSystem.actions.FindAction("Block");
            healAction = InputSystem.actions.FindAction("Heal");
            interactAction = InputSystem.actions.FindAction("Interact");
            

            itemMask = LayerMask.GetMask("itemLayer");

            // add new states here
            idleState = new IdleState(this, sm);
            walkingState = new WalkingState(this, sm);
            jumpingState = new JumpingState(this, sm);
            fallingState = new FallingState(this, sm);
            attackState = new AttackState(this, sm);
            blockingState = new BlockingState(this, sm);
            postureStunState = new PostureStunState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(idleState);



        }


        // Update is called once per frame
        public void Update()
        {
            playerHealthBar.UpdateHealthBar(playerHealth);
            playerPostureBar.UpdatePostureBar(playerPosture);
            
            if (playerPosture < 0)
            {
                playerPosture = 0;
            }

            if (playerPosture > maxPlayerPosture)
            {
                playerPosture = maxPlayerPosture;
            }

            if (playerHealth > maxPlayerHealth)
            {
                playerHealth = maxPlayerHealth;
            }



            GroundCheck();
            ItemDetection();
            PlayerHeal();

            if (blockAction.WasPressedThisFrame())
            {
                isBlocking = true;
                

            }

            else if (blockAction.WasReleasedThisFrame())
            {
                isBlocking = false;
            }

            stateText.text = "State: " + sm.CurrentState;
            postureText.text = "Posture: " + playerPostureBar;

           
            if ((sm.CurrentState == null))
            {
                
                return;
            }

            PlayerDie();

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
                
                return true;
            }

            return false;
        }

        public bool CheckForPostureStun()
        {
            if(playerPosture >= maxPlayerPosture)
            {
                
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

        #region leave parry coroutine
        public IEnumerator LeaveParry()
        {
            yield return new WaitForSeconds(0.1f);
            playerParry = false;
        }

        #endregion leave parry coroutines

        public IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(0.2f);
            print("hit enemy");
            //hitEnemy = true;
            if (!enemyScript.blockEnemy && !enemyScript.parryEnemy)
            {
                enemyScript.enemyHealth -= attackDamage;
            }
        }

        #region stun couroutines
        public IEnumerator PostureStun()
        {
            yield return new WaitForSeconds(1f);
            playerPosture = 0;
        }

        #endregion stun couroutines

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
           

            float dist = 4;


            Vector3 offset = new Vector3(2, 0, 0);

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

            if (itemHit &&  interactAction.WasPressedThisFrame())
            {
                print("pick up");
                Destroy(HealItem);
                hasHealItem = true;
            }
        }

        #endregion Raycast detection

        #region player death

        void PlayerDie()
        {
            if (playerHealth <= minPlayerHealth)
            {
                
                SceneManager.LoadSceneAsync("Game");
                sm.ChangeState(idleState);

            }
        }

        #endregion player death

        #region heal void

        void PlayerHeal()
        {
            if (hasHealItem && healAction.WasPressedThisFrame())
            {
                playerHealth = playerHealth + 30;
                hasHealItem = false;
            }
        }

        #endregion heal void

        public void RandomNumForEnemyBlock()
        {
            if (rng == null)
            {
                rng = RandomNumberGenerator.Create();
            }

            // Use 4 bytes to produce a 32-bit unsigned int, then map to 1..100
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            uint rand = System.BitConverter.ToUInt32(buffer, 0);
            int rngValue = (int)(rand % 100) + 1;

            enemyScript.blockOrParryChance = rngValue; 

            

           

            Debug.Log(rngValue);

            /* rng = RandomNumberGenerator.Create();

             byte[] randomNumber = new byte[1];
             rng.GetBytes(randomNumber);
             int rngValue = randomNumber[0] % 100;

             rngValue = enemyScript.blockOrParryChance;

             Debug.Log(rngValue);*/
        }
    }
}