using UnityEngine;

public class IceCreamPowerUp : MonoBehaviour
{
    public float healAmount = 20f; // сколько хп восстанавливаем
    public float fallSpeed = 2f;   // базовая скорость падения

    private LoopingTilemap tilemap;

    private void Start()
    {
        // Находим LoopingTilemap на сцене
        tilemap = FindObjectOfType<LoopingTilemap>();
    }

    private void Update()
    {
        // Итоговая скорость падения = базовая + скорость скролла тайлов
        float scrollSpeed = tilemap != null ? tilemap.scrollSpeed : 0f;
        float totalFallSpeed = fallSpeed + scrollSpeed;

        // Двигаем мороженое вниз с учётом скорости скролла
        transform.position += -Camera.main.transform.up * totalFallSpeed * Time.deltaTime;

        // Удаляем, если вышло за нижнюю границу камеры
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize - 1f;
        if (transform.position.y < cameraBottom)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
            }

            Destroy(gameObject); // уничтожаем мороженое
        }
    }
}