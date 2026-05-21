using Player;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public Transform player;
    public Transform respawnPoint;
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Respawn"))
        {
            print("damage taken");
            player.position = respawnPoint.position;
            LevelManager.instance.playerScript.RespawnDamage();
            
        }
    }
}
