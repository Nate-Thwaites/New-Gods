using UnityEngine;
using UnityEngine.UI;

public class MusicSliderScript : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    void Awake()
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
        
        SaveMusicVolume();
    }

    private void LoadMusicVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

}
