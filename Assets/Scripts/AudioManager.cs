using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioMixer audioMixer;
    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;
    public float fadeDuration = 1.0f; // Duration for fade out

    public int poolSize = 10;
    private List<AudioSource> sfxSources;
    private int currentSFXIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeSFXPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSFXPool()
    {
        sfxSources = new List<AudioSource>();
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0]; // Assign to SFX group in Audio Mixer
            sfxSources.Add(newSource);
        }
    }

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

    public bool IsMusicPlaying(string name)
    {
        if (musicSource.clip != null && musicSource.clip.name == name && musicSource.isPlaying)
        {
            return true;
        }
        return false;
    }

    public void PlaySFX(string name, bool loop = false)
    {
        if (sfxSources.Count == 0)
        {
            Debug.LogWarning("No SFX sources available");
            return;
        }

        AudioClip clip = GetClipFromName(name, sfxClips);
        if (clip != null)
        {
            AudioSource sfxSource = GetAvailableSFXSource();
            sfxSource.clip = clip;
            sfxSource.loop = loop;
            sfxSource.Play();
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + name);
        }
    }

    private AudioSource GetAvailableSFXSource()
    {
        // Rotate through the pool to ensure each AudioSource gets used in turn
        if (sfxSources.Count == 0)
        {
            Debug.LogWarning("SFX source pool is empty");
            return null;
        }

        currentSFXIndex = (currentSFXIndex + 1) % sfxSources.Count;
        return sfxSources[currentSFXIndex];
    }

    public void StopSFX()
    {
        foreach (AudioSource sfxSource in sfxSources)
        {
            sfxSource.Stop();
            sfxSource.loop = false;
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = musicSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = 0;
        musicSource.Stop();
        musicSource.volume = startVolume; // Reset volume after stopping
    }

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
