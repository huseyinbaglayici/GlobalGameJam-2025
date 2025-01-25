using System;
using UnityEngine;

public class PlayerBubble : MonoBehaviour
{
    public BubblePool bubblePool; // ref Bubble
    public float bubbleCoolDown = 0.5f; // Baloncuk birakma araligi
    private float bubbleTimer = 0f; //Zamanlayici

    private void Update()
    {   // bubble birakma araligini kontrol et ?

        bubbleTimer -= Time.deltaTime;
        if (bubbleTimer <= 0f)
        {
            CreateBubble();
            bubbleTimer = bubbleCoolDown;
        }

    }

    private void CreateBubble()
    {
        GameObject bubble = bubblePool.GetBubble();
        bubble.transform.position = transform.position; // baloncugu oyuncunun konumuna GETIR
        bubble.GetComponent<Bubble>().InitializeBubble(); // baloncugu baslat
        
    }
}
