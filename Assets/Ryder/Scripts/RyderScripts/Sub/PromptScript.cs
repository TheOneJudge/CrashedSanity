using UnityEngine;
using System.Collections.Generic;

public class PromptScript : MonoBehaviour
{
    public GameObject promptPrefab; // Prefab for the prompt
    public Vector3 offset = Vector3.up; // Adjustable offset for the prompt position
    public float radius = 10f; // Radius in which the prompt will appear
    public int maxPromptCount = 1; 

    private List<GameObject> spawnedPrompts = new List<GameObject>(); // List to keep track of spawned prompts
    private Transform mainCamera; // Reference to the main camera (overridden)

    private void Start()
    {
        // (overridden)
        // mainCamera = Camera.main.transform;
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

    private void Update()
    {
        // (overridden)
        // Rotate each spawned prompt to face the camera
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }

        foreach (GameObject prompt in spawnedPrompts)
        {
            if (prompt != null)
            {
                prompt.transform.LookAt(mainCamera);
            }
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
