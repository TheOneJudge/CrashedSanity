using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredDoor : MonoBehaviour
{
    public enum SlideDirection { XAxis, YAxis, ZAxis }
    public SlideDirection slideDirection = SlideDirection.XAxis;

    // Variables to control the door sliding
    public float activationWidth = 5f;
    public float activationHeight = 2f;
    public float slideDistance = 2f;
    public float doorSpeed = 2f;

    public Generator generator;

    private Transform player;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    private bool isPlayerInRange = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        closedPosition = transform.position;

        openPosition = CalculateOpenPosition();
    }

    void Update()
    {
        isPlayerInRange = IsPlayerInRange() && IsGeneratorPowered();

        if (isPlayerInRange)
        {
            MoveDoor(openPosition);
        }
        else
        {
            MoveDoor(closedPosition);
        }
    }

    private Vector3 CalculateOpenPosition()
    {
        switch (slideDirection)
        {
            case SlideDirection.XAxis:
                return closedPosition + new Vector3(slideDistance, 0f, 0f);
            case SlideDirection.YAxis:
                return closedPosition + new Vector3(0f, slideDistance, 0f);
            case SlideDirection.ZAxis:
                return closedPosition + new Vector3(0f, 0f, slideDistance);
            default:
                return closedPosition;
        }
    }

    private bool IsPlayerInRange()
    {
        if (player == null) return false;

        Vector3 playerPosition = player.position;

        float halfWidth = activationWidth / 2f;
        float halfHeight = activationHeight / 2f;

        return (playerPosition.x >= transform.position.x - halfWidth && playerPosition.x <= transform.position.x + halfWidth) &&
               (playerPosition.z >= transform.position.z - halfHeight && playerPosition.z <= transform.position.z + halfHeight);
    }

    private bool IsGeneratorPowered()
    {
        if (generator == null) return false;

        return generator.IsPowered();
    }

    private void MoveDoor(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * doorSpeed);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(activationWidth, 0.1f, activationHeight));
    }
}
