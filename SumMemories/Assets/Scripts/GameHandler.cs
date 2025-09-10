using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [Header("References (drag in Inspector)")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameOverUI gameOverUI;

    private bool isGameOver = false;

    private void Awake()
    {
        // Авто-поиск, если не привязали в инспекторе
        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();
        if (healthBar == null)
            healthBar = FindObjectOfType<HealthBar>();
    }

    private void Start()
    {
        if (playerHealth != null)
            playerHealth.OnDeath += HandleGameOver;
    }

    private void Update()
    {
        if (isGameOver) return;
        if (playerHealth == null || healthBar == null) return;

        float healthNormalized = playerHealth.currentHealth / playerHealth.maxHealth;
        healthBar.SetSize(Mathf.Clamp01(healthNormalized));

        if (healthNormalized < 0.3f)
            healthBar.SetColor(Color.red);
        else if (healthNormalized < 0.6f)
            healthBar.SetColor(Color.yellow);
        else
            healthBar.SetColor(Color.green);
    }

    private void HandleGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        // Ставим паузу (игра останавливается, но UI-анимация должна использовать unscaled time)
        Time.timeScale = 0f;

        if (gameOverUI != null)
            gameOverUI.ShowGameOver();
        else
            Debug.LogWarning("GameOverUI not assigned in GameHandler!");
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnDeath -= HandleGameOver;
    }
}
