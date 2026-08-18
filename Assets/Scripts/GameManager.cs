using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

  //  public BeatScroller theBS;
    public Conductor conductor;

    public InputActionReference startGame;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;

    //tagerting perfect score
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public Text scoreText;
    public Text multiText;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    [Header("Combo")]
    public int currentCombo;
    public Text comboText;

    [Header("Accuracy")]
    public int totalNotesJudged;
    public float totalAccuracyPoints;
    public Text accuracyText;

    [Header("Health")]
    public Slider healthBar;

    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public float perfectHealthGain = 2f;
    public float goodHealthGain = 1f;
    public float okHealthGain = 0.5f;

    public float missHealthLoss = 10f;

    private bool gameOver = false;

    [Header("Results Screen")]
    public GameObject resultsPanel;

    public Text resultTitleText;
    public Text finalScoreText;
    public Text finalAccuracyText;
    public Text finalComboText;
    public Text rankText;

    public int maxCombo;

    [Header("Judgement Counts")]
    public int perfectCount;
    public int goodCount;
    public int okCount;
    public int missCount;

    public Text resultPerfectText;
    public Text resultGoodText;
    public Text resultOkText;
    public Text resultMissText;

    // The multipliertracker still has an issue... its not changing I am not sure of what i did

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";

        currentMultiplier = 1;
        currentCombo = 0;

        multiText.text = "Multiplier: x1";
        comboText.text = "Combo: 0";
        accuracyText.text = "Accuracy: 100.00%";

        currentHealth = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)

        {
            if (startGame.action.WasPressedThisFrame())
            {
                startPlaying = true;
                //  theBS.hasStarted = true;
                conductor.StartSong();
            }

        }

        if (startPlaying && !gameOver)
        {
            if (!conductor.IsSongPlaying() && conductor.songPosition > 1f)
            {
                CompleteSong();
            }
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main Menu");
    }

    public void RetrySong()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    private void CompleteSong()
    {
        if (gameOver)
            return;

        gameOver = true;

        Debug.Log("SONG COMPLETE");

        ShowResults(false);

        Time.timeScale = 0f;
    }

    private void ShowResults(bool failed)
    {
        float finalAccuracy = GetFinalAccuracy();
        string finalRank = GetRank(finalAccuracy, failed);

        resultsPanel.SetActive(true);

        if (failed)
        {
            resultTitleText.text = "FAILED";
        }
        else
        {
            resultTitleText.text = "SONG COMPLETE";
        }

        finalScoreText.text = "Score: " + currentScore;
        finalAccuracyText.text = "Accuracy: " + finalAccuracy.ToString("F2") + "%";
        finalComboText.text = "Max Combo: " + maxCombo;
        rankText.text = "Rank: " + finalRank;
        resultPerfectText.text = "Perfect: " + perfectCount;
        resultGoodText.text = "Good: " + goodCount;
        resultOkText.text = "Ok: " + okCount;
        resultMissText.text = "Miss: " + missCount;
    }
    private string GetRank(float accuracy, bool failed)
    {
        if (failed)
            return "F";

        if (accuracy >= 95f)
            return "S";

        if (accuracy >= 90f)
            return "A";

        if (accuracy >= 80f)
            return "B";

        if (accuracy >= 70f)
            return "C";

        if (accuracy >= 60f)
            return "D";

        return "F";
    }

    private float GetFinalAccuracy()
    {
        if (totalNotesJudged <= 0)
            return 0f;

        return (totalAccuracyPoints / totalNotesJudged) * 100f;
    }

    private void FailSong()
    {
        if (gameOver)
            return;

        gameOver = true;

        Debug.Log("SONG FAILED");

        conductor.StopSong();

        ShowResults(true);

        Time.timeScale = 0f;
    }

    private void GainHealth(float amount)
    {
        if (gameOver)
            return;

        currentHealth += amount;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0f,
            maxHealth
        );

        healthBar.value = currentHealth;
    }

    private void LoseHealth(float amount)
    {
        if (gameOver)
            return;

        currentHealth -= amount;

        currentHealth = Mathf.Clamp(
            currentHealth,
            0f,
            maxHealth
        );

        healthBar.value = currentHealth;

        if (currentHealth <= 0f)
        {
            FailSong();
        }
    }

    private void UpdateAccuracy(float accuracyValue)
    {
        totalNotesJudged++;
        totalAccuracyPoints += accuracyValue;

        float accuracy = 0f;

        if (totalNotesJudged > 0)
        {
            accuracy = (totalAccuracyPoints / totalNotesJudged) * 100f;
        }

        accuracyText.text = "Accuracy: " + accuracy.ToString("F2") + "%";
    }

    public void NoteHit()
    {
        Debug.Log("hit on time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                currentMultiplier++;
            }

        }

            multiText.text = "Multiplier: x" + currentMultiplier;
            scoreText.text = "Score: " + currentScore;
        
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;

        okCount++;
        if (currentCombo > maxCombo)
        {
            maxCombo = currentCombo;
        }

        comboText.text = "Combo: " + currentCombo;

        UpdateAccuracy(0.50f);

        GainHealth(okHealthGain);


        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;

        goodCount++;

        if (currentCombo > maxCombo)
        {
            maxCombo = currentCombo;
        }

        comboText.text = "Combo: " + currentCombo;

        UpdateAccuracy(0.75f);

        GainHealth(goodHealthGain);

        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;


        perfectCount++;

        if (currentCombo > maxCombo)
        {
            maxCombo = currentCombo;
        }

        comboText.text = "Combo: " + currentCombo;

        UpdateAccuracy(1.00f);

        GainHealth(perfectHealthGain);

        NoteHit();
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        currentCombo = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;
        comboText.text = "Combo: " + currentCombo;

        UpdateAccuracy(0f);

        LoseHealth(missHealthLoss);

        missCount++;
    }
}
