using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SoundSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource ReelingSource;
    [Header("--- Audio Clips ---")]
    public AudioClip Ambience;
    public AudioClip Music;
    public AudioClip Casting;
    public AudioClip FishOnHook;
    public AudioClip Reeling;
    public AudioClip MinigameCatch;
    public AudioClip MinigameFail;

    void Start()
    {
        MusicSource.loop = true;
        SoundSource.loop = true;
        MusicSource.clip = Music;
        SoundSource.clip = Ambience;
        SoundSource.Play();
        MusicSource.Play();
        ReelingSource.clip = Reeling;
    }

    public void PlayReelingSound()
    {

        ReelingSource.Play();

    }

    public void StopReelingSound()
    {
        ReelingSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
