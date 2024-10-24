using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private bool isOn = false;

    public float activationRadius = 5f;

    private Transform player;

    public AudioSource generatorOn;


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

        if(isOn)
        {
            generatorOn.enabled = true;
        }
        else if(!isOn)
        {
            generatorOn.enabled = false;
        }
        
        Debug.Log($"{gameObject.name} generator is now " + (isOn ? "ON" : "OFF")); 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}
