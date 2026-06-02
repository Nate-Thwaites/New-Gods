using Enemy;
using System.Collections;
using System.Security.Cryptography;
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
        public GameObject slipObject;


        [Space(10)]

        #endregion core variables

        #region Audio

        [Header("Audio")]
        public AudioManager am;
        public AudioSource audioSource;

        [Space(10)]

        #endregion Audio

        #region UI variables

        [Header("UI variables")]
        public GameObject itemText;
        //public TMPro.TextMeshProUGUI stateText;
        public PlayerHealthBar playerHealthBar;
        public PlayerPostureBar playerPostureBar;
        public bool gameIsPaused = false;

        public Button resumeButton;
        public Button audioButton;
        public Button moveButton;
        public Button jumpButton;
        public Slider masterVolumeSlider;

        public GameObject pauseMenuUI;
        public GameObject pauseMenuCanvas;
        public bool canPressButton;
        public GameObject settingsMenu;
        public GameObject keyboardControlMenu;
        public GameObject gamepadControlMenu;
        public GameObject volumeSettings;
        public GameObject endScreen;
        [SerializeField] Slider musicSlider;

        public GameObject deathScreen;
        

        public TMPro.TextMeshProUGUI healthItemText;
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
        public float attackDamage;
        public bool hitEnemy;
        public bool attackStunned;
        [Space(10)]

        #endregion Attack variables

        #region Block variables

        [Header("Block variables")]

        //public BlockingAndParrying blockingAndParrying;

        
        public bool isBlocking;
        public bool playerParry;
        public bool stunEnemy;
        public bool parryStunned;


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
        public PlayerAttackStunState attackStunState;
        public PlayerParryStunState playerParryStunState;
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

        public HealthScript health;

        public int healthItemCount;

        [Space(10)]

        #endregion health variables

        #region rng variables

        [Header("RNG variables")]

        public RandomNumberGenerator rng;

        [Space(10)]

        #endregion rng variables

        #region posture variables

        [Header("Posture variables")]

        public PostureScript posture;
        public bool delayPostureDecrease;

        [Space(10)]

        #endregion posture variables

        #region Item Detection variables

        [Header("Item Detection variables")]
        LayerMask itemMask;
        public bool hasHealItem;
        public GameObject HealItem;

        #endregion Item Detection variables

        #region particle variables

        [Header("Particle variables")]
        public ParticleSystem particle;

        #endregion particle variables

        #region powerups

        [Header("Power Up variables")]

        public bool hasPowerup;
        #endregion powerups

        #endregion variables


        #region unity methods
        void Start()
        {
            healthItemCount = 0;
            playerPostureBar.SetMaxPosture(posture.maxPosture);
            playerHealthBar.SetMaxHealth(health.maxHealth);

            Time.timeScale = 1f;

            canPressButton = true;

            hasPowerup = false;

            delayPostureDecrease = false;

            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            anim = GetComponent<Animator>();

            audioSource = GetComponent<AudioSource>();

            health = GetComponent<HealthScript>();
            posture = GetComponent<PostureScript>();
            am = am.GetComponent<AudioManager>();




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
            attackStunState = new PlayerAttackStunState(this, sm);
            playerParryStunState = new PlayerParryStunState(this, sm);

            // initialise the statemachine with the default state
            sm.Init(idleState);



        }


        // Update is called once per frame
        public void Update()
        {
            playerHealthBar.UpdateHealthBar(health.health);
            playerPostureBar.UpdatePostureBar(posture.posture);

            



            if (pauseAction.WasPressedThisFrame())
            {
                if (!gameIsPaused)
                {
                    canPressButton = false;
                    Pause();
                }
            }

            particle.transform.position = transform.position;

            Die();

            

            GroundCheck();
            //ItemDetection();
            PlayerHeal();


            
            
            

            attackTimer -= Time.deltaTime;
            attackCompleteTimer -= Time.deltaTime;


            
            
            if (blockAction.WasPressedThisFrame() && canPressButton)
            {
                isBlocking = true;
            }



            else if (blockAction.WasReleasedThisFrame())
            {
                isBlocking = false;
            }
            

            
            healthItemText.text = "X " + healthItemCount;

           
            if ((sm.CurrentState == null))
            {
                
                return;
            }


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
            if (canPressButton)
            {
                if (jumpAction.WasPressedThisFrame() && isGrounded)//coyoteTime > 0)
                {
                    //coyoteTime = 0f;
                    return true;

                }
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
            if (canPressButton)
            {
                if (isBlocking)
                {
                    
                    return true;
                }
            }

            



            return false;
        }

        public bool CheckForParryStun()
        {
            if(parryStunned)
            {
                return true;
            }

            return false;
        }

        public bool CheckForPostureStun()
        {
            if(posture.posture >= posture.maxPosture)
            {
                
                return true;
            }

            return false;
        }

        public bool CheckForAttackStun()
        {
            if (attackStunned)
            {
                return true;
            }

            return false;
        }



        #endregion state checks

        

        #region leave parry coroutine
        public IEnumerator LeaveParry()
        {
            yield return new WaitForSeconds(0.1f);
            playerParry = false;
        }

        #endregion leave parry coroutines

        

        #region stun couroutines
        public IEnumerator PostureStun()
        {
            yield return new WaitForSeconds(0.4f);
            
            posture.posture = posture.minPosture;
        }

        public IEnumerator ParryStun()
        {
            yield return new WaitForSeconds(0.3f);
            parryStunned = false;
            canPressButton = true;
        }

        public IEnumerator AttackStun()
        {
            yield return new WaitForSeconds(0.3f);
            attackStunned = false;
            //print("attack stun over");
            canPressButton = true;
        }

        #endregion stun couroutines

        public IEnumerator PostureDecrease()
        {
            yield return new WaitForSeconds(0.2f);
            delayPostureDecrease = true;
        }

        #region Raycast detection 
        public void GroundCheck()
        {
            Vector3 ofs1 = new Vector3 (0,0,0);
            Vector3 ofs2 = new Vector3(-0.3f, 0, 0);
            Vector3 ofs3 = new Vector3(0.3f, 0, 0);

            bool hit1 = Physics2D.Raycast(transform.position + ofs1, Vector2.down, 0.55f, floor);
            bool hit2 = Physics2D.Raycast(transform.position + ofs2, Vector2.down, 0.55f, floor);
            bool hit3 = Physics2D.Raycast(transform.position + ofs3, Vector2.down, 0.55f, floor);

            
    
         

            if (hit1 || hit2 || hit3)
            {
                Debug.DrawRay(transform.position + ofs1, Vector2.down * 0.55f, Color.green);
                Debug.DrawRay(transform.position + ofs2, Vector2.down * 0.55f, Color.green);
                Debug.DrawRay(transform.position + ofs3, Vector2.down * 0.55f, Color.green);
                isGrounded = true;
            }

            else if (!hit1 && !hit2 && !hit3)
            {
                Debug.DrawRay(transform.position + ofs1, Vector2.down * 0.55f, Color.red);
                Debug.DrawRay(transform.position + ofs2, Vector2.down * 0.55f, Color.red);
                Debug.DrawRay(transform.position + ofs3, Vector2.down * 0.55f, Color.red);
                isGrounded = false;
               
            }
        }





        private void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.gameObject.CompareTag("Heal Item"))
            {
                print("pick up");
                Destroy(hit.gameObject);
                healthItemCount += 1;

                
            }

            if(hit.gameObject.CompareTag("Power Up"))
            {

               Destroy(hit.gameObject);
               hasPowerup = true;

            }


            if(hit.gameObject.CompareTag("End Zone"))
            {
                Time.timeScale = 0f;
                endScreen.SetActive(true);
            }

            
        }





        /*public void ItemDetection()
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
               
            }
        }*/

        #endregion Raycast detection

        #region player death

        public IEnumerator DeathScreen()
        {
            yield return new WaitForSeconds(1.3f);
            Time.timeScale = 0f;
            deathScreen.SetActive(true);
        }

        public void Die()
        {
            if (health.health <= health.minHealth)
            {
                sm = null;
                canPressButton = false;
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
                anim.Play("Die", 0);
                StartCoroutine(DeathScreen());
            }
        }

        public void Restart()
        {
            SceneManager.LoadSceneAsync("Game");
            
            

        }



        #endregion player death

        #region heal void

        public void PlayerHeal()
        {
            HealthScript health;

            health = GetComponent<HealthScript>();
            if (healthItemCount > 0 && healAction.WasPressedThisFrame())
            {
                health.health += 30;
                healthItemCount -= 1;
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
            canPressButton = true;

            pauseMenuCanvas.SetActive(false);
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1; 
            gameIsPaused = false;
        }


        public void Pause()
        {
            pauseMenuCanvas.SetActive(true);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            gameIsPaused = true;
            resumeButton.Select();
        }

        public void Menu()
        {
            SceneManager.LoadSceneAsync("Menu");
        }

        public void OpenSettings()
        {
            settingsMenu.SetActive(true);
            pauseMenuUI.SetActive(false);
            audioButton.Select();
        }

        #endregion main pause menu

        #region pause settings
        public void OpenVolumeSettings()
        {
            volumeSettings.SetActive(true);
            settingsMenu.SetActive(false);
            masterVolumeSlider.Select();
        }

        public void OpenKeyboardControls()
        {
            settingsMenu.SetActive(false);
            keyboardControlMenu.SetActive(true);
            moveButton.Select();
        }

        public void OpenGamepadControls()
        {
            settingsMenu.SetActive(false);
            gamepadControlMenu.SetActive(true);
            jumpButton.Select();
        }

        public void BackToSettingsMenu()
        {
            settingsMenu.SetActive(true);
            keyboardControlMenu.SetActive(false);
            gamepadControlMenu.SetActive(false);
            volumeSettings.SetActive(false);
            audioButton.Select();
        }

        public void BackToPauseMenu()
        {
            pauseMenuUI.SetActive(true);
            settingsMenu.SetActive(false);
            resumeButton.Select();
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

            

           

        }



        #endregion random num for enemy block
       
        public void RespawnDamage()
        {
            health.health = health.health - 10;
        }
        public void ParryParticle()
        {
            particle.Play();
        }

      
    }
}