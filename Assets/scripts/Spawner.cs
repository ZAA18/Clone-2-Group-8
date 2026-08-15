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

    // Speed variation parameters
    public float minSpeed = 1.5f;
    public float maxSpeed = 6f;
    public float speedChangeInterval = 5f; // How often speed changes (in seconds)
    public float fastSpeedThreshold = 4.5f; // Speed above this triggers double arrows

    private float currentSpeed;

    [Header("Rhythm")]
    public Conductor conductor;
    public float travelBeats = 4f;

    void Start()
    {
        centerX = centerLine.position.x;
        currentSpeed = speed; // Initialize with default speed
       // StartCoroutine(ChangeSpeedRoutine()); // Start speed variation coroutine
    }

    private void OnEnable()
    {
        Conductor.OnBeat += HandleBeat;
    }

    private void OnDisable()
    {
        Conductor.OnBeat -= HandleBeat;
    }

    private void HandleBeat(int beat)
    {
        Debug.Log("SPAWNER RECEIVED BEAT: " + beat);

        int targetBeat = beat + Mathf.RoundToInt(travelBeats);

        SpawnSingleArrow(targetBeat);
    }

   /* IEnumerator SpawnSequence()
    {
        while (true)
        {
            // Check if current speed is fast
            bool isFastSpeed = currentSpeed >= fastSpeedThreshold;

            // Determine how many arrows to spawn (1 or 2)
            int arrowsToSpawn = isFastSpeed ? 2 : 1;

            for (int i = 0; i < arrowsToSpawn; i++)
            {
                // Spawn an arrow
                SpawnSingleArrow();

                // If this is the first arrow and we're spawning 2, add a small delay
                if (i == 0 && arrowsToSpawn == 2)
                {
                    yield return new WaitForSeconds(0.2f); // Small delay between arrows
                }
            }

            // Wait for the spawn interval before next wave
            yield return new WaitForSeconds(spawnInterval);
        }
    }*/

    void SpawnSingleArrow(int targetBeat)
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
            SpawnFromLeft(arrow, y, targetBeat);
        }
        else
        {
            SpawnFromRight(arrow, y, targetBeat);
        }
    }

    // Coroutine to change speed randomly
    IEnumerator ChangeSpeedRoutine()
    {
        while (true)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(speedChangeInterval);

            // Generate a random speed between min and max
            currentSpeed = Random.Range(minSpeed, maxSpeed);

            // Uncomment to see speed changes in console
            Debug.Log($"Speed changed to: {currentSpeed} - Double arrows: {(currentSpeed >= fastSpeedThreshold ? "YES" : "NO")}");
        }
    }

    void SpawnFromLeft(GameObject prefab, float y, int targetBeat)
    {
        GameObject arrow = Instantiate(
            prefab,
            new Vector3(leftX, y, 0),
            Quaternion.identity
        );

        ArrowController c = arrow.GetComponent<ArrowController>();

        NoteObject note = arrow.GetComponent<NoteObject>();

        note.conductor = conductor;
        note.SetTargetHitTime(targetBeat * conductor.secondsPerBeat);

        // Use the current speed instead of the constant speed
        float travelTime = conductor.secondsPerBeat * travelBeats;
        float distance = Mathf.Abs(centerX - leftX);

        c.speed = distance / travelTime;
        c.targetX = centerX;
        c.fromLeft = true;

        RotateArrow(arrow, c.type);
    }

    void SpawnFromRight(GameObject prefab, float y, int targetBeat)
    {
        GameObject arrow = Instantiate(
            prefab,
            new Vector3(rightX, y, 0),
            Quaternion.identity
        );

        ArrowController c = arrow.GetComponent<ArrowController>();

        NoteObject note = arrow.GetComponent<NoteObject>();

        note.conductor = conductor;
        note.SetTargetHitTime(targetBeat * conductor.secondsPerBeat);

        // Use the current speed instead of the constant speed
        float travelTime = conductor.secondsPerBeat * travelBeats;
        float distance = Mathf.Abs(rightX - centerX);

        c.speed = distance / travelTime;
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
