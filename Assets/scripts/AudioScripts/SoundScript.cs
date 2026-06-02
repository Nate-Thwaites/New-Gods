using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundScript : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public const string MIXER_MASTER = "Master";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";


    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(AudioManager.masterVolume, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.musicVolume, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.sfxVolume, 1f);

        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.masterVolume, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.musicVolume, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.sfxVolume, sfxSlider.value);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        Debug.Log("Master volume set to: " + value);    
    }
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        Debug.Log("Music volume set to: " + value);

    }
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
                Debug.Log("SFX volume set to: " + value);
    }
}