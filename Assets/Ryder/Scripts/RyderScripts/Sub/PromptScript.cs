using UnityEngine;
using System.Collections.Generic;

public class PromptScript : MonoBehaviour
{
    public GameObject promptPrefab; // Prefab for the prompt
    public Vector3 offset = Vector3.up; // Adjustable offset for the prompt position
    public float radius = 10f; // Radius in which the prompt will appear
    public int maxPromptCount = 1;

    private List<GameObject> spawnedPrompts = new List<GameObject>(); // List to keep track of spawned prompts
    protected Transform playerTransform; // Reference to the player's transform

    protected virtual void Start() // Change to protected virtual to allow access in derived classes
    {
        // Find the player in the scene
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

    protected virtual void Update() // Change to protected virtual to allow access in derived classes
    {
        if (playerTransform == null) return; // Do nothing if the player is not found

        // Rotate each spawned prompt to face the player
        foreach (GameObject prompt in spawnedPrompts)
        {
            if (prompt != null)
            {
                Vector3 directionToPlayer = playerTransform.position - prompt.transform.position;
                directionToPlayer.y = 0; // Keep the rotation level
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                prompt.transform.rotation = targetRotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && promptPrefab != null)
        {
            // Check if we have reached the maximum number of prompts
            if (spawnedPrompts.Count >= maxPromptCount)
            {
                return; // Do not spawn more prompts
            }

            // Calculate random position within the radius
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * radius + offset;
            randomPosition.y = transform.position.y + offset.y; // Adjust the height based on offset

            // Spawn the prompt
            GameObject newPrompt = Instantiate(promptPrefab, randomPosition, Quaternion.identity);
            spawnedPrompts.Add(newPrompt); // Add the new prompt to the list
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy all spawned prompts and clear the list
            foreach (GameObject prompt in spawnedPrompts)
            {
                if (prompt != null)
                {
                    Destroy(prompt);
                }
            }
            spawnedPrompts.Clear(); // Clear the list after destroying the prompts
        }
    }
}
