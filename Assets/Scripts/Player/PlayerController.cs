using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables

    public float moveSpeed = 30f;

    #endregion

    public static PlayerController _instance;

    #region DashSettings

    public PlayerBubble playerBubble;
    private bool canDash = true;
    public bool isDashing = false;
    [SerializeField] private float dashPower = 20f;
    private float dashDuration = 0.2f;
    private Vector3 dashDirection;
    private Vector3 posBeforeDash;

    private bool canReturnToPreviousPosition = false; // Sağ tıkla dönüş yapılabilir mi?

    #endregion

    #region Health

    public float maxHealth = 999f; // Maksimum can
    public float currentHealth = 100;
    public float startingHealth = 100f; // Mevcut can

    public Image healthBar;

    private void Start()
    {
        currentHealth = startingHealth;
        
    }

    #endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (!isDashing)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0) && canDash)
            {
                StartDash(targetPosition).Forget();
            }
        }

        // Sağ tık kontrolü
        if (Input.GetMouseButtonDown(1) && canReturnToPreviousPosition)
        {
            canReturnToPreviousPosition = false; // Geri dönüş sadece bir kez yapılabilir
            BoomerangBackToPosition().Forget();
        }
    }

    private async UniTaskVoid StartDash(Vector3 targetPosition)
    {
        posBeforeDash = transform.position; // Dash öncesi pozisyonu kaydet
        isDashing = true;
        canDash = false;
        dashDirection = (targetPosition - transform.position).normalized;

        playerBubble.CreateBubbleLineForDash(dashDirection, 5, 12);

        float dashTimeLeft = dashDuration;
        while (dashTimeLeft > 0)
        {
            float deltaTime = Time.deltaTime;
            transform.position += dashDirection * (dashPower * deltaTime);
            dashTimeLeft -= deltaTime;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        isDashing = false;
        EnableReturnToPreviousPosition(); // Sağ tıkla geri dönüş iznini başlat

        await UniTask.WaitForSeconds(1f);
        canDash = true;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private async void EnableReturnToPreviousPosition()
    {
        canReturnToPreviousPosition = true; // Geri dönüş yapılabilir
        await UniTask.Delay(TimeSpan.FromSeconds(2)); // 2 saniye bekle
        canReturnToPreviousPosition = false; // Süre dolduktan sonra geri dönüş yapılamaz
    }

    private async UniTaskVoid BoomerangBackToPosition()
    {
        isDashing = true; // Boomerang sırasında başka işlem yapılmasını engelle
        float returnSpeed = dashPower; // Geri dönüş hızı
        playerBubble.CreateBubbleLineForDash(dashDirection, 5, 12);

        while (Vector3.Distance(transform.position, posBeforeDash) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, posBeforeDash, returnSpeed * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        transform.position = posBeforeDash; // Son pozisyonu düzelt
        isDashing = false;
        canDash = true; // Boomerang tamamlandıktan sonra tekrar dash yapılabilir
    }

    #region Health System

    public void TakeDamage(float damage)
    {
        startingHealth -= damage;

        if (currentHealth < 0)
            startingHealth = 0; 

        UpdateHealthBar(); 

        if (startingHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Sağlık barını startingHealth'e göre güncelle
            healthBar.fillAmount = startingHealth / currentHealth;
        }
    }


    public void Heal(float amount)
    {
        startingHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth; // Sağlık maksimum değeri aşamaz

        UpdateHealthBar(); // Sağlık barını güncelle
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Ölümle ilgili diğer işlemler burada yapılabilir
    }

    #endregion
}