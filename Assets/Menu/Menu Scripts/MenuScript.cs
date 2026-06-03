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

    public AudioManager am;

    public static float masterVolume = 0.5f;


    void Start()
    {
        am = AudioManager.instance.GetComponent<AudioManager>();

        print("master vol= " + masterVolume);

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
        am.PlaySFXClip(5);
    }

    #endregion click anywhere to start

    #region main menu

    public void NewGame()
    {
        am.PlaySFXClip(5);
        SceneManager.LoadSceneAsync("Game");
    }

    public void OpenSettings()
    {
        am.PlaySFXClip(5);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        musicButton.Select();
    }

    #endregion main menu

    #region settings menu

    public void OpenVolumeMenu()
    {
        am.PlaySFXClip(5);
        settingsMenu.SetActive(false);
        volumeMenu.SetActive(true);
        masterVolumeSlider.Select();
    }

    public void OpenKeyboardControlMenu()
    {
        am.PlaySFXClip(5);
        settingsMenu.SetActive(false);
        keyboardControlMenu.SetActive(true);
        moveButton.Select();
    }

    public void OpenGamepadControlMenu()
    {
        am.PlaySFXClip(5);
        settingsMenu.SetActive(false);
        gamepadControlMenu.SetActive(true);
        jumpButton.Select();
    }

    public void BackToSettingsMenu()
    {
        am.PlaySFXClip(5);
        settingsMenu.SetActive(true);
        keyboardControlMenu.SetActive(false);
        gamepadControlMenu.SetActive(false);
        volumeMenu.SetActive(false);
        musicButton.Select();
    }

    public void BackToMenu()
    {
        am.PlaySFXClip(5);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        newGameButton.Select();
    }
    #endregion settings menu 

    #region quit
    public void QuitGame()
    {
        am.PlaySFXClip(5);
        Application.Quit();
    }
    #endregion quit

    public void PlayNoise()
    {
        am.PlaySFXClip(5);
    }
}
