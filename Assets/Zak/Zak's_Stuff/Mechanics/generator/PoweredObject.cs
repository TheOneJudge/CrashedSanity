using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredObject : MonoBehaviour
{
     // Reference to the generator
    public Generator generator;

    // Update function to check if the generator is on
    void Update()
    {
        if (generator != null && generator.IsPowered())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ToggleDoor();
            }
        }
    }

    private void ToggleDoor()
    {
        Debug.Log("Door toggled open/close.");
    }
}
