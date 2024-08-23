// AudioManager.cs
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    // Global volume control for all managed audio sources
    private float globalVolume = 1f;

    // List to keep track of all registered audio sources
    private List<AudioSource> managedAudioSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            globalVolume = PlayerPrefs.GetFloat("Volume", 1f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Register a new AudioSource to be managed by this AudioManager
    public void RegisterAudioSource(AudioSource audioSource)
    {
        if (!managedAudioSources.Contains(audioSource))
        {
            managedAudioSources.Add(audioSource);
            audioSource.volume = globalVolume;
        }
    }

    // Unregister an AudioSource from being managed by this AudioManager
    public void UnregisterAudioSource(AudioSource audioSource)
    {
        managedAudioSources.Remove(audioSource);
    }

    // Set the global volume and update all managed AudioSources
    public void SetVolume(float volume)
    {
        globalVolume = volume;
        foreach (var audioSource in managedAudioSources)
        {
            audioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return globalVolume;
    }
}