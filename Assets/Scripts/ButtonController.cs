using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public InputActionReference keyToPress;
  
    private void Awake()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    
    private void OnEnable()
    {
        keyToPress.action.Enable();

        keyToPress.action.performed += OnKeyPressed;
        keyToPress.action.canceled += OnKeyReleased;

    }

    private void OnDisable()
    {
        keyToPress.action.performed -= OnKeyPressed;
        keyToPress.action.canceled -= OnKeyReleased;

        keyToPress.action.Disable();
    }

    private void OnKeyPressed (InputAction.CallbackContext context)
    {
        theSR.sprite = pressedImage;
    }

    private void OnKeyReleased(InputAction.CallbackContext context)
    {
        theSR.sprite = defaultImage;
    }
}
