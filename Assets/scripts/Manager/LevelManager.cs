using UnityEngine;
using Player;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    public GameObject player;
    public PlayerScript playerScript;

    public int testHealth;

    void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        testHealth = 100;

        player = GameObject.Find("player");
        playerScript = player.GetComponent<PlayerScript>();

       

        MenuScript.masterVolume = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {

    }
}