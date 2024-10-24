using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSoundScript : MonoBehaviour
{
    // AudioSource component to play the sound
    public AudioSource audioSource;

    // Animator component controlling the object's animations
    private Animator animator;

    // The animation state you want to trigger the sound on
    public string animationStateName;

    // Boolean to avoid replaying the sound while animation is active
    private bool hasPlayed = false;

    void Start()
    {
        // Get the Animator component attached to this object
        animator = GetComponent<Animator>();

        // Make sure AudioSource is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned!");
        }
    }

    void Update()
    {
        // Check if the object is playing the specified animation state
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationStateName))
        {
            // If the sound hasn't played yet, play it
            if (!hasPlayed)
            {
                audioSource.Play();
                hasPlayed = true; // Prevent the sound from playing multiple times
            }
        }
        else
        {
            // Reset when the animation state is not playing
            hasPlayed = false;
        }
    }
}
