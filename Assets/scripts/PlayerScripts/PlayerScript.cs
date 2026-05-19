using Enemy;
using System.Collections;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        public EnemyScript enemy;


        [Space(10)]
        
        #endregion core variables


        #region UI variables

        [Header("UI variables")]
        public GameObject itemText;
        //public TMPro.TextMeshProUGUI stateText;
        public PlayerHealthBar playerHealthBar;
        public PlayerPostureBar playerPostureBar;
        public bool gameIsPaused = false;
        public GameObject pauseMenuUI;
        public GameObject pauseMenuCanvas;
        public bool canPressButton;
        public GameObject settingsMenu;
        public GameObject keyboardControlMenu;
        public GameObject gamepadControlMenu;
        [SerializeField] Slider musicSlider;
        public GameObject enemyHealthCanvas;

        public TMPro.TextMeshProUGUI postureText;
        [Space(10)]

        #endregion UI variables

        #region Jump and Movement variables

        [Header("Jump and Movement variables")]
        public float jumpForce = 13f;
        public float coyoteTime = 0.2f;
        private float coyoteTimer;
        public float inputBuffer = 0.2f;
        private float inputTimer;
        public Vector2 moveInput;
        public float facingDir;
        public bool isGrounded;
        public LayerMask floor;
        public LayerMask wall;


        public int speed = 10;
        public bool jumpDirChange;
        
        [Space(10)]

        #endregion Jump and Movement variables

        #region Attack variables

        [Header("Attack variables")]
        public Transform attackPoint;
        public float attackRange = 1f;
        public LayerMask enemyLayer;
        public int maxAttackNum = 3;
        public float attackTimer = 2f;
        public float attackCompleteTimer;
        public int attackDamage;
        public bool hitEnemy;
        [Space(10)]

        #endregion Attack variables

        #region Block variables

        [Header("Block variables")]

        //public BlockingAndParrying blockingAndParrying;

        
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
        public InputAction pauseAction;
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
        public int postureDamage;

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

            canPressButton = true;

           /* if (!PlayerPrefs.HasKey("MusicVolume"))
            {
                PlayerPrefs.SetFloat("MusicVolume", 1);
                //LoadMusicVolume();
            }

            else
            {
                //LoadMusicVolume();
            }
*/
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
            pauseAction = InputSystem.actions.FindAction("Pause");


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
            if (pauseAction.WasPressedThisFrame())
            {
                if (gameIsPaused)
                {
                    canPressButton = true;
                    Resume();
                }
                else
                {
                    canPressButton = false;
                    Pause();
                }
            }

            playerHealthBar.UpdateHealthBar(playerHealth);
            playerPostureBar.UpdatePostureBar(playerPosture);

            

            if(!isGrounded)
            {
                coyoteTime -= Time.deltaTime;
            }

            GroundCheck();
            ItemDetection();
            PlayerHeal();


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

            attackTimer -= Time.deltaTime;
            attackCompleteTimer -= Time.deltaTime;

            

            if (blockAction.WasPressedThisFrame())
            {
                isBlocking = true;
                

            }
          /*  if (pauseAction.WasPressedThisFrame())
            {
                print("pause");
            }*/

            else if (blockAction.WasReleasedThisFrame())
            {
                isBlocking = false;
            }

            /*stateText.text = "State: " + sm.CurrentState;
            postureText.text = "Posture: " + playerPostureBar;*/

           
            if ((sm.CurrentState == null))
            {
                
                return;
            }

            PlayerDie();

            moveInput.x = moveAction.ReadValue<Vector2>().x;

            

            


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


            


        }
        #endregion unity methods


        #region state checks

        
        public bool CheckForRun()
        {
            if (canPressButton)
            {
                if (Mathf.Abs(moveInput.x) > 0 && isGrounded)
                {

                    return true;
                }
            }
            return false;
        }

        public bool CheckForIdle()
        {
            if (moveInput.x == 0 && isGrounded)
            {

                return true;
            }

            return false;
        }

        public bool CheckForJump()
        {
            if (jumpAction.WasPressedThisFrame() && isGrounded)//coyoteTime > 0)
            {
                //coyoteTime = 0f;
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
            if (canPressButton)
            {
                if (attackAction.WasPressedThisFrame() && attackCompleteTimer <= 0)
                {
                    if (attackTimer >= 0)
                    {
                        attackState.attackNum++;
                    }

                    if ((int)attackState.attackNum > maxAttackNum || attackTimer < 0)
                    {

                        attackState.attackNum = 0;
                    }


                    return true;
                }
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

        #region attack delay coroutine
        public IEnumerator AttackDelay(EnemyScript enemy)
        {

            yield return new WaitForSeconds(0.2f);
            print("hit enemy");
            //hitEnemy = true;
            if (!enemy.blockEnemy && !enemy.parryEnemy)
            {

                enemy.enemyHealth -= attackDamage;
            }
        }
        #endregion attack delay coroutine

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

            bool hit1 = Physics2D.Raycast(transform.position + ofs1, Vector2.down, 0.55f, floor | wall);
            bool hit2 = Physics2D.Raycast(transform.position + ofs2, Vector2.down, 0.55f, floor | wall);
            bool hit3 = Physics2D.Raycast(transform.position + ofs3, Vector2.down, 0.55f, floor | wall);

            
    
         

            if (hit1 || hit2 || hit3)
            {
                isGrounded = true;
            }

            else if (!hit1 && !hit2 && !hit3)
            {
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

        public void PlayerHeal()
        {
            if (hasHealItem && healAction.WasPressedThisFrame())
            {
                playerHealth = playerHealth + 30;
                hasHealItem = false;
            }
        }

        #endregion heal void

        #region player flip
        public void Flip()
        {
            if (moveInput.x > 0.1f)
            {
                facingDir = 1f;
            }

            else if(moveInput.x < -0.1f)
            {
                facingDir = -1f;
            }

            transform.localScale = new Vector3(facingDir, 1f, 1f);
        }
        #endregion player flip

        #region pause
        
        #region main pause menu
        public void Resume()
        {
            enemyHealthCanvas.SetActive(true);
            pauseMenuCanvas.SetActive(false);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1;
            gameIsPaused = false;
        }
        public void Pause()
        {
            enemyHealthCanvas.SetActive(false);
            pauseMenuCanvas.SetActive(true);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            gameIsPaused = true;
        }

        public void Menu()
        {
            SceneManager.LoadSceneAsync("Menu");
        }

        public void OpenSettings()
        {
            settingsMenu.SetActive(true);
            pauseMenuUI.SetActive(false);
        }

        #endregion main pause menu

        #region pause settings
        public void OpenKeyboardControls()
        {
            settingsMenu.SetActive(false);
            keyboardControlMenu.SetActive(true);
        }

        public void OpenGamepadControls()
        {
            settingsMenu.SetActive(false);
            gamepadControlMenu.SetActive(true);
        }

        public void BackToSettingsMenu()
        {
            settingsMenu.SetActive(true);
            keyboardControlMenu.SetActive(false);
            gamepadControlMenu.SetActive(false);
        }

        public void BackToPauseMenu()
        {
            pauseMenuUI.SetActive(true);
            settingsMenu.SetActive(false);
        }
        #endregion pause settings

        #endregion pause

        #region random num for enemy block

        public void RandomNumForEnemyBlock(EnemyScript enemy)
        {
            if (rng == null)
            {
                rng = RandomNumberGenerator.Create();
            }

            
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            uint rand = System.BitConverter.ToUInt32(buffer, 0);
            int rngValue = (int)(rand % 100) + 1;

            enemy.blockOrParryChance = rngValue; 

            

           

            Debug.Log(rngValue);
        }

        #endregion random num for enemy block
    }
}