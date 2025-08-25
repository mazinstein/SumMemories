using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TMP_Text scoreText;
    public float scorePerSecond = 10f;

    public float pulseScale = 1.2f;
    public float pulseSpeed = 5f;

    private float score = 0f;
    private Vector3 originalScale;
    private bool pulse = false;
    private float pulseTimer = 0f;

    void Start()
    {
        if (scoreText != null)
            originalScale = scoreText.transform.localScale;
    }

    void Update()
    {
        if (playerHealth == null || playerHealth.currentHealth <= 0) return;

        // Начисляем очки
        score += scorePerSecond * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();

        // Пульс при обновлении
        pulse = true;

        if (pulse)
        {
            pulseTimer += Time.deltaTime * pulseSpeed;
            float scale = Mathf.Lerp(1f, pulseScale, Mathf.Sin(pulseTimer * Mathf.PI));
            scoreText.transform.localScale = originalScale * scale;

            if (pulseTimer >= 1f)
            {
                pulse = false;
                pulseTimer = 0f;
                scoreText.transform.localScale = originalScale;
            }
        }
    }

    // 🔹 Добавляем этот метод
    public float GetScore()
    {
        return score;
    }
}
