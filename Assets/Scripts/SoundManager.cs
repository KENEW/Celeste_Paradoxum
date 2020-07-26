using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource BGMSource;
    public AudioSource SFXSource;

    private float defBgmvolume = 0.0f;
    private float defSfxvolume = 0.0f;

    private void Awake()
    {
        defBgmvolume = BGMSource.volume;
        defSfxvolume = SFXSource.volume;

        BGMSource.volume = defBgmvolume * PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        SFXSource.volume = defSfxvolume * PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    }

    private void Update()
    {
        BGMSource.enabled = (PlayerPrefs.GetInt("IsBGMMute", 0) == 0);
        SFXSource.enabled = (PlayerPrefs.GetInt("IsSFXMute", 0) == 0);
    }

    public void SetBGMVolume(float volume)
    {
        BGMSource.volume = defBgmvolume * volume;
    }

    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = defSfxvolume * volume;
    }

    public void PlayBGM(AudioClip clip, float volume = 1.0f)
    {
        BGMSource.loop = true;
        BGMSource.PlayOneShot(clip, volume);
    }

    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        SFXSource.PlayOneShot(clip, volume);
    }
}