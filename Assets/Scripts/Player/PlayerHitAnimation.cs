using UnityEngine;

public class PlayerHitAnimation : MonoBehaviour
{
    [SerializeField] private PlayerEnemyCollision playerEnemyCollision;
    [SerializeField] private Animator animator;
    
    private static readonly int İsTakingDamage = Animator.StringToHash("isTakingDamage");

    private void OnEnable()
    {
        playerEnemyCollision.OnHit += UpdateAnimation;
    }

    private void OnDisable()
    {
        playerEnemyCollision.OnHit -= UpdateAnimation;
    }

    private void UpdateAnimation()
    {
        animator.SetTrigger(İsTakingDamage);
    }
}
