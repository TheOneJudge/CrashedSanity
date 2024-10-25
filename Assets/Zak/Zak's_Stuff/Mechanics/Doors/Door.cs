using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float activationRadius = 5f;  // Radius in which the player can trigger the door
    public float doorSpeed = 2f;

    private Transform player;

    private bool isPlayerInRange = false;
    private bool isDoorOpen = false;

    // Animation components
    public Animator doorAnimator;  // Attach your Animator component here

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        isPlayerInRange = IsPlayerInRange();

        if (isPlayerInRange && !isDoorOpen)
        {
            // Play door open animation
            doorAnimator.SetTrigger("DoorOpen");
            isDoorOpen = true;
        }
        else if (!isPlayerInRange && isDoorOpen)
        {
            // Play door close animation
            doorAnimator.SetTrigger("DoorOpen");
            isDoorOpen = false;
        }
        else if (!isPlayerInRange && !isDoorOpen)
        {
            // Play idle animation after the door is fully closed
            doorAnimator.SetTrigger("Idle");
        }
    }

    private bool IsPlayerInRange()
    {
        if (player == null) return false;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        return distanceToPlayer <= activationRadius;
    }

    // Make sure the Gizmo is visible in the Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}
