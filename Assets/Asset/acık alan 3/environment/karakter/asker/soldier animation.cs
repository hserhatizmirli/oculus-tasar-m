using UnityEngine;
using System.Collections;

public class EnemyBotController : MonoBehaviour
{
    [Header("References")]
    public Transform target;
    public Animator animator;

    [Header("Settings")]
    public float visionAngle = 100f;
    public float visionRadius = 25f;
    public int maxAmmo = 30;
    public float reloadTime = 2f;

    private int currentAmmo;
    private bool isReloading;

    private void Start()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    private void Update()
    {
        if (isReloading) return;

        bool inSight = IsTargetInSight();

        if (inSight)
        {
            if (currentAmmo > 0)
            {
                animator.SetBool("Firing", true);
                animator.SetBool("Run", false);
                Fire();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
        else
        {
            animator.SetBool("Firing", false);
            animator.SetBool("Run", true);
        }

        animator.SetBool("Idle", !inSight && !animator.GetBool("Run"));
    }

    private bool IsTargetInSight()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, visionRadius);
        foreach (var collider in colliders)
        {
            if (collider.transform == target)
            {
                Vector3 directionToTarget = target.position - transform.position;
                float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

                if (angleToTarget <= visionAngle / 2f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void Fire()
    {
        currentAmmo--;
        if (currentAmmo <= 0)
        {
            animator.SetBool("Firing", false);
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reload", true);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        animator.SetBool("Reload", false);
        isReloading = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);

        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * transform.forward * visionRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2f, 0) * transform.forward * visionRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
