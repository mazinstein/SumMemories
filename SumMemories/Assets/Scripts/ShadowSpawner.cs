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

    void Start()
    {
        InvokeRepeating(nameof(SpawnShadow), 1f, spawnInterval);
    }

    void SpawnShadow()
    {
        if (shadowPrefabs.Length == 0 || spawnTopLeft == null || spawnBottomRight == null) return;

        GameObject prefab = shadowPrefabs[Random.Range(0, shadowPrefabs.Length)];
        float x = Random.Range(spawnTopLeft.position.x, spawnBottomRight.position.x);
        float y = Random.Range(spawnBottomRight.position.y, spawnTopLeft.position.y);
        Vector2 spawnPos = new Vector2(x, y);

        GameObject shadow = Instantiate(prefab, spawnPos, Quaternion.identity);

        HealZone healZone = shadow.GetComponent<HealZone>();
        if (healZone == null) healZone = shadow.AddComponent<HealZone>();
        healZone.healPerSecond = healPerSecond;

        Collider2D col = shadow.GetComponent<Collider2D>();
        if (col == null) col = shadow.AddComponent<BoxCollider2D>();
        col.isTrigger = true;

        Destroy(shadow, shadowDuration);
    }
}


