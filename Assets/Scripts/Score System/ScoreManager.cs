using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // ref so other scripts can use ScoreManager.Instance
    public static ScoreManager Instance;

    // players current score 
    public int score = 0;

    // players current combo
    public int combo = 0;

    // players highest combo 
    public int maxCombo = 0;

    // how many perfect hits
    public int perfectHits = 0;

    // how many good hits
    public int goodHits = 0;

    // how many okay hits
    public int okHits = 0;

    // how many misses
    public int misses = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterHit(HitJudgement judgement)
    {
        switch (judgement)
        {
            case HitJudgement.Perfect:
                score += 1000;
                combo++;
                perfectHits++;
                break;


            case HitJudgement.Miss:
                combo = 0;
                misses++;
                break;

            case HitJudgement.Good:
                score += 500;
                combo++;
                goodHits++;
                break;

            case HitJudgement.Ok:
                score += 200;
                combo++;
                okHits++;
                break;

        }

        if (combo > maxCombo)
        {
            maxCombo = combo;
        }
    }

    public float GetAccuracy()
    {
        int totalHits =
            perfectHits +
            goodHits +
            okHits +
            misses;

        if (totalHits == 0)
        {
            return 100f;
        }

        float earnedPoints =
            (perfectHits * 1f) +
            (goodHits * 0.5f) +
            (okHits * 0.2f);

        return (earnedPoints / totalHits) * 100f;
    }
}
