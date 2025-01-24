using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        // mouse kordinatlari
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        worldPosition.z = 0f;

        transform.position = worldPosition;
    }
}
