using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 30f;

    private void Update()
    {
        // Get the mouse position in world space
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        targetPosition.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}