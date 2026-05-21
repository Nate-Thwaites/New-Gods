using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSliderScript : MonoBehaviour
{
    [SerializeField] Slider masterVolumeSlider;
    void Start()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.SetFloat("MasterVolume", 1);
            LoadMasterVolume();
        }

        else
        {
            LoadMasterVolume();
        }
    }

    public void ChangeMasterVolume()
    {
        AudioListener.volume = masterVolumeSlider.value;
        SaveMasterVolume();
    }

    private void LoadMasterVolume()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
    }

    private void SaveMasterVolume()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
    }
}
