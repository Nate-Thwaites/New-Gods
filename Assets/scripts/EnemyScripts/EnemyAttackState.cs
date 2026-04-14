using Player;
using UnityEngine;

namespace Enemy
{
    public enum EnemyAttackType
    {
        SwipeLeft,
        SwipeRight,
        SwipeUp,
        SwipeDown
    }
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackType enemyAttackNum;

        public EnemyAttackState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("enemy attack");

            enemy.seePlayer = false;
        }

        void EnemyAttack()
        {
            enemy.enemyAttackTimer = 1.5f;
            enemy.enemyAttackCompleteTimer = 1f;
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemy.enemyAttackPoint.position, enemy.enemyAttackRange, enemy.playerLayer);
            foreach (Collider2D player in hitPlayer)
            {
                Debug.Log("hit player");
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            EnemyAttack();
            base.LogicUpdate();

            if(enemy.CheckForIdle())
            {
                esm.ChangeState(enemy.enemyIdleState);
            }


            if (enemy.CheckForChase())
            {
                esm.ChangeState(enemy.enemyChaseState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}