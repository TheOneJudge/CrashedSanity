using UnityEngine;

public class alienAnimStart : MonoBehaviour
{
    public Animator animator;    // Reference to the Animator component on the target object
    public string animationTrigger = "StartAnimation"; // Trigger name in the Animator
    public float detectionRange = 10f; // Adjust as needed

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Casts a ray from the center of the screen
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.collider.gameObject == gameObject) // Checks if the object hit is the target
            {
                animator.SetTrigger(animationTrigger); // Start the animation
            }
        }
    }
}
