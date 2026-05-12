using Enemy;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPostureBar : MonoBehaviour
{
    public Slider slider;
    public EnemyScript enemyScript;
    public GameObject enemy;
    void Start()
    {
        enemyScript = enemy.GetComponent<EnemyScript>();
    }

    public void SetMaxPosture(float posture)
    {
        slider.maxValue = posture;

    }
    public void UpdatePostureBar(float posture)
    {
        slider.value = posture;
    }
}