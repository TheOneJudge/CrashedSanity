using UnityEngine;

public class FloatingPrompt : PromptScript
{
    public float floatSpeed = 1f; // Speed of the floating motion
    public float floatAmplitude = 0.5f; // Amplitude of the floating motion
    public float rotationSpeed = 1f; // Speed of rotation towards the player

    private Vector3 startPosition; // Original position of the prompt

    protected override void Start() // Use override to extend the base class functionality
    {
        startPosition = transform.position;
        base.Start(); // Call the base class Start method to find the player
    }

    protected override void Update() // Use override to extend the base class functionality
    {
        base.Update(); // Call the base class Update method to handle rotation

        // Floating motion
        float floatY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = startPosition + new Vector3(0, floatY, 0);
    }
}
