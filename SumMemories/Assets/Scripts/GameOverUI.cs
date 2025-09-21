using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup panelGroup;      // фон меню
    public CanvasGroup buttonsGroup;    // контейнер кнопок
    public TMP_Text gameOverText;       // надпись "GAME OVER"
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    [Header("Animation Settings")]
    public float fadeDuration = 0.5f;
    public float buttonDelay = 0.3f;

    [Header("Pulse Settings")]
    public float pulseScale = 1.2f;
    public float pulseSpeed = 2f;

    private Vector3 originalScale;
    private Vector3 scoreOriginalScale;
    private Vector3 bestScoreOriginalScale;
    private bool isPulsing = false;
    private bool isScorePulsing = false;
    private bool isBestScorePulsing = false;

    private float scoreFade = 0f;
    private float bestScoreFade = 0f;

    public ScoreManager scoreManager; // Добавьте ссылку через инспектор или найдите на сцене

    private void Awake()
    {
        // Меню скрыто в начале
        panelGroup.alpha = 0;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;

        buttonsGroup.alpha = 0;
        buttonsGroup.interactable = false;
        buttonsGroup.blocksRaycasts = false;

        if (gameOverText != null)
            originalScale = gameOverText.transform.localScale;
        if (scoreText != null)
            scoreOriginalScale = scoreText.transform.localScale;
        if (bestScoreText != null)
            bestScoreOriginalScale = bestScoreText.transform.localScale;
    }

    public void ShowGameOver()
    {
        // Сохраняем лучший результат
        scoreManager.TrySetBestScore();

        // Показываем очки
        int currentScore = Mathf.FloorToInt(scoreManager.GetScore());
        int bestScore = scoreManager.GetBestScore();
        scoreText.text = $"Score: {currentScore}";
        bestScoreText.text = $"Best: {bestScore}";

        scoreText.alpha = 0f;
        scoreText.transform.localScale = scoreOriginalScale * 0.7f;
        bestScoreText.alpha = 0f;
        bestScoreText.transform.localScale = bestScoreOriginalScale * 0.7f;
        StartCoroutine(FadeInMenu());
        StartCoroutine(AnimateScoreText());
        StartCoroutine(AnimateBestScoreText());
    }

    private IEnumerator FadeInMenu()
    {
        // Плавное появление панели
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            panelGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }
        panelGroup.alpha = 1;
        panelGroup.interactable = true;
        panelGroup.blocksRaycasts = true;

        // Включаем пульсацию
        isPulsing = true;
        isScorePulsing = true;
        isBestScorePulsing = true;

        // Задержка перед кнопками
        yield return new WaitForSecondsRealtime(buttonDelay);

        // Плавное появление кнопок
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            buttonsGroup.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }
        buttonsGroup.alpha = 1;
        buttonsGroup.interactable = true;
        buttonsGroup.blocksRaycasts = true;
    }

    private void Update()
    {
        if (isPulsing && gameOverText != null)
        {
            float scale = 1f + Mathf.Sin(Time.unscaledTime * pulseSpeed) * (pulseScale - 1f);
            gameOverText.transform.localScale = originalScale * scale;
        }
        if (isScorePulsing && scoreText != null)
        {
            float scale = 1f + Mathf.Sin(Time.unscaledTime * pulseSpeed) * (pulseScale - 1f);
            scoreText.transform.localScale = scoreOriginalScale * scale;
        }
        if (isBestScorePulsing && bestScoreText != null)
        {
            float scale = 1f + Mathf.Sin(Time.unscaledTime * pulseSpeed) * (pulseScale - 1f);
            bestScoreText.transform.localScale = bestScoreOriginalScale * scale;
        }
    }

    // 🔹 Методы для кнопок
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator AnimateScoreText()
    {
        float t = 0f;
        float duration = 0.6f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float progress = Mathf.SmoothStep(0, 1, t / duration);
            scoreText.alpha = progress;
            scoreText.transform.localScale = Vector3.Lerp(scoreOriginalScale * 0.7f, scoreOriginalScale, progress);
            yield return null;
        }
        scoreText.alpha = 1f;
        scoreText.transform.localScale = scoreOriginalScale;
    }

    private IEnumerator AnimateBestScoreText()
    {
        float t = 0f;
        float duration = 0.6f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float progress = Mathf.SmoothStep(0, 1, t / duration);
            bestScoreText.alpha = progress;
            bestScoreText.transform.localScale = Vector3.Lerp(bestScoreOriginalScale * 0.7f, bestScoreOriginalScale, progress);
            yield return null;
        }
        bestScoreText.alpha = 1f;
        bestScoreText.transform.localScale = bestScoreOriginalScale;
    }
}
