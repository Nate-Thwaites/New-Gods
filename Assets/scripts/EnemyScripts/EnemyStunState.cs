using Enemy;
using UnityEngine;

public class EnemyStunState : EnemyState
{
    public EnemyStunState(EnemyScript enemy, EnemyStateMachine esm) : base(enemy, esm)
    {
    }
    

    public override void Enter()
    {
        base.Enter();
        
        enemy.erb.AddForce(5 * enemy.transform.right * 0.5f, ForceMode2D.Impulse);
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