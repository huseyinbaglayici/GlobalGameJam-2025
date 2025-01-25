using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int maxHealth = 5;

    private int currentHealth;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        currentHealth = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * (moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyTakesDamage(1);
        }
    }

    public void EnemyTakesDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Düşmanı yok et
        Destroy(gameObject);
    }
}