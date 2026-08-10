 using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float targetY = 0f;
    public ArrowSpawnDirection spawnDirection = ArrowSpawnDirection.Top;
    
    [Header("Arrow Type")]
    public ArrowType arrowType = ArrowType.Up;
    
    private bool isMoving = true;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ApplyArrowRotation();
    }
    
    void Update()
    {
        if (!isMoving) return;
        
        // Move straight up or down (only Y changes!)
        if (spawnDirection == ArrowSpawnDirection.Top)
        {
            // Moving DOWN from top
            transform.position += Vector3.down * speed * Time.deltaTime;
            
            if (transform.position.y <= targetY)
                Destroy(gameObject);
        }
        else // Bottom
        {
            // Moving UP from bottom
            transform.position += Vector3.up * speed * Time.deltaTime;
            
            if (transform.position.y >= targetY)
                Destroy(gameObject);
        }
    }
    
    void ApplyArrowRotation()
    {
        // Reset rotation first
        transform.rotation = Quaternion.identity;
        
        // Apply rotation based on arrow type
        switch (arrowType)
        {
            case ArrowType.Up:
                // Points UP (no rotation needed)
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case ArrowType.Down:
                // Points DOWN (rotate 180)
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case ArrowType.Left:
                // Points LEFT (rotate 90)
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case ArrowType.Right:
                // Points RIGHT (rotate -90)
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
        
        // Debug log to verify
        Debug.Log($"Arrow: {arrowType}, Rotation: {transform.rotation.eulerAngles.z}°");
    }
}