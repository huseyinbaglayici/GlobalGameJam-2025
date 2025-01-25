using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    #region Movement Variables

    public float moveSpeed = 30f;

    #endregion

    public static PlayerController _instance;

    #region DashSettings

    public PlayerBubble playerBubble;
    private bool canDash = true;
    public bool isDashing = false;
    [SerializeField] private float dashPower = 20f;
    private float dashDuration = 0.2f;
    private Vector3 dashDirection;

    #endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if (!isDashing)
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0) && canDash)
            {
                StartDash(targetPosition).Forget();
            }
        }
    }

    private async UniTaskVoid StartDash(Vector3 targetPosition)
    {
        isDashing = true;
        canDash = false;
        dashDirection = (targetPosition - transform.position).normalized;
        playerBubble.CreateBubbleLineForDash(dashDirection, 5, 10);
        float dashTimeLeft = dashDuration;
        while (dashTimeLeft > 0)
        {
            float deltaTime = Time.deltaTime;
            transform.position += dashDirection * (dashPower * deltaTime);
            dashTimeLeft -= deltaTime;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        isDashing = false;
        await UniTask.WaitForSeconds(1f);
        canDash = true;
    }
}