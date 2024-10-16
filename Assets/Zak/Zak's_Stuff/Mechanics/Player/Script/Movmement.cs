using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movmement : MonoBehaviour
{
   private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField]
    private float playerSpeed = 2.0f;
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
    
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.visible = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jumped = context.action.triggered;
    }

    void Update()
    {
        // Handle camera rotation
        RotatePlayerWithCamera();

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Convert movement input based on camera's forward direction
        Vector3 move = new Vector3(MovementInput.x, 0, MovementInput.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0; // Prevent player from moving up/down when looking up/down

        controller.Move(move * Time.deltaTime * playerSpeed);

        // Handle jumping
        if (Jumped && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Rotate player according to mouse or controller right stick
    private void RotatePlayerWithCamera()
    {
        // Horizontal rotation (left-right look)
        float yaw = LookInput.x * rotationSpeed * Time.deltaTime;

        // Apply rotation to the player
        transform.Rotate(0, yaw, 0);

<<<<<<< Updated upstream
        // Optionally, vertical rotation can be applied to the camera if needed
    }
}
=======
        public void OnJump(InputAction.CallbackContext context)
        {
            Jumped = context.action.triggered;
        }

        void Update()
        {
            // Handle camera rotation
            RotatePlayerWithCamera();

            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            // Convert movement input based on camera's forward direction
            Vector3 move = new Vector3(MovementInput.x, 0, MovementInput.y);
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0; // Prevent player from moving up/down when looking up/down

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

            controller.Move(move * Time.deltaTime * currentSpeed);

            // Handle jumping
            if (Jumped && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

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
>>>>>>> Stashed changes
