 using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 3f;
    public float targetX = 0f;
    public bool fromLeft = true;
    public ArrowType type;

    [Header("Judgement")]
    public float missedDistance = 1f;
    public bool hasBeenHit = false;

    void Update()
    {
        if (fromLeft)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

           /* if (transform.position.x >= targetX)
            {
                transform.position = new Vector3(
                    targetX,
                    transform.position.y,
                    transform.position.z
                );

                Destroy(gameObject);
            } */

            if (!hasBeenHit && transform.position.x > targetX + missedDistance)
            {
                Miss();
            }
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

           /* if (transform.position.x <= targetX)
            {
                transform.position = new Vector3(
                    targetX,
                    transform.position.y,
                    transform.position.z
                );

                Destroy(gameObject);
            }
           */

            if (!hasBeenHit && transform.position.x < targetX - missedDistance)
            {
                Miss();
            }
        }
    }

    public void Hit(HitJudgement judgement)
    {
        if (hasBeenHit)
        {
            return;
        }

        hasBeenHit = true;
        Debug.Log("REGISTERING HIT: " + judgement);

        switch (judgement)
        {
            case HitJudgement.Perfect:
                GameManager.instance.PerfectHit();
                break;

            case HitJudgement.Good:
                GameManager.instance.GoodHit();
                break;

            case HitJudgement.Ok:
                GameManager.instance.NormalHit();
                break;
        }

        Destroy(gameObject);
    }

    private void Miss()
    {
        if (hasBeenHit)
        {
            return;
        }

        hasBeenHit = true;

        GameManager.instance.NoteMissed();

        Destroy(gameObject);
    }
}

public enum ArrowType
{
    Up,
    Down,
    Left,
    Right
}