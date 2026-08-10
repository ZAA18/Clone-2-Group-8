 using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;

    public Transform centerLine;

    public float spawnInterval = 1.5f;
    public float speed = 3f;

    private float centerY;

    private float topY = 4f;
    private float bottomY = -4f;

    void Start()
    {
        centerY = centerLine.position.y;
        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        while (true)
        {
            // Randomly choose an arrow
            int randomArrow = Random.Range(0, 4);

            // Randomly choose top or bottom
            bool fromTop = Random.value > 0.5f;

            GameObject arrow = null;
            float x = 0f;

            // Choose the arrow and its lane
            switch (randomArrow)
            {
                case 0:
                    arrow = upArrow;
                    x = -3f;
                    break;

                case 1:
                    arrow = downArrow;
                    x = -1f;
                    break;

                case 2:
                    arrow = leftArrow;
                    x = 1f;
                    break;

                case 3:
                    arrow = rightArrow;
                    x = 3f;
                    break;
            }

            if (fromTop)
            {
                SpawnFromTop(arrow, x);
            }
            else
            {
                SpawnFromBottom(arrow, x);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnFromTop(GameObject prefab, float x)
    {
        GameObject arrow = Instantiate(
            prefab,
            new Vector3(x, topY, 0),
            Quaternion.identity
        );

        ArrowController c = arrow.GetComponent<ArrowController>();

        c.speed = speed;
        c.targetY = centerY;
        c.fromTop = true;

        RotateArrow(arrow, c.type);
    }

    void SpawnFromBottom(GameObject prefab, float x)
    {
        GameObject arrow = Instantiate(
            prefab,
            new Vector3(x, bottomY, 0),
            Quaternion.identity
        );

        ArrowController c = arrow.GetComponent<ArrowController>();

        c.speed = speed;
        c.targetY = centerY;
        c.fromTop = false;

        RotateArrow(arrow, c.type);
    }

    void RotateArrow(GameObject arrow, ArrowType type)
    {
        switch (type)
        {
            case ArrowType.Up:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case ArrowType.Down:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;

            case ArrowType.Left:
                arrow.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;

            case ArrowType.Right:
                arrow.transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }
    }
}
