using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public PlayerController playerController;
    public TMP_Text scoreText;

    [Header("Настройки очков")]
    public int scorePerUnit = 1;       // сколько очков даёт движение
    public int scoreStepForSpeed = 100; // через сколько очков увеличиваем скорость

    private float score = 0f;
    private Vector3 originalScale;
    public float pulseScale = 1.2f;
    public float pulseSpeed = 5f;
    private bool pulse = false;
    private float pulseTimer = 0f;

    void Start()
    {
        if (scoreText != null)
            originalScale = scoreText.transform.localScale;
    }

    void Update()
    {
        if (playerController == null || scoreText == null) return;

        // Начисляем очки только если есть движение
        if (playerController.movement.sqrMagnitude > 0)
        {
            score += scorePerUnit * Time.deltaTime;
            scoreText.text = Mathf.FloorToInt(score).ToString();

            // Пульс при обновлении
            pulse = true;
        }

        // Плавная пульсация
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

    public float GetScore()
    {
        return score;
    }

    public int GetSpeedStep()
    {
        // Возвращаем сколько “шагов скорости” уже пройдено
        return Mathf.FloorToInt(score / scoreStepForSpeed);
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore", 0);
    }

    public void TrySetBestScore()
    {
        int currentScore = Mathf.FloorToInt(score);
        int bestScore = GetBestScore();
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            PlayerPrefs.Save();
        }
    }
}
