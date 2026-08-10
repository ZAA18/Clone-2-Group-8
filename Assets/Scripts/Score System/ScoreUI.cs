using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreUI : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text comboText;
    [SerializeField] TMP_Text accuracyText;
    [SerializeField] Slider healthBar;


    private void Update()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.score;
        comboText.text = "Combo: x " + ScoreManager.Instance.combo;
    }

    public void UpdateAccuracy(float accuracy)
    {
        // F2 displays number with 2 decimals
        accuracyText.text = "Accuracy: " + accuracy.ToString("F2") + "%";
    }

    public void UpdateHealthBar(float health)
    {
       // healthBar.value = currentHealth;
    }
}
