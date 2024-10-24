using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private bool isOn = false;

    public float activationRadius = 5f;

    private Transform player;

    private AudioSource audioSource;

    public AudioClip generatorSound;

    public bool IsPowered()
    {
        return isOn;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        audioSource = GetComponent<AudioSource>();
        
        if (audioSource != null && generatorSound != null)
        {
            audioSource.clip = generatorSound;
            audioSource.loop = true;
        }
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

        if (isOn)
        {
            PlayGeneratorSound();
        }
        else
        {
            StopGeneratorSound();
        }
    }

    private void PlayGeneratorSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void StopGeneratorSound()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}
