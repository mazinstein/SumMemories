using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerHealth playerHealth;

    private void Update()
    {
        if (playerHealth == null || healthBar == null) return;

        float healthNormalized = playerHealth.currentHealth / playerHealth.maxHealth;
        healthBar.SetSize(healthNormalized);

        if (healthNormalized < 0.3f)
        {
            healthBar.SetColor(Color.red);
        }
        else if (healthNormalized < 0.6f)
        {
            healthBar.SetColor(Color.yellow);
        }
        else
        {
            healthBar.SetColor(Color.green);
        }
    }
}
