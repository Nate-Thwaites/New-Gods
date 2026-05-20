using Enemy;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;

public class HealthScript : MonoBehaviour
{
    GameObject enemy;
    public static HealthScript instance;
    //public GameObject player;
    //PlayerScript playerScript;
    public int maxPlayerHealth = 100;

    public int minPlayerHealth = 0;

    public int playerHealth;

    public int maxEnemyHealth = 100;

    public int minEnemyHealth = 0;

    public int enemyHealth;

    public EnemyHealthBar enemyHealthBar;
    public PlayerHealthBar playerHealthBar;
    private void Start()
    {
        //player = GameObject.Find("player");
        //playerScript = player.GetComponent<PlayerScript>();
        enemy = GameObject.Find("enemy");
        playerHealth = maxPlayerHealth;
        //print("player = " + player);
        enemyHealth = maxEnemyHealth;

        enemyHealthBar.SetMaxHealth(maxEnemyHealth);
        playerHealthBar.SetMaxHealth(maxPlayerHealth);

    }

    private void Update()
    {
        enemyHealthBar.UpdateHealthBar(enemyHealth);
        playerHealthBar.UpdateHealthBar(playerHealth);

        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

        if (playerHealth <= minPlayerHealth)
        {
            SceneManager.LoadSceneAsync("Game");
        }

        if (enemyHealth <= minEnemyHealth)
        {
            print("destroy enemy");
            Destroy(enemy);
        }
    }

    public IEnumerator AttackDelay(EnemyScript enemy)
    {

        yield return new WaitForSeconds(0.2f);
        //hitEnemy = true;
        if (!enemy.blockEnemy && !enemy.parryEnemy)
        {
            print("damage");
            enemyHealth -= LevelManager.instance.playerScript.attackDamage;
        }
    }
}
