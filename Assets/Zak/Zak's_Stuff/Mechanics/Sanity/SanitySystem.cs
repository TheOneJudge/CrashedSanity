using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanitySystem : MonoBehaviour
{
    public float maxSanity = 100f;         // The maximum sanity value
    public float sanityDecreaseRate = 5f;  // The rate at which sanity decreases in the DangerZone
    public float sanityIncreaseRate = 3f;  // The rate at which sanity increases in the SafeZone

    public float currentSanity;
    private bool inDangerZone = false;
    private bool inSafeZone = false;

    void Start()
    {
        currentSanity = maxSanity;  // Initialize sanity at maximum
    }

    void Update()
    {
        if (inDangerZone)
        {
            DecreaseSanity();
        }
        else if (inSafeZone)
        {
            IncreaseSanity();
        }
    }

    void DecreaseSanity()
    {
        currentSanity -= sanityDecreaseRate * Time.deltaTime;
        currentSanity = Mathf.Max(currentSanity, 0f);  // Clamp to minimum of 0
        Debug.Log("Sanity Decreasing: " + currentSanity);
    }

    void IncreaseSanity()
    {
        currentSanity += sanityIncreaseRate * Time.deltaTime;
        currentSanity = Mathf.Min(currentSanity, maxSanity);  // Clamp to maxSanity
        Debug.Log("Sanity Increasing: " + currentSanity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DangerZone"))
        {
            inDangerZone = true;
        }
        else if (other.CompareTag("SafeZone"))
        {
            inSafeZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DangerZone"))
        {
            inDangerZone = false;
        }
        else if (other.CompareTag("SafeZone"))
        {
            inSafeZone = false;
        }
    }

    public float GetCurrentSanity()
    {
        return currentSanity;
    }
}
