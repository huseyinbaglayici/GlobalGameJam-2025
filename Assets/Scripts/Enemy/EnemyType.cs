using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyType : ScriptableObject
{
    public string enemyName;
    public GameObject enemyPrefab; // Düşman prefab'ı
    public int health;
    public float speed;
    public int damage;
}