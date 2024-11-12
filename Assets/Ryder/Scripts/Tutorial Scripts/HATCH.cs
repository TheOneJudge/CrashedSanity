using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HATCH : MonoBehaviour
{
    public string playerTag = "Player";  

    // Trigger collision detection with the player
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Destroy(gameObject);  
        }
    }

} 
