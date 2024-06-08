using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;

    private void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Play a specific music track
    public void PlayMusic(string name)
    {
        AudioClip clip = GetClipFromName(name, musicClips);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found: " + name);
        }
    }

    // Play a specific sound effect
    public void PlaySFX(string name)
    {
        AudioClip clip = GetClipFromName(name, sfxClips);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + name);
        }
    }

    // Stop the currently playing music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Get an AudioClip by name from an array of AudioClips
    private AudioClip GetClipFromName(string name, AudioClip[] clips)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }
}
