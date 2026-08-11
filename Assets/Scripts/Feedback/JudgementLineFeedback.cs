using UnityEngine;
using UnityEngine.InputSystem;

public class JudgementLineFeedback : MonoBehaviour
{
    public InputActionReference inputAction;

    public float pressedScale = 0.9f;

    private Vector3 normalScale;
   


    private void Awake()
    {
        normalScale = transform.localScale;
    }


    private void OnEnable()
    {
        inputAction.action.started += OnPressed;
        inputAction.action.canceled += OnReleased;
    }


    private void OnDisable()
    {
        inputAction.action.started -= OnPressed;
        inputAction.action.canceled -= OnReleased;
    }


    private void OnPressed(InputAction.CallbackContext context)
    {
        transform.localScale = normalScale * pressedScale;
    }


    private void OnReleased(InputAction.CallbackContext context)
    {
        transform.localScale = normalScale;
    }
}
