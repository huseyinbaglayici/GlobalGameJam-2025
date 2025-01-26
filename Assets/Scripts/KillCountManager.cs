using UnityEngine;
using TMPro; // TextMesh Pro için gerekli

public class KillCountManager : MonoBehaviour
{
    public static KillCountManager Instance; // Singleton erişimi için

    [SerializeField] private TMP_Text killCountText; // TMP_Text kullanıyoruz
    internal int killCount = 0;

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
    }

    private void UpdateKillCountUI()
    {
        // Ekrandaki yazıyı güncelle
        if (killCountText != null)
        {
            killCountText.text = $"Kills: {killCount}";
        }
    }
}