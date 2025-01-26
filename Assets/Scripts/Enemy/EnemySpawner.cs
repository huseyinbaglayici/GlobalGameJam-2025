using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // İlk düşman türü prefab'ı
    public GameObject enemyPrefab2;      // İkinci düşman türü prefab'ı
    public float spawnInterval = 2f;     // Spawn süresi
    public float spawnRadius = 50f;      // Oyuncuya göre spawn mesafesi

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (player != null)
        {
            // KillCounterManager.Instance.killcount'a göre prefab seç
            GameObject prefabToSpawn = KillCountManager.Instance.killCount >= 10 ? enemyPrefab2 : enemyPrefab;

            // Spawn pozisyonunu belirle
            Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;

            // Prefab'ı oluştur
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}