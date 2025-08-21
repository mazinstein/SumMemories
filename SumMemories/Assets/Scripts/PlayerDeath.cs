using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        float lowerBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        if (transform.position.y < lowerBound)
        {
            if (playerHealth != null)
            {
                playerHealth.currentHealth = 0; // сразу убиваем
                playerHealth.TakeDamage(playerHealth.maxHealth); // если хочешь триггер анимаций/событий
            }

            Debug.Log("Game Over!");
            // можно отключить управление или вызвать меню
            // Destroy(gameObject); ← если надо полностью убрать игрока
        }
    }
}
