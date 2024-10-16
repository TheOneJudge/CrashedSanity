using UnityEngine;

public class FloatingPrompt : PromptScript
{
    public float floatSpeed = 1f; // Speed of the floating motion
    public float floatAmplitude = 0.5f; //floating motion
    public float rotationSpeed = 1f; // Speed of rotation towards the player

    private Vector3 startPosition; // Original position of the prompt
    private Transform playerTransform; 

    private void Start()
    {
       
        startPosition = transform.position;

       
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene. Please ensure the player is tagged as 'Player'.");
        }
    }

    private void Update()
    {
        if (playerTransform == null) return; // Do nothing 

        // Floating motion
        float floatY = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = startPosition + new Vector3(0, floatY, 0);

        
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0; // Keep the rotation level 

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        targetRotation.eulerAngles = new Vector3(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, 0); // Freeze Z-axis rotation

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
