using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public Animator animator;

    public AudioSource walking;
    public AudioSource jogging;
    public AudioSource sprinting;

    public AudioSource jumpSound;
    public AudioSource landSound;

    [SerializeField]
    private float playerSpeed = 2.0f; // Jogging speed
    [SerializeField]
    private float walkSpeed = 1.0f; // Walking speed (slower)
    [SerializeField]
    private float sprintSpeed = 4.0f; // Sprinting speed (faster)
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private Transform cameraTransform; // Reference to the player's camera

    private Vector2 movementInput = Vector2.zero;
    private Vector2 lookInput = Vector2.zero;
    private bool jumpRequest = false;

    [SerializeField]
    private float rotationSpeed = 10.0f; // Speed of rotation for smooth turning
    private float cameraPitch = 0.0f; // Track the camera's up/down rotation
    [SerializeField]
    private float maxLookAngle = 80.0f; // Limit the vertical look angle

    private bool isWalking = false;
    private bool isSprinting = false;

    private float currentSpeed; // Current speed of the player, which will gradually increase

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        currentSpeed = 0.0f; // Initially, set speed to 0
    }

    // Handle Jogging (default movement)
    public void OnJog(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    // Handle Walking (toggle walk on/off)
    public void OnWalk(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isWalking = !isWalking;  // Toggle walking state
            isSprinting = false;     // Ensure sprinting is off when walking
        }
        else if (context.canceled) // Reset to default jogging speed on Ctrl release
        {
            isWalking = false;  // Ensure walking is off when Ctrl is released
            currentSpeed = playerSpeed; // Reset to default jogging speed
        }
    }

    // Handle Sprinting (toggle sprint on/off)
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprinting = true;
            isWalking = false;  // Ensure walking is off when sprinting
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }

    // Handle Jumping
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && groundedPlayer)
        {
            jumpRequest = true;
        }
    }

    void FixedUpdate()
    {
        groundedPlayer = controller.isGrounded;

        // Reset vertical velocity if grounded (player is not falling)
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            landSound.Play();  // Play landing sound
        }

        // Convert movement input based on camera orientation
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0;  // Flatten the forward vector to remove vertical component
        cameraForward.Normalize();  // Normalize to maintain consistent movement speed

        // Calculate movement direction based on camera orientation
        Vector3 moveDirection = (cameraForward * movementInput.y) + (cameraTransform.right * movementInput.x);

        // Gradually increase speed based on current movement state
        if (isWalking)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, Time.deltaTime * 5f); // Speed up to walking speed
        }
        else if (isSprinting)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, sprintSpeed, Time.deltaTime * 10f); // Speed up to sprinting speed
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, playerSpeed, Time.deltaTime * 5f); // Speed up to jogging speed
        }

        // Move the player character
        controller.Move(moveDirection * Time.deltaTime * currentSpeed);

        // Rotate the player to face the direction they are moving
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // If the player is not moving, set speed to 0
            currentSpeed = 0f;
        }

        // Handle Jumping (apply jump force)
        if (jumpRequest && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);  // Jump force
            jumpSound.Play();  // Play jump sound
            jumpRequest = false;
        }

        // Apply gravity to player
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Update animator to reflect movement speed
        UpdateAnimatorAndAudio();
    }

    // Update the animator with player speed and play appropriate audio
    void UpdateAnimatorAndAudio()
    {
        // Pass the current speed to the Animator to control the animation
        animator.SetFloat("playerSpeed", currentSpeed);

        // Play appropriate audio based on current speed
        float speed = controller.velocity.magnitude;

        if (speed > 0 && speed < 10f)
        {
            if (!walking.isPlaying) walking.Play();
            if (jogging.isPlaying) jogging.Stop();
            if (sprinting.isPlaying) sprinting.Stop();
        }
        else if (speed >= 10f && speed < 20f)
        {
            if (!jogging.isPlaying) jogging.Play();
            if (walking.isPlaying) walking.Stop();
            if (sprinting.isPlaying) sprinting.Stop();
        }
        else if (speed >= 20f)
        {
            if (!sprinting.isPlaying) sprinting.Play();
            if (walking.isPlaying) walking.Stop();
            if (jogging.isPlaying) jogging.Stop();
        }
        else
        {
            // Stop all audio if speed is 0 (player is stationary)
            if (walking.isPlaying) walking.Stop();
            if (jogging.isPlaying) jogging.Stop();
            if (sprinting.isPlaying) sprinting.Stop();
        }
    }
}
