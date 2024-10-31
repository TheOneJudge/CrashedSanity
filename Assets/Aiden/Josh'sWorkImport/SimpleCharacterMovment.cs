using UnityEngine;

public class SimpleCharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;         // Speed of movement
    public float jumpForce = 5f;         // Force of the jump
    public float fallMultiplier = 2.5f;  // Multiplier for falling speed
    public float lowJumpMultiplier = 2f; // Multiplier for low jump speed
    public Transform cameraTransform;    // Reference to the camera for direction

    private Rigidbody rb;                // Reference to the Rigidbody
    private bool isGrounded = true;      // Check if the player is on the ground
    private Animator animator;           // Reference to the Animator

    void Start()
    {
        // Get the Rigidbody and Animator components
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");  // A/D or Left/Right arrow keys
        float moveZ = Input.GetAxis("Vertical");    // W/S or Up/Down arrow keys

        // Calculate the movement direction relative to the camera
        Vector3 moveDirection = cameraTransform.right * moveX + cameraTransform.forward * moveZ;
        moveDirection.y = 0f;  // Prevent movement on the y-axis

        // Move the player if there is any input
        if (moveDirection.magnitude > 0)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);

            // Rotate the character to face the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 4f));
        }

        // Update the animator state for running
        bool isRunning = moveX != 0 || moveZ != 0;
        animator.SetBool("isRunning", isRunning);

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
            animator.SetBool("isGrounded", false);
            isGrounded = false;
        }

        // Apply better jump logic for falling and low jump control
        if (rb.velocity.y < 0)  // Falling
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))  // Low jump
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // Detect collision with the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
        }
    }
}
