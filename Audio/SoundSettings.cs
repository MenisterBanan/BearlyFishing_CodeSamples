using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SoundSlider;
    [SerializeField] AudioMixer myMixer;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSoundVolume();
        }

    }
    public void SetMusicVolume()
    {
        float musicVolume = MusicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(musicVolume)*20);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

    }

    void LoadVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SoundSlider.value = PlayerPrefs.GetFloat("SoundVolume");

        SetMusicVolume();
        SetSoundVolume();
    }
    public void SetSoundVolume()
    {
        float soundVolume = SoundSlider.value;
        myMixer.SetFloat("Sound", Mathf.Log10(soundVolume)*20);
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);


    }

}
