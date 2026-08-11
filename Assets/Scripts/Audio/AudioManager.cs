using UnityEngine;

public class AudioManager : MonoBehaviour
{
     public static AudioManager Instance;

    [Header("Audio Components")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        // Plays the sound effect once
        sfxSource.PlayOneShot(sfxClip);
        
    }

    public void StopMusic()
    {
        musicSource?.Stop();
    }
}
