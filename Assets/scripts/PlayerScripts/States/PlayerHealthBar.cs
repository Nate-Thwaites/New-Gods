using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerScript playerScript;
    public GameObject player;
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        
    }
    public void UpdateHealthBar(float health)
    {
        slider.value = health;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
