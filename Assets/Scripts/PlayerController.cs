using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed = 50;

    private void Update()
    {
        // mouse kordinatlari
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        targetPosition.z = 0f;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}