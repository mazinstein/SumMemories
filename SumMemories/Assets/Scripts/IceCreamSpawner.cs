using UnityEngine;

public class IceCreamSpawner : MonoBehaviour
{
    [Header("Ice Cream Settings")]
    public GameObject iceCreamPrefab;
    public float minSpawnTime = 15f;
    public float maxSpawnTime = 20f;
    public float spawnXMin = -5f;
    public float spawnXMax = 5f;
    public float spawnYOffset = 1f; // смещение над камерой
    public LayerMask forbiddenLayers; // <- назначьте в инспекторе слой для деревьев/теней
    public float checkRadius = 0.5f;  // радиус проверки

    private float nextSpawnTime;

    private void Start()
    {
        ScheduleNextSpawn();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnIceCream();
            ScheduleNextSpawn();
        }
    }

    void SpawnIceCream()
    {
        if (iceCreamPrefab == null) return;

        int maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(spawnXMin, spawnXMax);
            float spawnY = Camera.main.transform.position.y + Camera.main.orthographicSize + spawnYOffset;
            Vector3 spawnPos = new Vector3(randomX, spawnY, 0);

            // Проверяем, нет ли в этом месте дерева/тени
            if (!Physics2D.OverlapCircle(spawnPos, checkRadius, forbiddenLayers))
            {
                Instantiate(iceCreamPrefab, spawnPos, Quaternion.identity);
                return;
            }
        }
        // Если не удалось найти свободное место, не спавним мороженое
    }

    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }
}