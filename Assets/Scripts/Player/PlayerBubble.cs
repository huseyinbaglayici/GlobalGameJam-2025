using System;
using Cysharp.Threading.Tasks; // UniTask için gerekli kütüphane
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBubble : MonoBehaviour
{
    public GameObject bubblePrefab;

    public Vector3 bubbleOffset;

    [FormerlySerializedAs("bubbleCoolDown")]
    public float defaultBubbleCoolDown = 0.5f;

    private float bubbleTimer = 0f;

    private void Update()
    {
        bubbleTimer -= Time.deltaTime;

        if (bubbleTimer <= 0f)
        {
            CreateBubble();
            bubbleTimer = defaultBubbleCoolDown;
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public async void CreateBubbleLineForDash(Vector3 direction, float dashDistance, int bubbleCount)
    {
        // PlayerController referansının geçerli olup olmadığını kontrol et
        if (PlayerController._instance == null)
        {
            Debug.LogWarning("PlayerController._instance is null, skipping bubble creation.");
            return; // Eğer null ise, balonlar oluşturulmaz
        }

        // Dash'in her adımı için pozisyonlar hesaplanıyor
        Vector3 startPosition = transform.position;
        float stepDistance = dashDistance / bubbleCount;

        // Balonlar oluşturuluyor
        for (int i = 0; i < bubbleCount; i++)
        {
            if (PlayerController._instance == null)
            {
                Debug.LogWarning("PlayerController._instance was destroyed during dash, skipping bubble creation.");
                return; // Eğer PlayerController nesnesi yoksa, işlem durdurulur
            }

            // Her balon için pozisyon hesaplanıyor
            Vector3 spawnPosition = startPosition + direction.normalized * stepDistance * i + bubbleOffset;
            GameObject bubble = Instantiate(bubblePrefab, PlayerController._instance.transform.position,
                Quaternion.identity);
            var bubbleScript = bubble.GetComponent<Bubble>();
            bubbleScript.InitializeBubble();

            await UniTask.Delay(TimeSpan.FromSeconds(0.02f));
        }
    }

    private void CreateBubble()
    {
        // PlayerController referansının geçerli olup olmadığını kontrol et
        if (PlayerController._instance == null)
        {
            Debug.LogWarning("PlayerController._instance is null, skipping bubble creation.");
            return; // Eğer null ise, balon oluşturulmaz
        }

        Vector3 spawnPosition = transform.position + bubbleOffset;
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        var bubbleScript = bubble.GetComponent<Bubble>();
        bubbleScript.InitializeBubble();
    }
}
