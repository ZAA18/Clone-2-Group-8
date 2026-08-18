using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

        currentCombo++;
        comboText.text = "Combo: " + currentCombo;

        UpdateAccuracy(0.50f);

        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;

        currentCombo++;
        comboText.text = "Combo: " + currentCombo;

        UpdateAccuracy(0.75f);

        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;

        currentCombo++;
        comboText.text = "Combo: " + currentCombo;

        UpdateAccuracy(1.00f);

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
    }
}
