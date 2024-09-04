using UnityEngine;
using System.Collections;

public class CameraIntroEffect : MonoBehaviour
{
    public Transform player;
    public float duration = 3f;
    public float heightAbovePlayer = 5f;
    public float distanceBehindPlayer = 3f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Vector3 endPosition;
    private Quaternion endRotation;

    private void Start()
    {
        // Store the starting position and rotation
        startPosition = transform.position;
        startRotation = transform.rotation;

        // Calculate the end position
        Vector3 behindPlayerPosition = player.position - player.forward * distanceBehindPlayer;
        endPosition = behindPlayerPosition + Vector3.up * heightAbovePlayer;

        // Calculate the end rotation to face forward
        endRotation = Quaternion.LookRotation(player.forward, Vector3.up);

        // Start the camera movement coroutine
        StartCoroutine(MoveCameraCoroutine());
    }

    private IEnumerator MoveCameraCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Smoothly interpolate position and rotation
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera ends at the exact end position and rotation
        transform.position = endPosition;
        transform.rotation = endRotation;
    }
}