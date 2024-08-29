using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotationSpeed;
    public GameObject onCollectEffect;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0); // Rotate the collectible around the Y-axis.

    }

    private void OnTriggerEnter(Collider other){
        
        if (other.CompareTag("Player")){

            PlayImpactSound();

            // Destroy the collectible
            Destroy(gameObject);

            // instantiate the particle effect
            Instantiate(onCollectEffect, transform.position, transform.rotation);
        }

    }

    void PlayImpactSound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

}