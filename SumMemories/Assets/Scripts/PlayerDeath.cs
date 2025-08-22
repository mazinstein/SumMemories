using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private HealthBar healthBar;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    void Update()
    {
        // Нижняя граница камеры
        float lowerBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        // Если игрок опустился ниже
        if (transform.position.y < lowerBound)
        {
            if (playerHealth != null)
                playerHealth.currentHealth = 0f;

            if (healthBar != null)
            {
                healthBar.SetSize(0f);
                healthBar.SetColor(Color.red);
            }

            Debug.Log("Game Over! Игрок коснулся нижней границы.");
            Destroy(gameObject);
        }
    }
}
