using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSliderScript : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    public const string SFX_KEY = "SFXVolume";
    void Awake()
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
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    private void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
