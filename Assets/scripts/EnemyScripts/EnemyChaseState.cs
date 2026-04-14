using UnityEngine;

namespace Enemy
{
    public class EnemyChaseState : EnemyState
    {
        public EnemyChaseState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log("chase");
        }

        public override void Exit()
        {
            enemy.erb.linearVelocity = new Vector2(0, enemy.erb.linearVelocity.y);
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            enemy.erb.linearVelocity = new Vector2(7 * enemy.enemyMoveDir, enemy.erb.linearVelocity.y);

            if(enemy.CheckForIdle())
            {
                esm.ChangeState(enemy.enemyIdleState);
            }

            if(enemy.CheckForAttack())
            {
                esm.ChangeState(enemy.enemyAttackState);
            }


        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
    }
}
