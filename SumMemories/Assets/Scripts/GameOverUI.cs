using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References (assign in Inspector)")]
    public CanvasGroup panelGroup;      // фон / панель
    public CanvasGroup buttonsGroup;    // контейнер для кнопок (Restart / Menu)

    [Header("Animation Settings")]
    public float fadeDuration = 0.5f;
    public float buttonDelay = 0.25f;

    private void Awake()
    {
        // безопасные null-проверки
        if (panelGroup != null)
        {
            panelGroup.alpha = 0f;
            panelGroup.interactable = false;
            panelGroup.blocksRaycasts = false;
        }

        if (buttonsGroup != null)
        {
            buttonsGroup.alpha = 0f;
            buttonsGroup.interactable = false;
            buttonsGroup.blocksRaycasts = false;
        }

        // по умолчанию объект может быть активен или не активен — не важно
        // но визуально скрываем элементы
    }

    // Вызывается из GameHandler; использует unscaled time чтобы анимация шла в паузе
    public void ShowGameOver()
    {
        gameObject.SetActive(true);
        StartCoroutine(FadeInSequence());
    }

    private IEnumerator FadeInSequence()
    {
        if (panelGroup != null)
        {
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                panelGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
                yield return null;
            }
            panelGroup.alpha = 1f;
            panelGroup.interactable = true;
            panelGroup.blocksRaycasts = true;
        }

        // задержка перед кнопками (немного паузы, чтобы выглядело ровнее)
        yield return new WaitForSecondsRealtime(buttonDelay);

        if (buttonsGroup != null)
        {
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                buttonsGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
                yield return null;
            }
            buttonsGroup.alpha = 1f;
            buttonsGroup.interactable = true;
            buttonsGroup.blocksRaycasts = true;
        }
    }

    // UI-кнопки -> назначить эти методы
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu(string menuSceneName = "MainMenu")
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}
