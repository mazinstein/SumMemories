using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Слежение за игроком")]
    public Transform player;
    public float moveSpeed = 2f;

    [Header("PlayerHealth для смерти")]
    public PlayerHealth playerHealth;

    void Update()
    {
        if (player == null) return;

        // Двигаем камеру вверх плавно, поджимая игрока
        Vector3 targetPos = new Vector3(transform.position.x, Mathf.Max(transform.position.y, player.position.y), transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // Нижняя граница камеры
        float lowerBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        // Если игрок опустился ниже → убиваем через TakeDamage
        if (player.position.y < lowerBound && playerHealth != null && playerHealth.currentHealth > 0f)
        {
            playerHealth.TakeDamage(playerHealth.currentHealth); // здоровье = 0
            Debug.Log("Game Over! Игрок коснулся нижней границы.");
        }
    }
}
