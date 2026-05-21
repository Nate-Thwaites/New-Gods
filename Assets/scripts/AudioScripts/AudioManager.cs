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

    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";


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
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MASTER, Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }
}