using UnityEngine;

public class ShadowSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] shadowPrefabs;
    public float spawnInterval = 3f;
    public Transform spawnTopLeft;
    public Transform spawnBottomRight;

    [Header("Shadow Settings")]
    public float shadowDuration = 10f;
    public float healPerSecond = 5f;

    [Header("Spawn Rules")]
    public float minDistance = 2f;   // минимальное расстояние между тенями
    public int maxAttempts = 10;     // сколько раз пробовать найти свободное место

    [Header("Tilemap Reference")]
    public LoopingTilemap loopingTilemap; // для скорости скролла

    void Start()
    {
        InvokeRepeating(nameof(SpawnShadow), 1f, spawnInterval);
    }

    void SpawnShadow()
    {
        if (shadowPrefabs.Length == 0 || spawnTopLeft == null || spawnBottomRight == null || loopingTilemap == null) return;

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            GameObject prefab = shadowPrefabs[Random.Range(0, shadowPrefabs.Length)];
            float x = Random.Range(spawnTopLeft.position.x, spawnBottomRight.position.x);
            float y = Random.Range(spawnBottomRight.position.y, spawnTopLeft.position.y);
            Vector2 spawnPos = new Vector2(x, y);

            // Проверка на минимальное расстояние
            Collider2D hit = Physics2D.OverlapCircle(spawnPos, minDistance);
            if (hit == null)
            {
                GameObject shadow = Instantiate(prefab, spawnPos, Quaternion.identity);

                // Скрипт движения с тайлмапом
                ShadowMover mover = shadow.AddComponent<ShadowMover>();
                mover.scrollSpeed = loopingTilemap.scrollSpeed;

                // HealZone
                HealZone healZone = shadow.GetComponent<HealZone>();
                if (healZone == null) healZone = shadow.AddComponent<HealZone>();
                healZone.healPerSecond = healPerSecond;

                // Коллайдер
                Collider2D col = shadow.GetComponent<Collider2D>();
                if (col == null) col = shadow.AddComponent<BoxCollider2D>();
                col.isTrigger = true;

                // Уничтожение через время
                Destroy(shadow, shadowDuration);

                break; // нашли свободное место → выходим из цикла
            }
        }
    }
}
