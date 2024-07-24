using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float minSize = 0.5f;
    public float maxSize = 2f;

    private void Start()
    {
        // Set a random rotation and size for each asteroid
        transform.rotation = Random.rotation;
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * randomSize;
    }

    private void Update()
    {
        // Make the asteroid rotate continuously
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        // The asteroid has moved out of the camera's view
        AsteroidAvoided();
    }

    private void PlayerHit()
    {
        Debug.Log("Giocatore colpito da un asteroide!");

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