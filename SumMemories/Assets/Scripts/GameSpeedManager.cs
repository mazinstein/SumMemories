using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    [Header("Ссылки")]
    public PlayerController playerController;  // скрипт движения игрока
    public LoopingTilemap loopingTilemap;      // скрипт скролла карты
    public ScoreManager scoreManager;          // счёт очков

    [Header("Базовые скорости")]
    public float basePlayerSpeed = 5f;
    public float baseScrollSpeed = 2f;

    [Header("Прирост скорости")]
    public float playerSpeedPerPoint = 0.01f;  // прирост скорости игрока за 1 очко
    public float scrollSpeedPerPoint = 0.005f; // прирост скролла тайлмапа за 1 очко

    void Update()
    {
        if (scoreManager == null || playerController == null || loopingTilemap == null) return;

        float currentScore = scoreManager.GetScore();

        // Плавное увеличение скоростей
        playerController.moveSpeed = basePlayerSpeed + currentScore * playerSpeedPerPoint;
        loopingTilemap.scrollSpeed = baseScrollSpeed + currentScore * scrollSpeedPerPoint;
    }
}
