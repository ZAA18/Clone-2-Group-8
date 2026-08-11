using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public InputActionReference keyToPress;

    private void Update()
    {
        if (keyToPress.action.WasPressedThisFrame())
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                //telling the game manager we hit the note
                GameManager.instance.NoteHit();
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
