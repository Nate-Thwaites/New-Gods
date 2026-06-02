using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSliderScript : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    public const string SFX_KEY = "SFXVolume";
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
