using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPostureBar : MonoBehaviour
{
    public Slider slider;
    public PlayerScript playerScript;
    public GameObject player;
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
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
