using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    public int totalCollectibles;
    private int collectedCount = 0;

    public GameObject vfxPrefab;
    private GameObject vfxInstance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem()
    {
        collectedCount++;
        if (collectedCount >= totalCollectibles)
        {
            TriggerCompletionVFX();
        }
    }

    void TriggerCompletionVFX()
    {
        if (vfxPrefab != null)
        {
            vfxInstance = Instantiate(vfxPrefab, transform.position, Quaternion.identity);
            Destroy(vfxInstance, 5f); // Destroy after 5 seconds, adjust as needed
        }
    }
}