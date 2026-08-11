 using UnityEngine;

public class SpawnPointVisualizer : MonoBehaviour
{
    public Color gizmoColor = Color.yellow;
    public float radius = 0.2f;
    
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        
        // Draw all spawn points
        Vector2[] topPoints = {
            new Vector2(-3f, 4f),
            new Vector2(0f, 4f),
            new Vector2(3f, 4f)
        };
        
        Vector2[] bottomPoints = {
            new Vector2(-3f, -4f),
            new Vector2(0f, -4f),
            new Vector2(3f, -4f)
        };
        
        // Draw top spawn points
        foreach (Vector2 point in topPoints)
        {
            Gizmos.DrawSphere(point, radius);
            // Draw arrow showing direction
            Gizmos.DrawLine(point, new Vector2(point.x, 0));
        }
        
        // Draw bottom spawn points
        foreach (Vector2 point in bottomPoints)
        {
            Gizmos.DrawSphere(point, radius);
            // Draw arrow showing direction
            Gizmos.DrawLine(point, new Vector2(point.x, 0));
        }
    }
}