using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private HashSet<int> collectedItemIDs = new HashSet<int>();

    public void AddItem(int itemId)
    {
        if (!collectedItemIDs.Contains(itemId))
        {
            collectedItemIDs.Add(itemId);
            Debug.Log($"Item with ID {itemId} added to inventory.");
        }
    }

    public bool IsItemCollected(int itemId)
    {
        return collectedItemIDs.Contains(itemId);
    }
}
