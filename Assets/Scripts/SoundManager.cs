using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource audioSource;
    public AudioClip TakingdamageSound;
    public AudioClip EnemyBubbledSound;

    private void Awake()
    {
        // Eğer başka bir SoundManager var ise, bu objeyi sil
        if (instance != null)
        {
            Destroy(gameObject); // Eğer eski bir SoundManager varsa onu yok et
            return;
        }

        // Eğer henüz yoksa, mevcut instance'ı bu objeye atıyoruz
        instance = this;

        // Sahne değiştiğinde bu GameObject'in kaybolmaması için DontDestroyOnLoad kullanıyoruz
        DontDestroyOnLoad(gameObject);

        // AudioSource bileşenini almak
        audioSource = GetComponent<AudioSource>();

        // Eğer audioSource null ise, hata mesajı yazdır
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on SoundManager.");
        }
    }

    // Hasar sesi çalma
    public void PlayDamageSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(TakingdamageSound); // Hasar sesini çalar
        }
        else
        {
            Debug.LogError("AudioSource is null in SoundManager.");
        }
    }

    // Düşman öldürme sesi çalma
    public void PlayEnemyDeathSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(EnemyBubbledSound); // Düşman öldürme sesini çalar
        }
        else
        {
            Debug.LogError("AudioSource is null in SoundManager.");
        }
    }
}