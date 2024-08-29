using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float minSize = 0.5f;
    public float maxSize = 2f;

    private void Start()
    {
        // Imposta una rotazione e una dimensione casuale per ogni asteroide
        transform.rotation = Random.rotation;
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * randomSize;
    }

    private void Update()
    {
        // Fa ruotare l'asteroide continuamente
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
            // Il giocatore ha colpito l'asteroide
    //        PlayerHit();
    //    }
    //}

    private void OnBecameInvisible()
    {
        // L'asteroide è uscito dalla visuale della camera
        AsteroidAvoided();
    }

    private void PlayerHit()
    {
        Debug.Log("Giocatore colpito da un asteroide!");
        // Qui puoi aggiungere la logica per gestire la collisione con il giocatore
        // Ad esempio, ridurre la vita del giocatore o terminare il gioco
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerHit();
        }
        Destroy(gameObject);
    }

    private void AsteroidAvoided()
    {
        if (GameManager.Instance != null)
        {
        GameManager.Instance.AsteroidAvoided();
        UIManager uiManager = FindObjectOfType<UIManager>();
        }
    Destroy(gameObject);
    }
}