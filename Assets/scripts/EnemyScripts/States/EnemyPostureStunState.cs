using UnityEngine;
namespace Enemy
{
    public class EnemyPostureStunState : EnemyState
    {
        public EnemyPostureStunState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }
        public bool attackLanded;
        //public float knockbackTime;
        public override void Enter()
        {
            base.Enter();
            enemy.playerScript.hitEnemy = false;
            enemy.StartCoroutine(enemy.PostureBreakStun());

            enemy.playerScript.attackDamage *= 3f;

            attackLanded = false;
            enemy.parryStunEnemy = false;
            enemy.stunned = true;

            //knockbackTime = 0.02f;
        }

        public override void Exit()
        {
            base.Exit();
            enemy.posture.posture = enemy.posture.minPosture;
            //enemy.playerScript.attackDamage = ;

        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            



            
            if (enemy.playerScript.hitEnemy)
            {
                //enemy.erb.AddForce(5f * enemy.enemyFacingDir * enemy.transform.right, ForceMode2D.Impulse);
                Debug.Log(enemy.playerScript.attackDamage);
                attackLanded = true;
                enemy.StopCoroutine(enemy.PostureBreakStun());
                enemy.postureBreakStunEnemy = false;

                
                //knockbackTime -= Time.deltaTime;

                /*if (knockbackTime <= 0)
                {
                    enemy.playerScript.hitEnemy = false;
                    knockbackTime = 0.02f;
                    //enemy.StopCoroutine(enemy.PostureBreakStun());
                }*/

                   
            }
           
            

            if (!enemy.postureBreakStunEnemy || enemy.playerScript.hitEnemy)
            {
                enemy.playerScript.attackDamage /= 3f;
                
                if (enemy.CheckForChase())
                {
                    esm.ChangeState(enemy.enemyChaseState);
                }



                if (enemy.CheckForAttack())
                { 
                    esm.ChangeState(enemy.enemyAttackState);
                }

                if (enemy.CheckForParryStun())
                {
                    esm.ChangeState(enemy.enemyParryStunState);
                }

                if (enemy.CheckForBlock())
                {
                    esm.ChangeState(enemy.enemyBlockState);
                }

                if (enemy.CheckForParry())
                {
                    esm.ChangeState(enemy.enemyParryState);
                }
                
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}
