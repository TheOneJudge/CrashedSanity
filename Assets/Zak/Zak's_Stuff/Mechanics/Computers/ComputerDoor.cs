using System.Collections;
using UnityEngine;

public class ComputerDoor : MonoBehaviour
{
    public Vector3 closedPosition;
    public Vector3 openPosition;
    public float slideSpeed = 1f;

    private bool isOpening = false;
    private bool isClosing = false;

    void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, slideSpeed * Time.deltaTime);
            if (transform.position == openPosition)
            {
                isOpening = false;
            }
        }
        else if (isClosing)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, slideSpeed * Time.deltaTime);
            if (transform.position == closedPosition)
            {
                isClosing = false;
            }
        }
    }

    public void RequestOpen()
    {
        isOpening = true; 
        isClosing = false;
    }

    public void RequestClose()
    {
        isClosing = true;
        isOpening = false;
    }
}
