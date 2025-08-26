using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 2f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float camY = cam.transform.position.y;
        float camX = cam.transform.position.x;

        float minY = camY - camHeight / 2f;
        float maxY = camY + camHeight / 2f;
        float spawnX = camX + camWidth / 2f + 1f; // чуть за правой границей

        Vector3 spawnPos = new Vector3(spawnX, Random.Range(minY, maxY), 0f);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}