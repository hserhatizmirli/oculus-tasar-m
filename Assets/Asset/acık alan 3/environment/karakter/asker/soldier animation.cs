using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBotController : MonoBehaviour
{
    [Header("References")]
    public Transform player; // XR Origin karakteri
    public Animator animator;

    [Header("Vision Settings")]
    public float smallRadius = 25f;  // K���k k�renin yar��ap�
    public float largeRadius = 50f;  // B�y�k k�renin yar��ap�
    public float visionAngle = 100f; // K�re diliminin a��s�
    public string targetTag = "Player"; // Tetiklenecek objenin etiketi (�rne�in Player)

    [Header("Animation States")]
    public string idleState = "Idle";       // Bo�ta durma animasyonu
    public string firingState = "Firing";  // Ate� etme animasyonu
    public string runningState = "Running"; // Ko�ma animasyonu

    private bool isPlayerDetected = false; // Oyuncunun alg�lan�p alg�lanmad���n� kontrol eder
    private static Vector3 lastFiringPosition; // Ate� eden botun pozisyonunu saklar

    void Update()
    {
        // K���k k�re i�inde hedefi alg�la
        if (IsTargetInVisionArea())
        {
            isPlayerDetected = true;
            TriggerFiring();
        }
        else
        {
            isPlayerDetected = false;
            TriggerIdle();
        }

        // B�y�k k�re i�inde di�er botlar� tetikle
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(firingState))
        {
            lastFiringPosition = transform.position;
            NotifyNearbyBots();
        }
    }

    bool IsTargetInVisionArea()
    {
        // K���k k�re i�inde hedef objeleri kontrol eder
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, smallRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(targetTag)) // Etiket hedefi kontrol eder
            {
                Vector3 directionToTarget = hitCollider.transform.position - transform.position;
                float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

                // E�er hedef belirtilen a�� i�indeyse
                if (angleToTarget <= visionAngle / 2f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void NotifyNearbyBots()
    {
        // B�y�k k�re i�inde di�er botlar� tetikle
        Collider[] nearbyBots = Physics.OverlapSphere(transform.position, largeRadius);
        foreach (var bot in nearbyBots)
        {
            if (bot.CompareTag("Enemy") && bot.gameObject != this.gameObject)
            {
                EnemyBotController botController = bot.GetComponent<EnemyBotController>();
                if (botController != null)
                {
                    botController.TriggerRunning(lastFiringPosition);
                }
            }
        }
    }

    void TriggerFiring()
    {
        animator.Play(firingState); // Ate� etme animasyonu ba�lat
    }

    void TriggerIdle()
    {
        animator.Play(idleState); // Bo�ta durma animasyonu ba�lat
    }

    public void TriggerRunning(Vector3 targetPosition)
    {
        animator.Play(runningState); // Ko�ma animasyonu ba�lat
        StartCoroutine(MoveToPosition(targetPosition));
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float speed = 5f; // Ko�ma h�z�
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        // K���k k�reyi �iz
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, smallRadius);

        // B�y�k k�reyi �iz
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, largeRadius);

        // G�r�� a��s�n� �iz
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * transform.forward * smallRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2f, 0) * transform.forward * smallRadius;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}

