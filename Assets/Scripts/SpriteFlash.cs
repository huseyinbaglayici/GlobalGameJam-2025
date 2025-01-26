using UnityEngine;

public class SpriteFlash : MonoBehaviour
{
    public static SpriteFlash Instance;

    private SpriteRenderer spriteRenderer;
    private bool isFlashing = false;
    [SerializeField] private float flashDuration = 0.1f; // Flash süresi
    private float flashTimer = 0f;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isFlashing)
        {
            flashTimer += Time.deltaTime;
            float alpha = Mathf.PingPong(flashTimer / flashDuration, 1); // Alpha değerini ping-pong efekti ile değiştirir
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);

            if (flashTimer >= flashDuration * 2) // Flash tamamlandıysa durdur
            {
                isFlashing = false;
                flashTimer = 0f; // Timer'ı sıfırlıyoruz
                // Flash bittikten sonra eski alpha değerini (tam opak) geri alıyoruz
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
            }
        }
    }

    // Flash başlatan fonksiyon
    public void StartFlash(float newDuration)
    {
        flashDuration = newDuration; // Yeni flash süresi atanır
        flashTimer = 0f; // Timer sıfırlanır
        isFlashing = true; // Flash başlatılır
    }
}