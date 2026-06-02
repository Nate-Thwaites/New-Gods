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
        public bool enemyIsGrounded;
        public LayerMask floor;
        [Space(10)]

        #endregion core variables

        #region health variables

        [Header("Health variables")]
        public HealthScript health;
        [Space(10)]

        #endregion health variables

        #region StateMachine variables

        [Header("StateMachine variables")]
        public EnemyIdleState enemyIdleState;
        public EnemyChaseState enemyChaseState;
        public EnemyAttackState enemyAttackState;
        public EnemyParryStunState enemyParryStunState;
        public EnemyBlockState enemyBlockState;
        public EnemyParryState enemyParryState;
        public EnemyPostureStunState enemyPostureStunState;
        public EnemyAttackStunState enemyAttackStunState;
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
        public bool attackStun;
        public float attackStunTimer;
        public bool postureBreakStunEnemy;
        public bool leavePostureStunEnemy;
        public bool stunned;
        [Space(10)]

        #endregion stun variables

        #region UI variables

        [Header("UI variables")]
        public GameObject enemyHealthCanvas;
        

        public EnemyPostureBar enemyPostureBar;
        public EnemyHealthBar enemyHealthBar;


        [Space(10)]

        #endregion UI variables

        #region enemy attack variables

        [Header("enemy attack variables")]
        public float enemyAttackTimer = 1.5f;
        public float enemyAttackCompleteTimer = 0.5f;
        public Transform enemyAttackPoint;
        public int maxEnemyAttackNum = 3;
        public float enemyAttackRange = 0.7f;
        public bool attackReset;
        public bool hitPlayer = false;
        public int enemyDamage;
        public bool leaveAttack;
        

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
        public float enemyFacingDir;

        #endregion enemy movement variables

        #region enemy posture variables

        [Header("Enemy posture variables")]
        public PostureScript posture;

        #endregion enemy posture variables

        #endregion variables

        void Start()
        {
            //enemyPostureBar.SetMaxPosture(maxEnemyPosture);
            enemyPostureBar.SetMaxPosture(posture.maxPosture);
            enemyHealthBar.SetMaxHealth(health.maxHealth);

            leaveAttack = false;
            stunned = false;

            erb = GetComponent<Rigidbody2D>();
            esm = gameObject.AddComponent<EnemyStateMachine>();
            health = health.GetComponent<HealthScript>();
            posture = posture.GetComponent<PostureScript>();

            enemyIdleState = new EnemyIdleState(this, esm);
            enemyChaseState = new EnemyChaseState(this, esm);
            enemyAttackState = new EnemyAttackState(this, esm);
            enemyParryStunState = new EnemyParryStunState(this, esm);
            enemyBlockState = new EnemyBlockState(this, esm);
            enemyParryState = new EnemyParryState(this, esm);
            enemyPostureStunState = new EnemyPostureStunState(this, esm);
            enemyAttackStunState = new EnemyAttackStunState(this, esm);

            player = GameObject.Find("player");
            playerScript = player.GetComponent<PlayerScript>();


            esm.Init(enemyIdleState);


        }

        // Update is called once per frame
        void Update()
        {
            attackStun = false;
            DetectPlayer();
            DetectAttackPlayer();
            Die();

            enemyHealthBar.UpdateHealthBar(health.health);
            enemyPostureBar.UpdatePostureBar(posture.posture);

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
            if (!stunned)
            {
                if (blockOrParryChance <= 50 && blockOrParryChance > 0 && playerScript.hitEnemy)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckForParry()
        {
            if (!stunned)
            {
                if (blockOrParryChance <= 90 && blockOrParryChance > 50 && blockOrParryChance > 0 && playerScript.hitEnemy)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckForIdle()
        {
            if (!stunned)
            {
                if (!seePlayer)
                {
                    return true;
                }
            }
            return false;

        }

        public bool CheckForChase()
        {
            if (!stunned)
            {
                if (seePlayer && !attackPlayer)
                {
                    return true;
                }
            }

            return false;
        }

       
        public bool CheckForAttack()
        {
            if (!stunned)
            {
                if (attackPlayer && enemyAttackCompleteTimer <= 0)
                {
                  

                    return true;
                }
            }


            return false;
        }

        public bool CheckForParryStun()
        {
            
            if (parryStunEnemy)
            {
                return true;
            }
            return false;
        }

        public bool CheckForPostureStun()
        {
            if (posture.posture >= posture.maxPosture)
            {
                postureBreakStunEnemy = true;

                return true;
            }
            return false;
        }

        public bool CheckForAttackStun()
        {
            if (attackStun && !postureBreakStunEnemy)
            {
                return true;
            }
            return false;
        }

        public void DetectAttackPlayer()
        {
            float dist = 1.8f;


            Vector3 offset = new Vector3(0.9f, 0.3f, 0);

            bool playerHit = Physics2D.Raycast(transform.position - offset, Vector2.right, dist, playerLayer);

            if (playerHit)
            {
                Debug.DrawRay(transform.position - offset, Vector2.right * dist, Color.blue);
                attackPlayer = true;
                erb.linearVelocity = new Vector2(0, 0);

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

            RaycastHit2D see = Physics2D.Raycast(transform.position, lookDir, 10f, LayerMask.GetMask("player"));// | LayerMask.GetMask("floor"));


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

        public void DealDamage()
        {
            if (!parryStunEnemy)
            {
                hitPlayer = true;


                if (!playerScript.CheckForBlock() || !playerScript.CheckForBlock())
                {

                    playerScript.health.health -= enemyDamage;
                    playerScript.attackStunned = true;

                    playerScript.audioSource.PlayOneShot(playerScript.am.SFXClips[4]);



                }
            }
        }



        public IEnumerator LeaveAttack()
        {
            yield return new WaitForSeconds(0.5f);
            //print("reset attack");
            leaveAttack = true;             
        }

        public IEnumerator LeaveEnemyBlock()
        {
            yield return new WaitForSeconds(0.2f);
            //print("leave block");
            blockEnemy = false;
        }

        public IEnumerator LeaveEnemyParry()
        {
            yield return new WaitForSeconds(0.9f);
            print("leave parry");
            parryEnemy = false;
        }

        public IEnumerator ParryStun()
        {
            yield return new WaitForSeconds(1f);
            stunned = false;
            parryStunEnemy = false;
        }

        public IEnumerator AttackStun()
        {
            yield return new WaitForSeconds(attackStunTimer);
            attackPlayer = false;
            stunned = false;
        }

        public IEnumerator PostureBreakStun()
        {
            yield return new WaitForSeconds(1.2f);
            posture.posture = posture.minPosture;
            stunned = false;
            postureBreakStunEnemy = false;

        }

        
        public void Die()
        {
            if (health.health <= health.minHealth)
            {
                stunned = true;
                erb.linearVelocity = Vector2.zero;
                anim.Play("Die", 0);
                
            }
        }

        public void DestroyEnemy()
        {
            Destroy(gameObject);
        }   

        public void TakeDamage()
        {
            health.health -= playerScript.attackDamage;
        }

        /*private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(enemyAttackPoint.position, enemyAttackRange);
        }*/

      /*  public IEnumerator AttackDelay( )
        {

            yield return new WaitForSeconds(0.1f);
            //hitEnemy = true;
            if (!blockEnemy && !parryEnemy)
            {
                health.health -= LevelManager.instance.playerScript.attackDamage;
            }
        }*/


        
        public void EnemyFlip()
        {
            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
            if (sr == null) return;

            if (player.transform.position.x < transform.position.x)
            {
                enemyFacingDir = 1;
                sr.flipX = true;
            }

            else
            {
                enemyFacingDir = -1;
                sr.flipX = false;
            }

        }



        public void ParryPostureDamage()
        {
            posture.posture += 25;
        }
    }
}
