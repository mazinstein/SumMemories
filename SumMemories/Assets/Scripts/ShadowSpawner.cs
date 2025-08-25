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

    [Header("Ссылка на тайлмап для скорости")]
    public LoopingTilemap loopingTilemap;

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

            // Спавним **выше камеры**, чтобы игрок не видел появление
            float x = Random.Range(spawnTopLeft.position.x, spawnBottomRight.position.x);
            float y = Camera.main.transform.position.y + Camera.main.orthographicSize + Random.Range(1f, 3f); 
            Vector2 spawnPos = new Vector2(x, y);

            // Проверка на минимальное расстояние
            Collider2D hit = Physics2D.OverlapCircle(spawnPos, minDistance);
            if (hit == null)
            {
                GameObject shadow = Instantiate(prefab, spawnPos, Quaternion.identity);

                // Движение вниз
                ShadowMover mover = shadow.GetComponent<ShadowMover>();
                if (mover == null) mover = shadow.AddComponent<ShadowMover>();
                mover.moveSpeed = loopingTilemap.scrollSpeed;

                // HealZone
                HealZone healZone = shadow.GetComponent<HealZone>();
                if (healZone == null) healZone = shadow.AddComponent<HealZone>();
                healZone.healPerSecond = healPerSecond;

                // Коллайдер
                Collider2D col = shadow.GetComponent<Collider2D>();
                if (col == null) col = shadow.AddComponent<BoxCollider2D>();
                col.isTrigger = true;

                // Плавное исчезновение
                Destroy(shadow, shadowDuration);

                break; // нашли свободное место → выходим из цикла
            }
        }
    }
}
