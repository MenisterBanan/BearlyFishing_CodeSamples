using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsMainMenu : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SoundSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }

    }
    public void SetMusicVolume()
    {
        float musicVolume = MusicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

    }

    void LoadVolume()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume();

    }

}
