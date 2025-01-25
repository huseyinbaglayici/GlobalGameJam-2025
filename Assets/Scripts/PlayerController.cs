using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 30f;
    public float smoothTime = 0.2f;
    private bool canDash = true;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        // Get the mouse position in world space
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        targetPosition.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // transform.position =
        //     Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, moveSpeed);
    }

    private void FixedUpdate()
    {
    }
}