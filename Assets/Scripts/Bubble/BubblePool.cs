using System;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BubblePool : MonoBehaviour
{
    public GameObject bubblePrefab;
    public int poolSize = 50;
    private Queue<GameObject> bubblePool = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bubble = Instantiate(bubblePrefab);
            bubble.SetActive(false);
            bubblePool.Enqueue(bubble); // kuyruga ekle
        }
    }

    public GameObject GetBubble()
    {
        if (bubblePool.Count > 0)
        {
            GameObject bubble = bubblePool.Dequeue(); // kuyruktan balon al
            bubble.SetActive(true);
            return bubble;
        }
        else
        {   // havuzda bubble yoksa
            GameObject bubble = Instantiate(bubblePrefab);
            return bubble;
        }
    }
    
    // baloncugu havuza geri gonder
    public void ReturnBubble(GameObject bubble)
    {
        bubble.SetActive(false);
        bubblePool.Enqueue(bubble); // kuyruga ekle
    }
}