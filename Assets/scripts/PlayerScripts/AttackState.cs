using UnityEngine;


namespace Player
{
    public class AttackState : State
    {
        // constructor
        public AttackState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(player.attackPoint.position, player.attackRange, player.enemyLayer);


            foreach(Collider2D enemy in hitEnemy)
            {
                Debug.Log("hit enemy");
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
            base.LogicUpdate();

            if(player.CheckForIdle())
            {
                sm.ChangeState(player.idleState);
            }

            if (player.CheckForRun())
            {
                sm.ChangeState(player.walkingState);
            }

            if (player.CheckForJump())
            {
                sm.ChangeState(player.jumpingState);
            }

            if (player.CheckForFall())
            {
                sm.ChangeState(player.fallingState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}

