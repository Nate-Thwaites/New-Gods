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
        Debug.Log("Enemy Stunned");
        enemy.erb.AddForce(5 * enemy.transform.right * 0.2f, ForceMode2D.Impulse);
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
        enemy.StartCoroutine(enemy.ParryStun());

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

            
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();


    }
}