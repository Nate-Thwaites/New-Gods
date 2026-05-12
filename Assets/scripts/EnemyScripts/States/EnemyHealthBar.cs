using Enemy;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public Slider slider;
    public EnemyScript enemyScript;
    public GameObject enemy;
    void Start()
    {
        enemyScript = enemy.GetComponent<EnemyScript>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;

    }
    public void UpdateHealthBar(int health)
    {
        slider.value = health;
    }
}
