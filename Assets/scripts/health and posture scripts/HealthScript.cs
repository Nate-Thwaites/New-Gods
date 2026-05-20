using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
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
        playerHealth = maxPlayerHealth;

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
            Destroy(gameObject);
        }
    }

    
}
