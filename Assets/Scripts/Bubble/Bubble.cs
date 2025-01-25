using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float damage = 10f; // baloncugun vereegi hasar
    public float lifeTime = 3f; // baloncuk omru

    private float lifetimeTimer;

    private void OnEnable()
    {
        lifetimeTimer = lifeTime; // yeniden aktif oldugunda omrunu sifirla
        
    }

    private void Update()
    {
        lifetimeTimer -= Time.deltaTime;
        if (lifetimeTimer <= 0f)
        {
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        FindObjectOfType<BubblePool>().ReturnBubble(gameObject);
    }

    public void InitializeBubble()
    {
        Vector3 offset = new Vector3(0, 0, -1);
        transform.position = transform.parent.position + offset;
    }
}
