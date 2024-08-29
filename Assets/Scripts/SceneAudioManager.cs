// SceneAudioManager.cs
using UnityEngine;

public class SceneAudioManager : MonoBehaviour
{
    public AudioSource[] sceneAudioSources;

    private void Start()
    {
        foreach (var audioSource in sceneAudioSources)
        {
            AudioManager.Instance.RegisterAudioSource(audioSource);
        }
    }

    private void OnDestroy()
    {
        foreach (var audioSource in sceneAudioSources)
        {
            AudioManager.Instance.UnregisterAudioSource(audioSource);
        }
    }
}