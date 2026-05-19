using Enemy;
using JetBrains.Annotations;
using System;
using UnityEngine;


namespace Player
{
    public class BlockingState : State
    {


       
        // constructor
        public BlockingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {  
        }

        public override void Enter()
        {
            base.Enter();

            player.rb.linearVelocity = Vector2.zero;


        }

        public override void Exit()
        {
            base.Exit();
            
            player.isBlocking = false;
            player.parryTimer = 0.18f;
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {

            
            DetectParry();
            DetectBlock();

           /* if (player.parryTimer > 0 && enemy.hitPlayer)
            {

                Debug.Log("Parried attack");
                player.playerParry = true;
                player.enemy.parryStunEnemy = true;
                if (player.playerParry)
                {
                    player.enemy.enemyPosture += 10;
                }
            }*/


       /*     if (enemy.hitPlayer && player.isBlocking)
            {
               player.isBlocking = false;

                if(!player.playerParry)
                {
                    player.playerPosture += 10;
                }
            }*/

            




            player.parryTimer -= Time.deltaTime;
            
            base.LogicUpdate();
            if (!player.CheckForBlock())
            {
                player.StartCoroutine(player.LeaveParry());

                if (player.CheckForIdle())
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

                if (player.CheckForAttack())
                {
                    sm.ChangeState(player.attackState);
                }

                if (player.CheckForPostureStun())
                {
                    sm.ChangeState(player.postureStunState);
                }

            }
        }

      

        public void DetectParry()
        {


            EnemyScript enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();

            //enemy.GetComponent<EnemyScript>()

            if (player.parryTimer > 0 && enemy.hitPlayer)
            {

                Debug.Log("Parried attack");
                player.playerParry = true;
                enemy.parryStunEnemy = true;
                if (player.playerParry)
                {
                    enemy.enemyPosture += 10;
                }
            }
        }

        public void DetectBlock()
        {
            EnemyScript enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyScript>();
            //make array and use foreach loop to check for multiple enemies later
            if (enemy.hitPlayer && player.isBlocking)
            {
                player.isBlocking = false;
                if (!player.playerParry)
                {
                    player.playerPosture += 10;
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();


        }
        
       

    }
}
