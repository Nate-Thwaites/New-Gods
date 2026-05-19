using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject clickAnywhereToStart;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject keyboardControlMenu;
    public GameObject gamepadControlMenu;

    [SerializeField] Slider musicSlider;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region click anywhere to start

    public void ClickToStart()
    {
        clickAnywhereToStart.SetActive(false);
        mainMenu.SetActive(true);   
    }

    #endregion click anywhere to start

    #region main menu

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    #endregion main menu

    #region settings menu

    

    public void OpenKeyboardControlMenu()
    {
        settingsMenu.SetActive(false);
        keyboardControlMenu.SetActive(true);
    }

    public void OpenGamepadControlMenu()
    {
        settingsMenu.SetActive(false);
        gamepadControlMenu.SetActive(true);
    }

    public void BackToSettingsMenu()
    {
        settingsMenu.SetActive(true);
        keyboardControlMenu.SetActive(false);
        gamepadControlMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    #endregion settings menu 

    #region quit
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion quit

}
