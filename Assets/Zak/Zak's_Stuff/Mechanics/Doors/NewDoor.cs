using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoor : MonoBehaviour
{
    public Animator doorAnim;

    void Start ()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player") || other.CompareTag("Alien"))
        {
            doorAnim.SetTrigger("Open");
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player") || other.CompareTag("Alien"))
        {
            doorAnim.SetTrigger("Closed");
        }
    }
}
