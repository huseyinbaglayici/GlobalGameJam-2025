using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables

    public float moveSpeed = 30f;
    public bool playerCanMove = true; // Oyuncunun hareket edebilirliği

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

    public GameObject toiletPrefab; 
    public float flashDuration = 0.1f;

    private bool canReturnToPreviousPosition = false;

    #endregion

    #region Health

    public float maxHealth = 999f;
    public float currentHealth = 100f;
    public float startingHealth = 100f;

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
        if (playerCanMove && !isDashing)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0) && canDash)
            {
                StartDash(targetPosition).Forget();
            }
        }

        if (KillCountManager.Instance.isPulling)
        {
            LevelCompletePlayerMove();
        }

        // Sağ tık kontrolü
        if (Input.GetMouseButtonDown(1) && canReturnToPreviousPosition)
        {
            canReturnToPreviousPosition = false;
            BoomerangBackToPosition().Forget();
        }
    }

    private async UniTaskVoid StartDash(Vector3 targetPosition)
    {
        if (!playerCanMove || this == null) return;

        SpriteFlash.Instance.StartFlash(flashDuration);
        TakeDamage(10);
        posBeforeDash = transform.position;
        isDashing = true;
        canDash = false;
        dashDirection = (targetPosition - transform.position).normalized;

        playerBubble.CreateBubbleLineForDash(dashDirection, 5, 12);

        float dashTimeLeft = dashDuration;
        while (dashTimeLeft > 0)
        {
            if (this == null) return;
            float deltaTime = Time.deltaTime;
            transform.position += dashDirection * (dashPower * deltaTime);
            dashTimeLeft -= deltaTime;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        isDashing = false;
        EnableReturnToPreviousPosition();

        await UniTask.WaitForSeconds(1f);
        canDash = true;
    }

    private async void EnableReturnToPreviousPosition()
    {
        if (this == null) return;

        canReturnToPreviousPosition = true;
        await UniTask.Delay(TimeSpan.FromSeconds(2));
        canReturnToPreviousPosition = false;
    }

    private async UniTaskVoid BoomerangBackToPosition()
    {
        if (!playerCanMove || this == null) return;

        SpriteFlash.Instance.StartFlash(flashDuration);
        isDashing = true;
        float returnSpeed = dashPower;
        playerBubble.CreateBubbleLineForDash(dashDirection, 5, 12);
        TakeDamage(5);

        while (Vector3.Distance(transform.position, posBeforeDash) > 0.1f)
        {
            if (this == null) return;
            transform.position = Vector3.MoveTowards(transform.position, posBeforeDash, returnSpeed * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        transform.position = posBeforeDash;
        isDashing = false;
        canDash = true;
    }

    #region Health System

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / startingHealth;
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthBar();
    }

    public void LevelCompletePlayerMove()
    {
        if (this == null) return;

        EnemySpawner.instance.canSpawn = false;
        playerCanMove = false; // Oyuncunun hareketini durdur
        Vector3 targetPosition = toiletPrefab.transform.position; // Hedef pozisyon olarak tuvaleti belirle
        MoveToPosition(targetPosition).Forget();
        StartCoroutine(nameof(EndGame));
    }

    IEnumerator EndGame()
    {
        if (this == null) yield break;

        Debug.Log("EndGame coroutine başlatıldı.");
        yield return new WaitForSeconds(4f);
        Debug.Log("4 saniye geçti.");
        SceneManager.LoadScene(0);
    }

    private async UniTaskVoid MoveToPosition(Vector3 targetPosition)
    {
        if (this == null) return;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            if (this == null) return;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        transform.position = targetPosition; // Nihai pozisyona tam olarak yerleştir
    }

    private void Die()
    {
        MenuController.Instance.GameOver();
    }

    #endregion
}
