using UnityEngine;

public class ShadowMover : MonoBehaviour
{
    public float scrollSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
    }
}
