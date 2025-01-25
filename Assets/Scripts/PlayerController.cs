using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables

    public float moveSpeed = 30f;
    public float smoothTime = 0.2f;

    #endregion

    #region DashSettings

    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashPower = 20f;
    private float dashDuration = 0.2f;
    private float dashTimeLeft;
    private Vector3 dashDirection;

    #endregion

    private void Update()
    {
        if (!isDashing)
        {
            // Fare pozisyonuna doğru hareket
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Sol tık ile dash başlat
            if (Input.GetMouseButtonDown(0) && canDash)
            {
                StartDash(targetPosition);
            }
        }
        else
        {
            // Dash hareketini gerçekleştir
            DashMovement();
        }
    }

    private void StartDash(Vector3 targetPosition)
    {
        isDashing = true;
        canDash = false;
        dashTimeLeft = dashDuration; // Dash süresini sıfırla
        dashDirection = (targetPosition - transform.position).normalized;
    }

    private void DashMovement()
    {
        if (dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;
            transform.position += dashDirection * (dashPower * Time.deltaTime);
        }
        else
        {
            EndDash();
        }
    }

    private void EndDash()
    {
        isDashing = false;
        StartCoroutine(DashCooldown()); // Cooldown başlat
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(1f); // 1 saniyelik cooldown süresi
        canDash = true;
    }
}