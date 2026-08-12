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

    private float centerX;

    private float leftX = -8f;
    private float rightX = 8f;

    private Coroutine spawnCoroutine;

    void Start()
    {
        centerX = centerLine.position.x;
       // StartCoroutine(SpawnSequence());
    }

    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine =
                StartCoroutine(SpawnSequence());
        }
    }

    IEnumerator SpawnSequence()
    {
        while (true)
        {
            int randomArrow = Random.Range(0, 4);

            bool fromLeft = Random.value > 0.5f;

            GameObject arrow = null;
            float y = 0f;

            switch (randomArrow)
            {
                case 0:
                    arrow = upArrow;
                    y = 3f;
                    break;

                case 1:
                    arrow = downArrow;
                    y = 1f;
                    break;

                case 2:
                    arrow = leftArrow;
                    y = -1f;
                    break;

                case 3:
                    arrow = rightArrow;
                    y = -3f;
                    break;
            }

            if (fromLeft)
            {
                SpawnFromLeft(arrow, y);
            }
            else
            {
                SpawnFromRight(arrow, y);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnFromLeft(GameObject prefab, float y)
    {
        GameObject arrow = Instantiate(
            prefab,
            new Vector3(leftX, y, 0),
            Quaternion.identity
        );

        ArrowController c = arrow.GetComponent<ArrowController>();

        c.speed = speed;
        c.targetX = centerX;
        c.fromLeft = true;

        RotateArrow(arrow, c.type);
    }

    void SpawnFromRight(GameObject prefab, float y)
    {
        /*GameObject arrow = Instantiate(
            prefab,
            new Vector3(rightX, y, 0),
            Quaternion.identity
        );

        ArrowController c = arrow.GetComponent<ArrowController>();

        c.speed = speed;
        c.targetX = centerX;
        c.fromLeft = false;

        RotateArrow(arrow, c.type); */

        if (prefab == null)
        {
            Debug.LogError("Spawner: prefab is missing in SpawnFromRight!");
            return;
        }

        GameObject arrow = Instantiate(
            prefab,
            new Vector3(rightX, y, 0),
            Quaternion.identity
        );

        ArrowController c = arrow.GetComponent<ArrowController>();

        if (c == null)
        {
            Debug.LogError(
                "ArrowController is missing from prefab: " + prefab.name
            );

            Destroy(arrow);
            return;
        }

        c.speed = speed;
        c.targetX = centerX;
        c.fromLeft = false;

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
