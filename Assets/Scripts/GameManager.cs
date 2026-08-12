using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)

        {
            if (startGame.action.WasPressedThisFrame())
            {
                startPlaying = true;
                theBS.hasStarted = true;
                theMusic.Play();
            }

        }
    }

    public void NoteHit()
    {
        Debug.Log("hit on time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker++;
                currentMultiplier++;
            }

        }

         multiText.text = "Multiplier: x" + currentMultiplier;

            //currentScore += scorePerNote * currentMultiplier;
           // currentScore += scorePerNote;
            scoreText.text = "Score: " + currentScore;
        
    }

    public void NormalHit()
    {
        NoteHit();

    }

    public void GoodHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    public void PerfectHit()
    {

    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;
    }
}
