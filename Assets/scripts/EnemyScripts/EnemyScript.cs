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
        public EnemyPostureStunState enemyPostureStunState;
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
        public bool leavePostureStunEnemy;
        [Space(10)]

        #endregion stun variables

        #region UI variables

        [Header("UI variables")]
        public GameObject enemyHealthCanvas;
        public TMPro.TextMeshProUGUI enemyStateText;

        public EnemyPostureBar enemyPostureBar;


        [Space(10)]

        #endregion UI variables

        #region health variables

        [Header("Health variables")]
        
        [Space(10)]

        #endregion health variables

        #region enemy attack variables

        [Header("enemy attack variables")]
        public float enemyAttackTimer = 1.5f;
        public float enemyAttackCompleteTimer = 0.5f;
        public Transform enemyAttackPoint;
        public int maxEnemyAttackNum = 3;
        public float enemyAttackRange = 5f;
        public bool attackReset;
        public bool hitPlayer = false;
        public int enemyDamage;

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

        #region enemy posture variables

        [Header("Enemy posture variables")]
        public float maxEnemyPosture = 100;
        public float minEnemyPosture = 0;
        public float enemyPosture;

        #endregion enemy posture variables

        #endregion variables

        void Start()
        {
            //enemyPostureBar.SetMaxPosture(maxEnemyPosture);
            enemyPostureBar.SetMaxPosture(maxEnemyPosture);


            erb = GetComponent<Rigidbody2D>();
            esm = gameObject.AddComponent<EnemyStateMachine>();

            enemyIdleState = new EnemyIdleState(this, esm);
            enemyChaseState = new EnemyChaseState(this, esm);
            enemyAttackState = new EnemyAttackState(this, esm);
            enemyParryStunState = new EnemyParryStunState(this, esm);
            enemyBlockState = new EnemyBlockState(this, esm);
            enemyParryState = new EnemyParryState(this, esm);
            enemyPostureStunState = new EnemyPostureStunState(this, esm);

            player = GameObject.Find("player");
            playerScript = player.GetComponent<PlayerScript>();
            

            esm.Init(enemyIdleState);


        }

        // Update is called once per frame
        void Update()
        {
            DetectPlayer();
            DetectAttackPlayer();
            

            
            enemyPostureBar.UpdatePostureBar(enemyPosture);

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

            return false;
        }

        public bool CheckForParry()
        {
            if (blockOrParryChance <= 90 && blockOrParryChance > 50 && blockOrParryChance > 0 && playerScript.hitEnemy)
            {
                return true;
            }

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

        public bool CheckForPostureStun()
        {
            if (enemyPosture >= maxEnemyPosture)
            {
                print("posture break");
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

        public IEnumerator LeaveEnemyBlock()
        {
            yield return new WaitForSeconds(0.2f);
            print("leave block");
            blockEnemy = false;
        }

        public IEnumerator LeaveEnemyParry()
        {
            yield return new WaitForSeconds(0.2f);
            print("leave parry");
            parryEnemy = false;
        }

        public IEnumerator ParryStun()
        {
            yield return new WaitForSeconds(0.5f);

            parryStunEnemy = false;
        }

        public IEnumerator PostureBreakStun()
        {
            yield return new WaitForSeconds(3f);
            enemyPosture = minEnemyPosture;

            postureBreakStunEnemy = false;
        }

        public IEnumerator LeavePostureStun()
        {
            yield return new WaitForSeconds(0.5f);

            leavePostureStunEnemy = true;
        }
        

        public void TakeDamage(HealthScript health)
        {
            health.playerHealth -= enemyDamage;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(enemyAttackPoint.position, enemyAttackRange);
        }
    }
}
