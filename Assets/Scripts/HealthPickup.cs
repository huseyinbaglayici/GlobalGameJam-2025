using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthIncreaseAmount = 20f; // Alındığında artırılacak can miktarı

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Oyuncu ile çarpışmayı kontrol ediyoruz
        if (collision.CompareTag("Player"))
        {
            // Oyuncunun canını artır
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Heal(healthIncreaseAmount);
            }

            // Pickup objesini yok et
            Destroy(gameObject);
        }
    }
}