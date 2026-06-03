using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSliderScript : MonoBehaviour
{
    [SerializeField] Slider masterVolumeSlider;
    void Awake()
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
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }

    private void SaveMasterVolume()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }
}
