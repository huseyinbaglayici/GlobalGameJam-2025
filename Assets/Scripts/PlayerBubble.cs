using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBubble : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Vector3 bubbleOffset;
    [FormerlySerializedAs("bubbleCoolDown")] public float defaultBubbleCoolDown = 0.5f;
    private float bubbleMultiplier = 1.3f;
    private float bubbleTimer = 0f;

    private void Update()
    {
        bubbleTimer -= Time.deltaTime;
        if (bubbleTimer <= 0f)
        {
            CreateBubble();
            bubbleTimer = defaultBubbleCoolDown;
        }
    }

    private void CreateBubble()
    {   
        Vector3 spawnPosition = transform.position + bubbleOffset; 
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, quaternion.identity);
        var bubbleScript = bubble.GetComponent<Bubble>();
        bubbleScript.InitializeBubble();
    }
}