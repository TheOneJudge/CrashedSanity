using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;          // The target the camera will follow (usually the player)
    public float distance = 20f;       // Default distance from the player
    public float minDistance = 2f;    // Minimum zoom distance
    public float maxDistance = 30f;   // Maximum zoom distance
    public float sensitivityX = 3f;   // Sensitivity for mouse movement on the X-axis (left/right)
    public float sensitivityY = 1.5f; // Sensitivity for mouse movement on the Y-axis (up/down)
    public float zoomSpeed = 2f;      // Speed for zooming in/out with the scroll wheel
    public float minY = -20f;         // Minimum Y angle (how far you can look down)
    public float maxY = 80f;          // Maximum Y angle (how far you can look up)
    public float smoothTime = 0.1f;   // Smooth transition for camera movement

    private float currentX = 0f;      // Current X rotation (horizontal)
    private float currentY = 0f;      // Current Y rotation (vertical)
    private Vector3 currentVelocity;  // Used for smoothing the camera movement

    private bool active = false;

    void Start()
    {
        // Lock the cursor to the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        
            // Get mouse movement for rotating the camera
            currentX += Input.GetAxis("Mouse X") * sensitivityX;
            currentY -= Input.GetAxis("Mouse Y") * sensitivityY;

            // Clamp the Y rotation between minY and maxY to prevent over-rotation
            currentY = Mathf.Clamp(currentY, minY, maxY);

            // Zoom in and out using the scroll wheel
            distance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);  // Clamp the zoom distance

            // Calculate the camera rotation around the target (player)
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

            // Calculate the desired camera position based on the zoom distance
            Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance);

            // Smoothly move the camera to the desired position
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);

            // Always look at the target (player)
            transform.LookAt(target.position);
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && !active)
        {
            float timer = 0;
            timer += Time.deltaTime;
            if(timer > 1)
            {
                timer = 0;
                Debug.Log("time");

            }
            active = true;

        }
        else if (Input.GetKeyUp(KeyCode.Tab) && active)
        {
            active = false;
        }
    }
}
