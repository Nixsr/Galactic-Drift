// AudioManager.cs
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private float globalVolume = 1f;
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

    public void RegisterAudioSource(AudioSource audioSource)
    {
        if (!managedAudioSources.Contains(audioSource))
        {
            managedAudioSources.Add(audioSource);
            audioSource.volume = globalVolume;
        }
    }

    public void UnregisterAudioSource(AudioSource audioSource)
    {
        managedAudioSources.Remove(audioSource);
    }

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