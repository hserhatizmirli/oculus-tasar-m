using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBotController : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public Animator animator;

    [Header("Vision Settings")]
    public float smallRadius = 25f;
    public float largeRadius = 50f;
    public float visionAngle = 100f;

    [Header("Reload Settings")]
    public bool isReloading;

    [Header("Animator Parameters")]
    public string playerInSightParam = "PlayerInSight";
    public string isFiringParam = "IsFiring";
    public string isReloadingParam = "IsReloading";
    public string isRunningParam = "IsRunning";
    public string idleParam = "Idle";

    private void Update()
    {
        ResetAllAnimatorParameters();

        if (isReloading)
        {
            animator.SetBool(isReloadingParam, true);
        }
        else if (IsTargetInArea(smallRadius))
        {
            animator.SetBool(playerInSightParam, true);
            animator.SetBool(isFiringParam, true);
        }
        else if (IsTargetInArea(largeRadius))
        {
            animator.SetBool(isRunningParam, true);
        }
        else
        {
            animator.SetBool(idleParam, true);
        }
    }

    private bool IsTargetInArea(float radius)
    {
        if (target == null) return false;

        Vector3 directionToTarget = target.position - transform.position;
        float distanceToTarget = directionToTarget.magnitude;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

        return distanceToTarget <= radius && angleToTarget <= visionAngle / 2f;
    }

    private void ResetAllAnimatorParameters()
    {
        animator.SetBool(playerInSightParam, false);
        animator.SetBool(isFiringParam, false);
        animator.SetBool(isReloadingParam, false);
        animator.SetBool(isRunningParam, false);
        animator.SetBool(idleParam, false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, smallRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, largeRadius);

        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * transform.forward * largeRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2f, 0) * transform.forward * largeRadius;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
