using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class NoteObject : MonoBehaviour
{
    private static List<NoteObject> activeNotes = new List<NoteObject>();

    [Header("Input")]
    public InputActionReference keyToPress;

    [Header("HitDetection")]
    public bool canBePressed;

    [Header("Rhythm Timing")]
    public Conductor conductor;

    private float targetHitTime;

    //dustance from judgement line for each
    public float perfectWindow = 0.15f;
    public float goodWindow = 0.35f;
    public float okWindow = 0.6f;

    private ArrowController arrowController;

    private void Awake()
    {
        arrowController = GetComponent<ArrowController>();
    }

    private void OnEnable()
    {
        activeNotes.Add(this);
    }

    private void OnDisable()
    {
        activeNotes.Remove(this);
    }

    private bool IsClosestHittableNote()
    {
        NoteObject closestNote = null;
        float closestTimingDifference = Mathf.Infinity;

        foreach (NoteObject note in activeNotes)
        {
            if (note == null)
                continue;

            if (!note.canBePressed)
                continue;

            // Only compare notes with the same arrow direction
            if (note.arrowController.type != arrowController.type)
                continue;

            float timingDifference =
                Mathf.Abs(note.conductor.songPosition - note.targetHitTime);

            if (timingDifference < closestTimingDifference)
            {
                closestTimingDifference = timingDifference;
                closestNote = note;
            }
        }

        return closestNote == this;
    }

    private void Update()
    {

        if (keyToPress.action.WasPressedThisFrame())
        {
            if (canBePressed && IsClosestHittableNote())
            {
                Debug.Log("HITTING CLOSEST NOTE: " + gameObject.name);

                JudgeHit();
            }
        }
    }
    public void SetTargetHitTime(float time)
    {
        targetHitTime = time;
    }

    private void JudgeHit()
    {
        float timingDifference = Mathf.Abs(conductor.songPosition - targetHitTime);

        Debug.Log("Timing Difference: " + timingDifference);

        if (timingDifference <= perfectWindow)
        {
            Debug.Log("Perfect");
            arrowController.Hit(HitJudgement.Perfect);
        }
        else if (timingDifference <= goodWindow)
        {
            Debug.Log("Good");
            arrowController.Hit(HitJudgement.Good);
        }
        else if (timingDifference <= okWindow)
        {
            Debug.Log("Ok");
            arrowController.Hit(HitJudgement.Ok);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;

            Debug.Log("ENTERED ACTIVATOR: " + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;

            Debug.Log("LEFT ACTIVATOR: " + gameObject.name);
        }
    }

}
