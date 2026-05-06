using UnityEngine;

namespace Enemy
{
    public class EnemyBlockState : EnemyState
    {
        public EnemyBlockState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
        {
        }
        public override void Enter()
        {
            base.Enter();
            Debug.Log("block");
            enemy.blockEnemy = true;

        }

        public override void Exit()
        {
            base.Exit();
            enemy.blockEnemy = false;
            Debug.Log("leave block");
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            
            

                if (enemy.CheckForChase())
                {
                    esm.ChangeState(enemy.enemyChaseState);
                }

                if (enemy.CheckForAttack())
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