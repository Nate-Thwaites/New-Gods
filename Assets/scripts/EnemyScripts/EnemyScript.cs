using Player;
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

        #region scriptable object variables

        [Header("Scriptable Object variables")]

        public HealthValues healthValues;

        [Space(10)]

        #endregion scriptable object variables

        #region StateMachine variables

        [Header("StateMachine variables")]
        public EnemyIdleState enemyIdleState;
        public EnemyChaseState enemyChaseState;
        public EnemyAttackState enemyAttackState;   
        public EnemyStateMachine esm;
        [Space(10)]

        #endregion StateMachine variables

        #region UI variables

        [Header("UI variables")]
        public TMPro.TextMeshProUGUI enemyStateText;
        [Space(10)]

        #endregion UI variables

        #region enemy attack variables

        [Header("enemy attack variables")]
        public float enemyAttackTimer = 1.5f;
        public float enemyAttackCompleteTimer = 0.5f;
        public Transform enemyAttackPoint;
        public int maxEnemyAttackNum = 3;
        public float enemyAttackRange = 0.5f;
        public bool hitPlayer;

        [Space(10)]

        #endregion eneny attack variables

        #region player detecting variables

        [Header("Player detecting variables")]
        public bool seePlayer;
        public bool attackPlayer;   
        public Transform player;
        public LayerMask playerLayer;
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
            erb = GetComponent<Rigidbody2D>();
            esm = gameObject.AddComponent<EnemyStateMachine>();

            enemyIdleState = new EnemyIdleState(this, esm);
            enemyChaseState = new EnemyChaseState(this, esm);
            enemyAttackState = new EnemyAttackState(this, esm);

            


            esm.Init(enemyIdleState);


        }

        // Update is called once per frame
        void Update()
        {
            DetectPlayer();
            DetectAttackPlayer();
            enemyStateText.text = "State: " + esm.CurrentState;

            if(player.transform.position.x < transform.position.x)
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

            print("player health = " + healthValues.playerHealth);

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
                if (enemyAttackTimer >= 0)
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
            Vector2 lookDir = (player.position - transform.position).normalized;

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
    }
}
