using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private EnemyType enemyType; // Düşman türünü temsil eden ScriptableObject

    private int currentHealth;
    private Transform player;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (enemyType == null)
        {
            Debug.LogError("Enemy Type is not assigned!");
            return;
        }

        currentHealth = enemyType.health; // Health'i EnemyType'tan alıyoruz
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Düşmanın sprite'ını prefab'tan alalım
        if (enemyType.enemyPrefab != null)
        {
            spriteRenderer.sprite = enemyType.enemyPrefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * (enemyType.speed * Time.deltaTime); // Speed'i EnemyType'tan alıyoruz
        }
    }

}