using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class PlayerBubble : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Vector3 bubbleOffset;
    public float bubbleCoolDown = 0.5f;
    private float bubbleTimer = 0f;

    private void Update()
    {
        bubbleTimer -= Time.deltaTime;
        if (bubbleTimer <= 0f)
        {
            CreateBubble();
            bubbleTimer = bubbleCoolDown;
        }
    }

    private void CreateBubble()
    {
        Vector3 spawnPosition = transform.position + bubbleOffset;
        Debug.Log("Spawn Position: " + spawnPosition);

        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, quaternion.identity);
        var bubbleScript = bubble.GetComponent<Bubble>();
        bubbleScript.InitializeBubble();
    }
}