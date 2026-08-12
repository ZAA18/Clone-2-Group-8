using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference keyToPress;

    [Header("HitDetection")]
    public bool canBePressed;

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
        keyToPress.action.Enable();
    }

    private void OnDisable()
    {
        keyToPress.action.Disable();
    }

    private void Update()
    {
        /* if (keyToPress.action.WasPressedThisFrame())
         {
             if (canBePressed)
             {
                 gameObject.SetActive(false);

                 //telling the game manager we hit the note
                 //GameManager.instance.NoteHit();

                 //checking for normal hit which is 0.25 on a line

                 if (Mathf.Abs(transform.position. y) > 0.25)
                 {
                     Debug.Log("Normal hit");
                     GameManager.instance.NormalHit();
                     Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);

                 } 

                 else if (Mathf.Abs(transform.position.y) > 0.05f)
                 {
                     Debug.Log("Goodhit");
                     GameManager.instance.GoodHit();
                     Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                 }

                 else
                 {
                     Debug.Log("Perfect");
                     GameManager.instance.PerfectHit();
                     Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                 }

                 // we are at -0.25 we still want it to be normal

             }
         } */

        if (!keyToPress.action.WasPressedThisFrame())
        {
            return;
        }

        if (!canBePressed)
        {
            return;
        }

        JudgeHit();
    }

    private void JudgeHit()
    {
        float distanceFromLine = Mathf.Abs(transform.position.x - arrowController.targetX);

        if (distanceFromLine <= perfectWindow)
        {
            Debug.Log("Perfect");
            arrowController.Hit(HitJudgement.Perfect);
        }
        else if (distanceFromLine <= goodWindow)
        {
            Debug.Log("Good");
            arrowController.Hit(HitJudgement.Good);
        }
        else if (distanceFromLine <= okWindow)
        {
            Debug.Log("Ok");
            arrowController.Hit(HitJudgement.Ok); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Activator"))
        {
            canBePressed = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;
        }
    }
   
}
