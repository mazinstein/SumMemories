using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    public PlayerController playerController;
    public LoopingTilemap loopingTilemap;
    public ScoreManager scoreManager;

    public float basePlayerSpeed = 5f;
    public float baseScrollSpeed = 2f;
    public float playerSpeedPerStep = 0.5f;
    public float scrollSpeedPerStep = 0.2f;

    public float maxPlayerSpeed = 15f;
    public float maxScrollSpeed = 6f;

    public float smoothTime = 1f;

    private float playerVelocity = 0f;
    private float scrollVelocity = 0f;
    private int lastSpeedStep = 0;

    void Update()
    {
        if (scoreManager == null || playerController == null || loopingTilemap == null) return;

        int currentStep = scoreManager.GetSpeedStep();

        if (currentStep > lastSpeedStep)
        {
            lastSpeedStep = currentStep;
        }

        float targetPlayerSpeed = Mathf.Min(basePlayerSpeed + lastSpeedStep * playerSpeedPerStep, maxPlayerSpeed);
        float targetScrollSpeed = Mathf.Min(baseScrollSpeed + lastSpeedStep * scrollSpeedPerStep, maxScrollSpeed);

        playerController.moveSpeed = Mathf.SmoothDamp(playerController.moveSpeed, targetPlayerSpeed, ref playerVelocity, smoothTime);
        loopingTilemap.scrollSpeed = Mathf.SmoothDamp(loopingTilemap.scrollSpeed, targetScrollSpeed, ref scrollVelocity, smoothTime);
    }
}
