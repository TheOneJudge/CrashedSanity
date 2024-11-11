using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HATCH : MonoBehaviour
{
    public string playerTag = "Player";  // Tag for the player (you can set this in the Inspector or keep it hardcoded)

    // Trigger collision detection with the player
    void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding has the "Player" tag
        if (other.CompareTag(playerTag))
        {
            // Destroy this game object upon collision with the player
            Destroy(gameObject);  // This deletes the object this script is attached to
        }
    }

    // Optionally, you can use OnCollisionEnter if you need non-trigger colliders
    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag(playerTag))
    //     {
    //         Destroy(gameObject);  // This deletes the object this script is attached to
    //     }
    // }
}
