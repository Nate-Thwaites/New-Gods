using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject clickAnywhereToStart;
    public GameObject mainMenu;
    public GameObject settingsMenu;

    [SerializeField] Slider musicSlider;


    void Start()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            LoadMusicVolume();
        }

        else
        {
            LoadMusicVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickToStart()
    {
        clickAnywhereToStart.SetActive(false);
        mainMenu.SetActive(true);   
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void ChangeMusicVolume()
    {
        AudioListener.volume = musicSlider.value;
        SaveMusicVolume();
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

}
