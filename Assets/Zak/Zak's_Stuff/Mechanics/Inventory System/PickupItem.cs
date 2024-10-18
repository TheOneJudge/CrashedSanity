using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Item item; // Assign this in the Inspector

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>(); // Get the InventoryManager
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the correct tag
        {
            PickUpItem(item);
            Destroy(gameObject); // Destroy the pickup object after collection
        }
    }

    private void PickUpItem(Item item)
    {
        if (inventoryManager != null)
        {
            inventoryManager.AddItem(item.ID);
            Debug.Log($"Picked up: {item.Name}");
        }
        else
        {
            Debug.LogError("InventoryManager not found.");
        }
    }
}
