using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class SunDamage : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damagePerSecond = 10f;

    private PlayerHealth health;
    private bool inShadow = false;
    private SunlightEffect sunlightEffect;

    void Start()
    {
        health = GetComponent<PlayerHealth>();

        // Новый способ поиска
        sunlightEffect = Object.FindFirstObjectByType<SunlightEffect>();
    }

    void Update()
    {
        if (!inShadow)
        {
            health.currentHealth -= damagePerSecond * Time.deltaTime;

            if (health.currentHealth <= 0)
            {
                Debug.Log("Game Over!");
                Destroy(gameObject); // потом заменишь на вызов GameManager
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HealZone>() != null)
        {
            inShadow = true;
            sunlightEffect?.SetInShadow(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<HealZone>() != null)
        {
            inShadow = false;
            sunlightEffect?.SetInShadow(false);
        }
    }
}
