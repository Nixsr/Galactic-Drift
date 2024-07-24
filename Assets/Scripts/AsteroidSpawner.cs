using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRate = 1.1f;
    public float asteroidForce = 500f;
    public Vector3 spawnAreaSize = new Vector3(20f, 10f, 0f);
    public float spawnDistance = 400f;
    public float avoidanceRadius = 10f; // Radius around the camera to avoid

    private Camera mainCamera;

    public int totalAsteroids = 10;

    void Start()
    {
        mainCamera = Camera.main;
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.totalAsteroids = totalAsteroids;
            
            Debug.Log("AsteroidSpawner set total asteroids: " + totalAsteroids);
        }
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnAsteroids()
    {
        for (int i = 0; i < totalAsteroids; i++)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnAsteroid()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rb = asteroid.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            Vector3 direction = CalculateAvoidanceDirection(asteroid.transform.position);
            rb.AddForce(direction * asteroidForce);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float y = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * spawnDistance;
        spawnPosition += mainCamera.transform.right * x + mainCamera.transform.up * y;
        return spawnPosition;
    }

    Vector3 CalculateAvoidanceDirection(Vector3 asteroidPosition)
    {
        Vector3 directionToCamera = mainCamera.transform.position - asteroidPosition;
        Vector3 perpendicularDirection = Vector3.Cross(directionToCamera, Vector3.up).normalized;
        
        // Randomly choose whether to go to the right or to the left of the room
        if (Random.value > 0.5f)
        {
            perpendicularDirection = -perpendicularDirection;
        }

        // Calculate a direction that avoids the camera
        Vector3 avoidanceDirection = (directionToCamera + perpendicularDirection * avoidanceRadius).normalized;
        return avoidanceDirection;
    }
}