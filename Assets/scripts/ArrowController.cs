 using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 3f;
    public float targetX = 0f;
    public bool fromLeft = true;
    public ArrowType type;

    void Update()
    {
        if (fromLeft)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            if (transform.position.x >= targetX)
            {
                transform.position = new Vector3(
                    targetX,
                    transform.position.y,
                    transform.position.z
                );

                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (transform.position.x <= targetX)
            {
                transform.position = new Vector3(
                    targetX,
                    transform.position.y,
                    transform.position.z
                );

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