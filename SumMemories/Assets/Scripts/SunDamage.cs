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

        // Новый способ поиска (Unity 2023+)
        sunlightEffect = Object.FindFirstObjectByType<SunlightEffect>();
    }

    void Update()
    {
        if (!inShadow && health != null && health.currentHealth > 0)
        {
            // Наносим урон через PlayerHealth
            health.TakeDamage(damagePerSecond * Time.deltaTime);
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
