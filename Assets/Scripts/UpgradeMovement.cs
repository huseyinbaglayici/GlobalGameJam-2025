using UnityEngine;

public class UpgradeMovement : MonoBehaviour
{
    public float floatSpeed = 1f; 
    public float floatAmount = 0.5f;
    public float swaySpeed = 1.5f; 
    public float swayAmount = 5f; 
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        // Yukarı-aşağı hareket (sinüs dalgası)
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;

        // Sağa-sola sallanma (sinüs dalgası ile açıyı hesaplama)
        float swayAngle = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        // Yeni pozisyon ve rotasyonu uygula
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        transform.rotation = startRotation * Quaternion.Euler(0, 0, swayAngle);
    }
}