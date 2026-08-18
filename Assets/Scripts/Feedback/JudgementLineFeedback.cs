using UnityEngine;
using UnityEngine.InputSystem;

public class JudgementLineFeedback : MonoBehaviour
{

    [Header("Judgement Popup")]
    public GameObject judgementPopupPrefab;
    public Transform judgementSpawnPoint;

    public Sprite perfectSprite;
    public Sprite goodSprite;
    public Sprite okSprite;

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
    public void ShowJudgement(HitJudgement judgement)
    {
        GameObject popup = Instantiate(
            judgementPopupPrefab,
            judgementSpawnPoint.position,
            Quaternion.identity
        );

        JudgementPopup popupScript =
            popup.GetComponent<JudgementPopup>();

        switch (judgement)
        {
            case HitJudgement.Perfect:
                popupScript.SetSprite(perfectSprite);
                break;

            case HitJudgement.Good:
                popupScript.SetSprite(goodSprite);
                break;

            case HitJudgement.Ok:
                popupScript.SetSprite(okSprite);
                break;
        }
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
