using UnityEngine;

public class ShadowMover : MonoBehaviour
{
    [HideInInspector] public float moveSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
