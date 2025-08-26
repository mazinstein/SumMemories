using UnityEngine;

public class IceCreamPowerUp : MonoBehaviour
{
    public float healAmount = 20f; // сколько хп восстанавливаем
    public float fallSpeed = 2f;   // скорость падения

    private void Update()
    {
        // Двигаем мороженое вниз относительно камеры
        transform.position += -Camera.main.transform.up * fallSpeed * Time.deltaTime;

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