using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource ShotSource;
    [SerializeField] List<AudioClip> eatClip = new List<AudioClip>();

    public const string MUSIC_KEY = "musicVolumen";
    public const string SFX_KEY = "sfxVolumen";

    private void Awake()
    {
           if ( instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolume();
    }

    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY,1F);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1F);

        mixer.SetFloat(Volume.MIXER_MUSIC,Mathf.Log10(musicVolume)* 20);
        mixer.SetFloat(Volume.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }
    public void ShotSFX()
    {
        AudioClip clip = eatClip[Random.Range(0, eatClip.Count)];
        ShotSource.PlayOneShot(clip);
    }
}
