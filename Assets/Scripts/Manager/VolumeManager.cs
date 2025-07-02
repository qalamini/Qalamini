using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private AudioMixer audioMixer;

    void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        float savedSfxVolume = PlayerPrefs.GetFloat("sfxVolume", 0.5f);

        // Slider value
        musicSlider.value = savedMusicVolume;
        sfxSlider.value = savedSfxVolume;

        SetMusicVolume(savedMusicVolume);
        SetSfxVolume(savedSfxVolume);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == 0) audioMixer.SetFloat("MusicVolume", -80);
        else audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float volume)
    {
        if (volume == 0) audioMixer.SetFloat("SFXVolume", -80);
        else audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("sfxVolume", volume);
        PlayerPrefs.Save();
    }
}

