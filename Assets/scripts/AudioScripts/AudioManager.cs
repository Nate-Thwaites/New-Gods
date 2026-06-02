using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static bool isGameEnding = false;

    [SerializeField] AudioMixer mixer;

    public AudioClip[] musicClips;
    public AudioClip[] SFXClips;
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    public const string masterVolume = "Master";
    public const string musicVolume = "MusicVolume";
    public const string sfxVolume = "SFXVolume";


    private void Awake()
    {
        isGameEnding = false;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            print("do not destroy");

            LoadVolume();
            PlayMusicClip(0);
        }
        else
        {
            print("do destroy");
            Destroy(gameObject);
        }
    }

    public void PlaySFXClip(int clipNumber)
    {
        sfxAudioSource.enabled = true;
        sfxAudioSource.PlayOneShot(SFXClips[clipNumber]); // start clip  
    }
    public void PlayMusicClip(int clipNumber)
    {
        // Stop any currently playing music first
        musicAudioSource.Stop();

        musicAudioSource.enabled = true;
        musicAudioSource.clip = musicClips[clipNumber];
        musicAudioSource.Play();
    }

    public void StopSFXClip()
    {
        sfxAudioSource.Stop(); //stop currently playing clip  
    }
    public void StopMusicClip()
    {
        musicAudioSource.Stop(); //stop currently playing clip  
    }

 

    void LoadVolume()
    {
        float masterVolumeValue = PlayerPrefs.GetFloat(masterVolume, 1f);
        float musicVolumeValue = PlayerPrefs.GetFloat(musicVolume, 1f);
        float sfxVolumeValue = PlayerPrefs.GetFloat(sfxVolume, 1f);

        float safeMaster = Mathf.Max(masterVolumeValue, 0.0001f);
        float safeMusic = Mathf.Max(musicVolumeValue, 0.0001f);
        float safeSfx = Mathf.Max(sfxVolumeValue, 0.0001f);

        mixer.SetFloat(SoundScript.MIXER_MASTER, Mathf.Log10(safeMaster) * 20);
        mixer.SetFloat(SoundScript.MIXER_MUSIC, Mathf.Log10(safeMusic) * 20);
        mixer.SetFloat(SoundScript.MIXER_SFX, Mathf.Log10(safeSfx) * 20);
    }
}