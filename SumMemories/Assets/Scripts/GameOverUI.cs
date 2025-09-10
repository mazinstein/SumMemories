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
    private bool isPulsing = false;

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
    }

    public void ShowGameOver()
    {
        StartCoroutine(FadeInMenu());
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
}
