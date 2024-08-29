using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 500f;
    public float boundaryOffset = 0.1f; // Distance from screen edges

    private float minX, maxX, minY, maxY;
    private float fixedZ;
    private Camera mainCamera;

    // Per le funzioni di touching 
    private Vector2 initialTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isTouching = false;
    // Fine per le funzioni di touching


    void Start()
    {
        //asteroidsCounter = new AsteroidsCounter();
        
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
            return;
        }

        CalculateBoundaries();
        fixedZ = transform.position.z; // Store the initial Z position
    }

    void Update()
    {
        HandleInput();
        // Per le funzioni di touching
        HandleTouch();
    }

    void CalculateBoundaries()
    {
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(boundaryOffset, boundaryOffset, -mainCamera.transform.position.z));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1 - boundaryOffset, 1 - boundaryOffset, -mainCamera.transform.position.z));
        
        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;
    }

    void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;
        
        // Clamp the X and Y positions within boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        
        // Keep the Z position fixed
        newPosition.z = fixedZ;
        
        transform.position = newPosition;
    }

// Per le funzioni di touching
void HandleTouch()
{
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                isTouching = true;
                initialTouchPosition = touch.position;
                break;
            case TouchPhase.Moved:
                if (isTouching)
                {
                    currentTouchPosition = touch.position;
                    HandleTouchMovement();
                }
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                isTouching = false;
                break;
        }
    }
    else
    {
        isTouching = false;
    }
}

void HandleTouchMovement()
{
    float horizontalInput = (currentTouchPosition.x - initialTouchPosition.x) / Screen.width;
    float verticalInput = (currentTouchPosition.y - initialTouchPosition.y) / Screen.height;

    Vector3 movement = new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * 100 * Time.deltaTime;
    Vector3 newPosition = transform.position + movement;

    // Clamp the X and Y positions within boundaries
    newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
    newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

    // Keep the Z position fixed
    newPosition.z = fixedZ;

    transform.position = newPosition;

    // Reset the initial touch position
    initialTouchPosition = currentTouchPosition;
}

// Fine per le funzioni di touching


     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlayerHit();
            }
        }
    }

}