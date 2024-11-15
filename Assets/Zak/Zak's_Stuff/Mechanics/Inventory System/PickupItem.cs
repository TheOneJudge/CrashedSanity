using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemID; // e.g., "ID.1224"
    public GameObject inventory; // Reference to the Inventory GameObject
    public float pickupRange = 2f; // Distance within which the player can pick up the item

    private bool isPlayerInRange = false; // Track if player is in range

    private void Update()
    {
        // Check if the player is within pickup range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Q)) // Check if E is pressed
        {
            // Disable the object
            gameObject.SetActive(false);

            // Call method in the inventory to enable the specific panel
            Inventory inventoryScript = inventory.GetComponent<Inventory>();
            inventoryScript.EnablePanel(itemID);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the "Player" tag
        {
            isPlayerInRange = true; // Set the flag when the player enters the range
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the "Player" tag
        {
            isPlayerInRange = false; // Reset the flag when the player exits the range
        }
    }
}
