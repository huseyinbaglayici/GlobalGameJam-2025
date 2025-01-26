using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollision : MonoBehaviour
{
    public event Action OnHit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnHit?.Invoke();
            PlayerController._instance.TakeDamage(10);
        }
    }
}
