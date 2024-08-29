using UnityEngine;

public class ImpactSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Play the sound on collision
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}