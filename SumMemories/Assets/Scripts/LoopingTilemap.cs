using UnityEngine;
using UnityEngine.Tilemaps;

public class LoopingTilemap : MonoBehaviour
{
    public Transform segment1;       // первый сегмент (Grid)
    public Transform segment2;       // второй сегмент (Grid)
    public float scrollSpeed = 2f;   // скорость движения

    private float segmentHeight;

    void Start()
    {
        // автоматом вычисляем высоту сегмента
        segmentHeight = GetSegmentHeight(segment1.gameObject);

        // ставим второй сегмент ровно над первым
        segment2.position = segment1.position + Vector3.up * segmentHeight;
    }

    void Update()
    {
        // двигаем оба вниз
        segment1.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        segment2.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // если сегмент ушёл вниз за камеру → переносим его наверх
        if (segment1.position.y < Camera.main.transform.position.y - segmentHeight)
        {
            segment1.position = segment2.position + Vector3.up * segmentHeight;
            SwapSegments();
        }
        if (segment2.position.y < Camera.main.transform.position.y - segmentHeight)
        {
            segment2.position = segment1.position + Vector3.up * segmentHeight;
            SwapSegments();
        }
    }

    // меняем местами ссылки, чтобы всегда знать какой верхний/нижний
    void SwapSegments()
    {
        Transform tmp = segment1;
        segment1 = segment2;
        segment2 = tmp;
    }

    private float GetSegmentHeight(GameObject segment)
    {
        Tilemap[] tilemaps = segment.GetComponentsInChildren<Tilemap>();
        Bounds bounds = new Bounds(tilemaps[0].localBounds.center, tilemaps[0].localBounds.size);
        foreach (var tm in tilemaps) bounds.Encapsulate(tm.localBounds);
        return bounds.size.y;
    }
}
