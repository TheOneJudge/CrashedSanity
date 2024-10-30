using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineendererScript : MonoBehaviour
{
    public Transform fixedPoint;  // Assign this in the inspector or dynamically
    public Transform hangingLight;  // Assign this in the inspector or dynamically

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        // Check if both points are assigned
        if (fixedPoint == null || hangingLight == null)
        {
            Debug.LogError("FixedPoint or HangingLight is not assigned.");
            return;
        }
        
        // Set the line renderer positions
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, fixedPoint.position);
        lineRenderer.SetPosition(1, hangingLight.position);
        
        // Ensure a material is assigned
        if (lineRenderer.material == null)
        {
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));  // Simple default material
            lineRenderer.startColor = Color.black;
            lineRenderer.endColor = Color.black;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
        }
    }

    void Update()
    {
        // Update the positions in case the light moves
        lineRenderer.SetPosition(0, fixedPoint.position);
        lineRenderer.SetPosition(1, hangingLight.position);
    }
}
