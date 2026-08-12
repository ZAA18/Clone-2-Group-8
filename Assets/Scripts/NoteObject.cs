using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public InputActionReference keyToPress;

    // for hit system Display
    public GameObject hitEffect, goodEffect, missEffect;



    private void Update()
    {
        if (keyToPress.action.WasPressedThisFrame())
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

                } 
                
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Goodhit");
                    GameManager.instance.GoodHit();
                }

                else
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                }

                // we are at -0.25 we still want it to be normal

            }
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
           GameManager.instance.NoteMissed();
        }
    }
}
