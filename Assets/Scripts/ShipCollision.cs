using UnityEngine;

public class ShipCollision : MonoBehaviour
{
    public AudioClip impactSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        // Configura l'Audio Source per riprodurre il suono una sola volta
        if (audioSource != null)
        {
            audioSource.clip = impactSound;
            audioSource.playOnAwake = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica se la collisione è con un asteroide
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Riproduci il suono di impatto
            if (audioSource != null && impactSound != null)
            {
                audioSource.PlayOneShot(impactSound);
            }

            // Qui puoi aggiungere altra logica per la collisione, se necessario
        }
    }
}