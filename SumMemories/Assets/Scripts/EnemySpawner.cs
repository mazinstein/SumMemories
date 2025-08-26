using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnX = 10f; // правая граница карты

    private float minY;
    private float maxY;

    private void Start()
    {
        // Получаем границы камеры по Y
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camY = cam.transform.position.y;
        minY = camY - camHeight / 2f;
        maxY = camY + camHeight / 2f;

        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(spawnX, Random.Range(minY, maxY), 0f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}