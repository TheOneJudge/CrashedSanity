using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] inventoryPanels; // Assign these in the Inspector
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GetComponent<InventoryManager>();
    }

    public void OpenInventory()
    {
        // Disable all panels initially
        foreach (var panel in inventoryPanels)
        {
            panel.SetActive(false);
        }

        // Enable panels based on collected item IDs
        foreach (var panel in inventoryPanels)
        {
            int panelID = panel.GetComponent<ItemPanel>().ID; // Assume each panel has a script with ID property
            if (inventoryManager.IsItemCollected(panelID))
            {
                panel.SetActive(true);
            }
        }
    }
}
