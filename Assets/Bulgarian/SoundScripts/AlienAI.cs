using UnityEngine;
using System.Collections;

public class AlienAI : MonoBehaviour
{
    public Transform[] patrolWaypoints;
    public Transform player;
    public float patrolSpeed = 2f;
    public float fleeSpeed = 3f;
    public float attackSpeed = 5f;
    public float detectionRange = 10f;
    private int currentWaypoint = 0;
    private bool isAttacking = false;
    public LayerMask obstructionLayer; // Layer for obstacles like walls

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip[] patrolSounds; // Array for patrol sounds (4 different clips)
    public AudioClip chaseSound; // Clip for the chase sound
    private Coroutine patrolSoundCoroutine;

    void Start()
    {
        StartCoroutine(AttackRoutine());
        StartPatrolSounds(); // Start the patrol sounds on startup
    }

    void Update()
    {
        if (isAttacking && HasLineOfSight())
        {
            ChasePlayer();
        }
        else if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            FleeFromPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Transform targetWaypoint = patrolWaypoints[currentWaypoint];
        MoveTowards(targetWaypoint.position, patrolSpeed);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % patrolWaypoints.Length;
            AlignToSurface();
        }

        // If not already patrolling, restart patrol sounds
        if (!audioSource.isPlaying && !isAttacking)
        {
            StartPatrolSounds();
        }
    }

    void FleeFromPlayer()
    {
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 fleeTarget = transform.position + fleeDirection * detectionRange;
        MoveTowards(fleeTarget, fleeSpeed);
    }

    void ChasePlayer()
    {
        MoveTowards(player.position, attackSpeed);

        // Stop patrol sounds and play chase sound
        if (patrolSoundCoroutine != null)
        {
            StopCoroutine(patrolSoundCoroutine);
            patrolSoundCoroutine = null;
        }
        if (audioSource.clip != chaseSound || !audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = chaseSound;
            audioSource.loop = true; // Loop chase sound while chasing
            audioSource.Play();
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void AlignToSurface()
    {
        // Raycast logic here to detect and align with wall/ceiling
    }

    bool HasLineOfSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer, obstructionLayer))
        {
            return hit.transform == player;
        }
        return true;
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            isAttacking = !isAttacking;
            if (!isAttacking)
            {
                StartPatrolSounds(); // Return to patrol sounds when done attacking
            }
        }
    }

    void StartPatrolSounds()
    {
        if (patrolSoundCoroutine == null && patrolSounds.Length > 0)
        {
            patrolSoundCoroutine = StartCoroutine(CyclePatrolSounds());
        }
    }

    IEnumerator CyclePatrolSounds()
    {
        int soundIndex = 0;
        while (!isAttacking) // Only cycle patrol sounds when not attacking
        {
            audioSource.clip = patrolSounds[soundIndex];
            audioSource.loop = false;
            audioSource.Play();

            // Wait for the current clip to finish before playing the next one
            yield return new WaitForSeconds(audioSource.clip.length);

            // Move to the next sound in the array, looping back to the start if needed
            soundIndex = (soundIndex + 1) % patrolSounds.Length;
        }
    }
}


