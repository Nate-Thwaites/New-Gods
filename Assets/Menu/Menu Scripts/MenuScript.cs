using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject clickAnywhereToStart;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject keyboardControlMenu;
    public GameObject gamepadControlMenu;
    public GameObject volumeMenu;
    public Button newGameButton;
    public Button musicButton;
    public Slider masterVolumeSlider;
    public Button moveButton;
    public Button jumpButton;




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //OnMouseOver();
    }

    /*private void OnMouseOver()
    {
        Button button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        button.Select();
        print("mouse over " + button);
    }*/

    #region click anywhere to start

    public void ClickToStart()
    {
        clickAnywhereToStart.SetActive(false);
        mainMenu.SetActive(true);   
        newGameButton.Select();
        
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
        musicButton.Select();
    }

    #endregion main menu

    #region settings menu

    public void OpenVolumeMenu()
    {
        settingsMenu.SetActive(false);
        volumeMenu.SetActive(true);
        masterVolumeSlider.Select();
    }

    public void OpenKeyboardControlMenu()
    {
        settingsMenu.SetActive(false);
        keyboardControlMenu.SetActive(true);
        moveButton.Select();
    }

    public void OpenGamepadControlMenu()
    {
        settingsMenu.SetActive(false);
        gamepadControlMenu.SetActive(true);
        jumpButton.Select();
    }

    public void BackToSettingsMenu()
    {
        settingsMenu.SetActive(true);
        keyboardControlMenu.SetActive(false);
        gamepadControlMenu.SetActive(false);
        volumeMenu.SetActive(false);
        musicButton.Select();
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        newGameButton.Select();
    }
    #endregion settings menu 

    #region quit
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion quit

}
