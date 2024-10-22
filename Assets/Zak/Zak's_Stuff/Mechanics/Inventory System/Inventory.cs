using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] idPanels; // Reference to your ID panels (assign these in the Inspector)

    private void Start()
    {
        // Disable all ID panels at start
        foreach (GameObject panel in idPanels)
        {
            panel.SetActive(false);
        }
    }

    public void EnablePanel(string itemID)
    {
        foreach (GameObject panel in idPanels)
    {
        // Assuming your panel names are something like "Panel_1224"
        if (panel.name.EndsWith(itemID)) // Check if the panel name ends with the item ID
        {
            panel.SetActive(true); // Enable the panel
            break;
        }
    }
    }
}
