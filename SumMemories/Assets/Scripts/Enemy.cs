using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float moveSpeed = 2f;
    public int damage = 10;
    public float flashDuration = 1f;      // сколько секунд мигает
    public float flashInterval = 0.1f;    // частота мигания

    private float leftBoundaryX;
    private bool canDamage = true;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        Camera cam = Camera.main;
        float camWidth = 2f * cam.orthographicSize * cam.aspect;
        leftBoundaryX = cam.transform.position.x - camWidth / 2f - 1f;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < leftBoundaryX)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDamage && collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        canDamage = false;
        float timer = 0f;
        bool visible = true;

        while (timer < flashDuration)
        {
            visible = !visible;
            if (spriteRenderer != null)
                spriteRenderer.enabled = visible;

            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        canDamage = true;
    }
}