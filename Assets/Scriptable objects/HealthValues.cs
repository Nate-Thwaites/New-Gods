using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthValues", menuName = "Scriptable Objects/HealthValues", order = 1)]
public class HealthValues : ScriptableObject
{
    public int maxPlayerHealth = 100;
    public int maxEnemyHealth = 100;
    public int minPlayerHealth = 0;
    public int minEnemyHealth = 0;
    public int playerHealth;
    public int enemyHealth;
    
    private void OnEnable()
    {
        playerHealth = maxPlayerHealth;
        enemyHealth = maxEnemyHealth;
    }
}
