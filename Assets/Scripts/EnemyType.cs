using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyType : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public float health;
    public float speed;
    public int damage;

}
