using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    void Update()
    {
        float lowerBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        if (transform.position.y < lowerBound)
        {
            Debug.Log("Game Over!");
            // Здесь можно вызвать Game Over экран или рестарт сцены
            Destroy(gameObject);
        }
    }
}
