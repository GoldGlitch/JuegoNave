using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musciSlider;
    [SerializeField] Slider sfxSlider;

    public const string MIXER_MUSIC = "MusicVolumen";
    public const string MIXER_SFX = "SfxVolumen";

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY,musciSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }


    private void Start()
    {
        musciSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }
    void Awake()
    {
        musciSlider.onValueChanged.AddListener(SetMusicVolumen);
        sfxSlider.onValueChanged.AddListener(SetSFXVolumen);

    }

    void SetMusicVolumen(float value)
    {
        mixer.SetFloat(MIXER_MUSIC,Mathf.Log10(value)*20);

    }
    void SetSFXVolumen(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);

    }
}
