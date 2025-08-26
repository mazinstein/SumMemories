using UnityEngine;

public class IceCreamPowerUp : MonoBehaviour
{
    public float healAmount = 20f; // сколько хп восстанавливаем

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
