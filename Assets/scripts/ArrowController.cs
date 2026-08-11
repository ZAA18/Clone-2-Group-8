 using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 3f;
    public float targetY = 0f;
    public bool fromTop = true;
    public ArrowType type = ArrowType.Up;

    void Update()
    {
        if (fromTop)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            if (transform.position.y <= targetY)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            if (transform.position.y >= targetY)
            {
                Destroy(gameObject);
            }
        }
    }
}

public enum ArrowType
{
    Up,
    Down,
    Left,
    Right
}