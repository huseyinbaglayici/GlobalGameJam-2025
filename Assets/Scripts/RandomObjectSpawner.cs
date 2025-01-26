using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject objectPrefab; // Oluşturulacak obje prefab'ı
    [SerializeField] private Vector2 spawnAreaOffset = new Vector2(5f, 5f); // Oyuncuya göre spawn alanı genişliği ve yüksekliği
    [SerializeField] private float spawnInterval = 2f; // Her obje oluşturma süresi
    [SerializeField] private float spawnProbability = 0.5f; // 0 ile 1 arasında rastgele obje oluşturma olasılığı

    [Header("Spawn Timing")]
    [SerializeField] private float startDelay = 1f; // Başlangıç gecikmesi
    [SerializeField] private float stopAfterTime = 0f; // Belirtilirse, belirli bir süre sonra durur (0 = sınırsız)

    private Transform player;
    private float elapsedTime = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Ensure your player has the 'Player' tag.");
            enabled = false;
            return;
        }

        InvokeRepeating(nameof(TrySpawnObject), startDelay, spawnInterval);
    }

    private void Update()
    {
        if (stopAfterTime > 0)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= stopAfterTime)
            {
                CancelInvoke(nameof(TrySpawnObject));
            }
        }
    }

    private void TrySpawnObject()
    {
        if (objectPrefab == null || player == null)
        {
            Debug.LogWarning("Object Prefab or Player is not assigned!");
            return;
        }

        // Olasılıkla spawn kontrolü
        if (Random.value <= spawnProbability)
        {
            Vector3 spawnPosition = GetRandomSpawnPositionNearPlayer();
            Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPositionNearPlayer()
    {
        // Oyuncunun etrafında belirli bir alanda rastgele pozisyon oluştur
        float x = Random.Range(-spawnAreaOffset.x, spawnAreaOffset.x);
        float y = Random.Range(-spawnAreaOffset.y, spawnAreaOffset.y);

        return player.position + new Vector3(x, y, 0);
    }
}
