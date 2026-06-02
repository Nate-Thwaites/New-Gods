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

            player.anim.Play("Block", 0);
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

            /*player.enemy = player.enemy.GetComponent<EnemyScript>();
            if (player.enemy.hitPlayer && player.parryTimer > 0)
            {
                Debug.Log("Parried attack");
                player.playerParry = true;
                player.enemy.parryStunEnemy = true;
            }


            if (player.playerParry)
            {
                player.enemy.ParryPostureDamage();
            }*/

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            EnemyScript parriedEnemy = null;

            foreach (GameObject e in enemies)
            {
                if (e == null) continue;
                EnemyScript enemyScript = e.GetComponent<EnemyScript>();
                if (enemyScript == null) continue;

                if (enemyScript.hitPlayer && player.parryTimer > 0)
                {
                    player.audioSource.PlayOneShot(player.am.SFXClips[1]);

                    enemyScript.enemyDamage = 0;
                    player.playerParry = true;
                    player.ParryParticle();
                    enemyScript.parryStunEnemy = true;
                    
                    player.isBlocking = false;

                    if (enemyScript.CheckForParryStun())
                    {
                        enemyScript.esm.ChangeState(enemyScript.enemyParryStunState);
                    }
                    // remember which enemy was parried so we only apply posture damage once
                    parriedEnemy = enemyScript;
                    break;
                }
            }

            if (player.playerParry && parriedEnemy != null)
            {
                parriedEnemy.ParryPostureDamage();
            }
        }

        

        public void DetectBlock()
        {
               GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
               foreach (GameObject e in enemy)
               {
                   EnemyScript enemyScript = e.GetComponent<EnemyScript>();

                   if (enemyScript.hitPlayer && player.isBlocking)
                   {
                       player.isBlocking = false;
                       enemyScript.hitPlayer = false;

                       if (!player.playerParry)
                       {
                           player.audioSource.PlayOneShot(player.am.SFXClips[2]);
                           player.posture.posture += 15;
                       }
                   }
               }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        
       

    }
}
