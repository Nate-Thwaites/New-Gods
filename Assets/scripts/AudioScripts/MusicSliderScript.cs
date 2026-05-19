using UnityEngine;
using UnityEngine.UI;

public class MusicSliderScript : MonoBehaviour
{
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
