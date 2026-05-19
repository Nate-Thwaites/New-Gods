using Player;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    PlayerScript player;

    public GameObject pauseMenuUI;

    private void Start()
    {
        player = player.GetComponent<PlayerScript>();
    }

   
  
    void Update()
    {
        if(player.pauseAction.WasPressedThisFrame())
        {
            if(gameIsPaused )
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }
}
