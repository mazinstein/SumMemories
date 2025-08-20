using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 2f;

    void Update()
    {
        // Двигаем камеру вверх каждый кадр
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
    }
}
