using UnityEngine;
using TMPro; // TextMesh Pro için gerekli

public class KillCountManager : MonoBehaviour
{
    public static KillCountManager Instance; // Singleton erişimi için


    public bool isPulling = false;
    
    [SerializeField] private TMP_Text killCountText; // TMP_Text kullanıyoruz
    internal int killCount = 0;

    [Header("Spawn Settings")]
    [SerializeField] private int spawnThreshold = 40; // Nesne oluşturulması için gereken öldürme sayısı
    [SerializeField] private GameObject spawnObject; // Oluşturulacak nesne
    [SerializeField] private Transform playerTransform; // Oyuncunun konumu
    [SerializeField] private float spawnRadius = 5f; // Nesnenin spawnlanacağı çevrenin yarıçapı

    private void Awake()
    {
        // Singleton kontrolü
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateKillCountUI();
    }

    public void IncrementKillCount()
    {
        killCount++; // Sayacı artır
        UpdateKillCountUI();

        // Kill count eşik değerine ulaştıysa nesne oluştur
        if (killCount >= spawnThreshold)
        {
            isPulling = true;
        }
    }

    private void UpdateKillCountUI()
    {
        // Ekrandaki yazıyı güncelle
        if (killCountText != null)
        {
            killCountText.text = $"Defeated Bacteria(s) \n {killCount}";
        }
    }

    private void SpawnObject()
    {
        if (spawnObject != null && playerTransform != null)
        {
            Vector3 spawnPosition = GetRandomPositionAroundPlayer();
            Instantiate(spawnObject, spawnPosition, Quaternion.identity);
            Debug.Log("Nesne oluşturuldu! Sayaç sıfırlanıyor.");
            ResetKillCount();
        }
        else
        {
            Debug.LogWarning("SpawnObject veya PlayerTransform atanmadı!");
        }
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        // Oyuncunun çevresinde rastgele bir konum hesaplar
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0, randomCircle.y);
        return playerTransform.position + randomPosition;
    }

    private void ResetKillCount()
    {
        killCount = 0; // Sayaç sıfırlanır
        UpdateKillCountUI();
    }
}
