using Enemy;
using UnityEngine;

public class EnemyParryStunState : EnemyState
{
    public EnemyParryStunState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
    {
    }
    

    public override void Enter()
    {
        
        base.Enter();
        enemy.parryStunEnemy = true;
        enemy.stunned = true;

        enemy.StartCoroutine(enemy.ParryStun());
        
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
        base.LogicUpdate();


        enemy.erb.linearVelocity = Vector2.zero;

        if (enemy.CheckForPostureStun())
        {
            esm.ChangeState(enemy.enemyPostureStunState);
        }

        if (!enemy.parryStunEnemy)
        {
            if (enemy.CheckForAttack())
            {
                esm.ChangeState(enemy.enemyAttackState);
            }

            if (enemy.CheckForIdle())
            {
                esm.ChangeState(enemy.enemyIdleState);
            }
            
            if (enemy.CheckForChase())
            {
                esm.ChangeState(enemy.enemyChaseState);
            }

            if(enemy.CheckForBlock())
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