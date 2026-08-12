using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text comboText;
    [SerializeField] TMP_Text accuracyText;
    [SerializeField] Slider healthBar;
    [SerializeField] Image judgementImage;
    [SerializeField] CanvasGroup judgementCanvasGroup;

    [Header("Sprites")]
    public Sprite perfectSprite;
    public Sprite goodSprite;
    public Sprite okSprite;
    public Sprite missSprite;

    public float displayTime = 0.3f;

    public float fadeTime = 0.2f;

    private Coroutine judgementCoroutine;


    private void Update()
    {
        if (ScoreManager.Instance == null)
        {
            return;
        }

        scoreText.text =
            "Score: " + ScoreManager.Instance.score;

        comboText.text =
            "Combo: x " + ScoreManager.Instance.combo;

        accuracyText.text =
            "Accuracy: " +
            ScoreManager.Instance.GetAccuracy().ToString("F2") +
            "%";
    }

    public void UpdateHealthBar(float health)
    {
       // healthBar.value = currentHealth;
    }

    public void ShowJudgement(HitJudgement judgement)
    {
        switch (judgement)
        {
            case HitJudgement.Perfect:
                judgementImage.sprite = perfectSprite;
                break;

            case HitJudgement.Good:
                judgementImage.sprite = goodSprite;
                break;

            case HitJudgement.Ok:
                judgementImage.sprite = okSprite;
                break;

            case HitJudgement.Miss:
                judgementImage.sprite = missSprite;
                break;
        }

        if (judgementCoroutine != null)
        {
            StopCoroutine(judgementCoroutine);
        }

        judgementCoroutine = StartCoroutine(DisplayJudgement());
    }

    private IEnumerator DisplayJudgement()
    {
        judgementImage.gameObject.SetActive(true);
       

        judgementCanvasGroup.alpha = 1f;
       

        yield return new WaitForSeconds(displayTime);
        


        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            judgementCanvasGroup.alpha =
                Mathf.Lerp(1f, 0f, timer / fadeTime);

         

            yield return null;
        }


        judgementCanvasGroup.alpha = 0f;
        judgementImage.gameObject.SetActive(false);
    
    }
}
