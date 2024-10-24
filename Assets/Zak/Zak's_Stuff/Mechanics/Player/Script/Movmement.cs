using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
    public class Movmement : MonoBehaviour 
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public Animator animator;
    float speed;

    public AudioSource walking;
    public AudioSource Jog;
    public AudioSource sprint;

    public AudioSource jump;
    public AudioSource land;
    

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

    Vector2 MovementInput = Vector2.zero;
    Vector2 LookInput = Vector2.zero;
    private bool Jumped = false;

    [SerializeField]
    private float rotationSpeed = 5.0f; // Sensitivity for mouse/controller look
    private float cameraPitch = 0.0f; // Track the camera's up/down rotation
    [SerializeField]
    private float maxLookAngle = 80.0f; // Limit the vertical look angle to prevent over-rotation

    // State management for walking, jogging, and sprinting
    private bool isWalking = false;
    private bool isSprinting = false;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    // Jogging (default movement)
    public void OnJog(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    // Walk when Ctrl is pressed, toggle back to jogging when pressed again
    public void OnWalk(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isWalking = !isWalking; // Toggle walking state
            isSprinting = false; // Ensure sprinting is off
        }
    }

    // Sprint when Shift is held, return to jogging when released
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprinting = true;
            isWalking = false; // Ensure walking is off
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }

    // Look method to handle camera movement
    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jumped = context.action.triggered;
        jump.enabled = true;
    }

    void Update()
    {
        // Handle camera rotation
        RotatePlayerWithCamera();

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            land.enabled = true;
        }

        // Convert movement input based on camera's forward direction
        Vector3 move = new Vector3(MovementInput.x, 0, MovementInput.y);

        // Get the camera's forward vector and ignore the vertical component (y)
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Flatten the forward vector to only consider horizontal movement
        cameraForward.Normalize(); // Normalize to maintain consistent movement speed

        // Now, map the player's movement to only forward and backward along the camera's forward direction
        move = cameraForward * MovementInput.y;

        // Adjust movement speed based on state (walking, jogging, or sprinting)
        float currentSpeed = playerSpeed; // Default jog speed
        
        if (isWalking)
        {
            currentSpeed = walkSpeed;
        }
        else if (isSprinting)
        {
            currentSpeed = sprintSpeed;
        }

        // Move the player forward or backward
        controller.Move(move * Time.deltaTime * currentSpeed);

        // Update speed for the animator
        speed = controller.velocity.magnitude;
        animator.SetFloat("playerSpeed", speed);

        if (speed > 0 && speed < 2 && !walking.isPlaying) // Player is moving and sound is not playing
        {
            walking.enabled = true;
            Jog.enabled = false;
            sprint.enabled = false;
        }
        else if (speed == 0 && walking.isPlaying) // Player stopped moving
        {
            walking.enabled = false;
        }

        if (speed > 2.1 && speed < 5 && !Jog.isPlaying) // Player is moving and sound is not playing
        {
            Jog.enabled = true;
            walking.enabled = false;
            sprint.enabled = false;
        }

        else if (speed == 0 && speed < 2 && Jog.isPlaying) // Player stopped moving
        {
            Jog.enabled = false;
        }

        if (speed > 5.1 && !sprint.isPlaying) // Player is moving and sound is not playing
        {
            sprint.enabled = true;
            walking.enabled = false;
            Jog.enabled = false;
        }

        else if (speed == 0 && speed < 5 && sprint.isPlaying) // Player stopped moving
        {
            sprint.enabled = false;
        }

        // Handle jumping
        if (Jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jump.enabled = false;
            land.enabled = false;
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Rotate player according to mouse or controller right stick
    private void RotatePlayerWithCamera()
    {
        // Horizontal rotation (left-right look)
        float yaw = LookInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, yaw, 0); // Apply horizontal rotation to the player

        // Vertical rotation (up-down look)
        float pitch = LookInput.y * rotationSpeed * Time.deltaTime;
        cameraPitch -= pitch; // Subtract because looking up is negative in most systems
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle); // Clamp the pitch to avoid over-rotation

        // Apply vertical rotation to the camera
        cameraTransform.localEulerAngles = new Vector3(cameraPitch, 0, 0);
    }
}
