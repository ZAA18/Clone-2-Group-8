using UnityEngine;

public class JudgementPopup : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float lifetime = 0.6f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
