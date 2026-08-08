using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public InputActionReference keyToPress;


  /*  private void OnEnable()
    {
        keyToPress.action.Enable();
    }

    private void OnDisable()
    {
        keyToPress.action.Disable();
    }
  */

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if ( canBePressed)
            {
                gameObject.SetActive(false);
            }
        }

    }
    */

    private void Update()
    {
        if (keyToPress.action.WasPressedThisFrame())
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
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
        }
    }
}
