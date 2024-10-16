using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
    public class Movmement : MonoBehaviour 
{
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        public Animator animator;

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
                //animator.Set
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
