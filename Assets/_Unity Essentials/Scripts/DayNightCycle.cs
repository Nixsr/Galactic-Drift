using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Tooltip("The number of seconds for a full day-night cycle")]
    public float secondsPerDay = 240f;

    [Tooltip("The axis around which the light rotates")]
    public Vector3 rotationAxis = Vector3.right;

    private float rotationSpeed;

    void Start()
    {
        // Calculate rotation speed in degrees per second
        rotationSpeed = 360f / secondsPerDay;
    }

    void Update()
    {
        // Rotate the light around the specified axis
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}