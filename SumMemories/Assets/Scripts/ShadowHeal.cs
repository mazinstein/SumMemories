using UnityEngine;

public class ShadowHeal : MonoBehaviour
{
    public float healPerSecond = 5f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healPerSecond * Time.deltaTime);
            }
        }
    }
}
