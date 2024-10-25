using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{
    public AudioClip[] collisionSounds;  // Array of sound effects to choose from
    private AudioSource audioSource;     // The audio source component
    private bool hasPlayed = false;      // Flag to check if the sound has played

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found! Please add an AudioSource component.");
        }

        // Ensure there are 3 sound effects in the array
        if (collisionSounds.Length != 3)
        {
            Debug.LogError("Please assign exactly 3 AudioClip elements to collisionSounds array.");
        }
    }

    // This function is called when the object collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasPlayed)
        {
            PlayRandomSoundOnce();
        }
    }

    // This function is called when another object enters the trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            PlayRandomSoundOnce();
        }
    }

    // Plays a random sound from the array, only once
    private void PlayRandomSoundOnce()
    {
        if (audioSource != null && collisionSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, collisionSounds.Length); // Get a random index
            audioSource.PlayOneShot(collisionSounds[randomIndex]);      // Play the random sound
            hasPlayed = true;                                           // Mark the sound as played
        }
    }
}
