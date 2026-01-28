using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else 
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMasterVolume()
    {
        float volume = volumeSlider.value;
        Mixer.SetFloat("master", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume",volume);
    }
    
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        Mixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume",volume);
    }

    public void SetSFXVolume()
    {
        float volume = soundfxSlider.value;
        Mixer.SetFloat("sfx", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume",volume);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetMusicVolume();
    }

    public void setPlaySFXclose()
    {
        AudioManager.instance.PlaySFXclose();
    }
    public void setPlaySFXopen()
    {
        AudioManager.instance.PlaySFXopen();
    }
}
