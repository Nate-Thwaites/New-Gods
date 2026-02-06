using NUnit.Framework;
using Player;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

namespace Enemy
{
     

    public class EnemyScript : MonoBehaviour
    {
        public Rigidbody2D erb;
        
        public bool seePlayer;
        public Transform player;

       

        void Start()
        {
            erb = GetComponent<Rigidbody2D>();

            
            
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 lookDir = (player.position - transform.position).normalized;

            RaycastHit2D see = Physics2D.Raycast(transform.position, lookDir, 10f, LayerMask.GetMask("player") | LayerMask.GetMask("floor"));

            List<int> enemyVision = new List<int>();

            enemyVision.Add(see.transform.gameObject.layer);

            foreach (LayerMask obj in enemyVision)
            {
                Debug.Log(obj);
                
            }



            if (enemyVision[0] == 7)
            {
                Debug.DrawRay(transform.position, lookDir * 10f, Color.green);
                seePlayer = true;
                
            }

            else
            {
                Debug.DrawRay(transform.position, lookDir * 10f, Color.red);
                seePlayer = false;
            }

            enemyVision.Clear();
        }
   

        public void CheckForIdle()
        {
            if (!seePlayer)
            {
                print("idle");
            }

        }

        
    }

}