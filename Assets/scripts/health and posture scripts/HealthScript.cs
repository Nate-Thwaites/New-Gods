using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health;
    public int maxHealth = 100;
    public int minHealth = 0;


    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    
}
