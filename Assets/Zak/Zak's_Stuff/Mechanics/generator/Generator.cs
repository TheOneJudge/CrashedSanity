using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private bool isOn = false;

    public float activationRadius = 5f;

    private Transform player;
    public AudioSource GeneratorOn;

    public bool IsPowered()
    {
        return isOn;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        if (IsPlayerInRange() && Input.GetKeyDown(KeyCode.E))
        {
            ToggleGenerator();
        }
    }

    private bool IsPlayerInRange()
    {
        if (player == null) return false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= activationRadius;
    }
    private void ToggleGenerator()
    {
        isOn = !isOn;
        Debug.Log($"{gameObject.name} generator is now " + (isOn ? "ON" : "OFF"));

        if(isOn)
        {
            GeneratorOn.enabled = true;
        }
        else if(!isOn)
        {
            GeneratorOn.enabled = false;
        } 
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}
