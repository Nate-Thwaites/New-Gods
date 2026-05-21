using UnityEngine;
using UnityEngine.UI;

public class SFXSliderScript : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    void Start()
    {
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
            LoadSFXVolume();
        }

        else
        {
            LoadSFXVolume();
        }
    }

    public void ChangeSFXVolume()
    {
        AudioListener.volume = sfxSlider.value;
        SaveSFXVolume();
    }

    private void LoadSFXVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }
}
