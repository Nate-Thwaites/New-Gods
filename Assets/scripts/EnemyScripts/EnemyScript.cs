using Player;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemy
{


    public class EnemyScript : MonoBehaviour
    {
        #region variables 

        #region core variables

        [HideInInspector]
        public Rigidbody2D erb;
        [Header("Core variables")]
        public Animator anim;
        [Space(10)]

        #endregion core variables

        #region scriptable object, and manager script variables

        [Header("Scriptable Object variables")]

        public HealthManager healthManager;

        //public BlockingAndParrying blockingAndParrying;

        [Space(10)]

        #endregion scriptable object, and manager script variables

        #region StateMachine variables

        [Header("StateMachine variables")]
        public EnemyIdleState enemyIdleState;
        public EnemyChaseState enemyChaseState;
        public EnemyAttackState enemyAttackState;
        public EnemyParryStunState enemyParryStunState;
        public EnemyBlockState enemyBlockState;
        public EnemyParryState enemyParryState;
        public EnemyStateMachine esm;
        [Space(10)]

        #endregion StateMachine variables

        #region block and parry variables

        [Header("Block and parry variables")]
        public int blockOrParryChance;
        public bool blockEnemy;
        public bool parryEnemy;
        [Space(10)]

        #endregion block and parry variables
        #region stun variables

        [Header("Stun variables")]
        public bool parryStunEnemy;
        public bool attackStunEnemy;
        public bool postureBreakStunEnemy;
        [Space(10)]

        #endregion stun variables

        #region UI variables

        [Header("UI variables")]

        public TMPro.TextMeshProUGUI enemyStateText;
        public EnemyHealthBar enemyHealthBar;
        //public EnemyPostureBar enemyPostureBar;


        [Space(10)]

        #endregion UI variables

        #region health variables

        [Header("Health variables")]
        public int maxEnemyHealth = 100;
        public int minEnemyHealth = 0;
        public int enemyHealth;
        [Space(10)]

        #endregion health variables

        #region enemy attack variables

        [Header("enemy attack variables")]
        public float enemyAttackTimer = 1.5f;
        public float enemyAttackCompleteTimer = 0.5f;
        public Transform enemyAttackPoint;
        public int maxEnemyAttackNum = 3;
        public float enemyAttackRange = 0.5f;
        public bool attackReset;

        [Space(10)]

        #endregion eneny attack variables

        #region player detecting variables

        [Header("Player detecting variables")]
        public bool seePlayer;
        public bool attackPlayer;

        //public Transform player;
        public LayerMask playerLayer;

        public GameObject player;
        public PlayerScript playerScript;


        [Space(10)]

        #endregion player detecting variables

        #region enemy movement variables

        [Header("enemy movement Variables")]
        public float enemySpeed = 4f;
        public int enemyMoveDir;

        #endregion enemy movement variables

        #endregion variables

        void Start()
        {
            //enemyPostureBar.SetMaxPosture(maxEnemyPosture);
            enemyHealthBar.SetMaxHealth(maxEnemyHealth);
            erb = GetComponent<Rigidbody2D>();
            esm = gameObject.AddComponent<EnemyStateMachine>();

            enemyIdleState = new EnemyIdleState(this, esm);
            enemyChaseState = new EnemyChaseState(this, esm);
            enemyAttackState = new EnemyAttackState(this, esm);
            enemyParryStunState = new EnemyParryStunState(this, esm);
            enemyBlockState = new EnemyBlockState(this, esm);
            enemyParryState = new EnemyParryState(this, esm);

            player = GameObject.Find("player");
            playerScript = player.GetComponent<PlayerScript>();

            esm.Init(enemyIdleState);


        }

        // Update is called once per frame
        void Update()
        {
            DetectPlayer();
            DetectAttackPlayer();
            EnemyDie();

            enemyHealthBar.UpdateHealthBar(enemyHealth);
            //enemyPostureBar.UpdatePostureBar(enemyPosture);

            if (player.transform.position.x < transform.position.x)
            {
                enemyMoveDir = -1;
            }

            else
            {
                enemyMoveDir = 1;
            }

            if ((esm.CurrentState == null))
            {

                return;
            }

            enemyStateText.text = "Enemy State: " + esm.CurrentState;


            enemyAttackTimer -= Time.deltaTime;
            enemyAttackCompleteTimer -= Time.deltaTime;


            esm.CurrentState.LogicUpdate();
        }

        void FixedUpdate()
        {
            if ((esm.CurrentState == null))
            {
                print("physics update null");

                return;
            }

            esm.CurrentState.PhysicsUpdate();





        }

        public bool CheckForBlock()
        {
            if (blockOrParryChance <= 50 && blockOrParryChance > 0 && playerScript.hitEnemy)
            {
                return true;
            }

            //blockEnemy = false;
            return false;
        }

        public bool CheckForParry()
        {
            if (blockOrParryChance <= 90 && blockOrParryChance > 50 && blockOrParryChance > 0 && playerScript.hitEnemy)
            {
                return true;
            }

            //parryEnemy = false;
            return false;
        }

        public bool CheckForIdle()
        {
            if (!seePlayer)
            {
                return true;
            }

            return false;

        }

        public bool CheckForChase()
        {
            if (seePlayer && !attackPlayer)
            {
                return true;
            }


            return false;
        }

        public bool CheckForAttack()
        {
            if (attackPlayer && enemyAttackCompleteTimer <= 0)
            {
                if (enemyAttackTimer > 0)
                {

                    enemyAttackState.enemyAttackNum++;
                }

                if ((int)enemyAttackState.enemyAttackNum > maxEnemyAttackNum || enemyAttackTimer < 0)
                {

                    enemyAttackState.enemyAttackNum = 0;
                }

                return true;
            }



            return false;
        }

        public bool CheckForParryStun()
        {
            if (parryStunEnemy)
            {
                print("stunned");
                return true;
            }
            return false;
        }

        public void DetectAttackPlayer()
        {
            float dist = 4;


            Vector3 offset = new Vector3(2, 0, 0);

            bool playerHit = Physics2D.Raycast(transform.position - offset, Vector2.right, dist, playerLayer);

            if (playerHit)
            {
                Debug.DrawRay(transform.position - offset, Vector2.right * dist, Color.blue);
                attackPlayer = true;


            }

            else
            {
                Debug.DrawRay(transform.position - offset, Vector2.right * dist, Color.pink);
                attackPlayer = false;
            }
        }


        void DetectPlayer()
        {
            Vector2 lookDir = (player.transform.position - transform.position).normalized;

            RaycastHit2D see = Physics2D.Raycast(transform.position, lookDir, 10f, LayerMask.GetMask("player") | LayerMask.GetMask("floor"));


            if (see)
            {
                if (see.transform.gameObject.layer == 7)
                {
                    Debug.DrawRay(transform.position, lookDir * 10f, Color.green);
                    seePlayer = true;

                }


            }

            else
            {

                Debug.DrawRay(transform.position, lookDir * 10f, Color.red);
                seePlayer = false;

            }

        }

        public IEnumerator Attackreset()
        {
            yield return new WaitForSeconds(1f);
            print("reset attack");
            attackReset = false;
        }

        /*public IEnumerator LeaveEnemyBlockOrParry()
        {
            yield return new WaitForSeconds(0.1f);
            print("leave block or parry");
            blockEnemy = false;
            parryEnemy = false;
        }*/

        public IEnumerator ParryStun()
        {
            yield return new WaitForSeconds(0.5f);

            parryStunEnemy = false;
        }
        void EnemyDie()
        { 
            if (enemyHealth <= minEnemyHealth)
            {
                Destroy(gameObject);
            }
        }
    }
}
