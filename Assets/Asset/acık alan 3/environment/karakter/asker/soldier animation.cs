using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBotController : MonoBehaviour
{
    [Header("References")]
    public Transform player; // XR Origin karakteri
    public Animator animator;

    [Header("Vision Settings")]
    public float smallRadius = 25f;  // Küçük kürenin yarýçapý
    public float largeRadius = 50f;  // Büyük kürenin yarýçapý
    public float visionAngle = 100f; // Küre diliminin açýsý
    public string targetTag = "Player"; // Tetiklenecek objenin etiketi (örneðin Player)

    [Header("Animation States")]
    public string idleState = "Idle";       // Boþta durma animasyonu
    public string firingState = "Firing";  // Ateþ etme animasyonu
    public string runningState = "Running"; // Koþma animasyonu

    private bool isPlayerDetected = false; // Oyuncunun algýlanýp algýlanmadýðýný kontrol eder
    private static Vector3 lastFiringPosition; // Ateþ eden botun pozisyonunu saklar

    void Update()
    {
        // Küçük küre içinde hedefi algýla
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

        // Büyük küre içinde diðer botlarý tetikle
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(firingState))
        {
            lastFiringPosition = transform.position;
            NotifyNearbyBots();
        }
    }

    bool IsTargetInVisionArea()
    {
        // Küçük küre içinde hedef objeleri kontrol eder
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, smallRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(targetTag)) // Etiket hedefi kontrol eder
            {
                Vector3 directionToTarget = hitCollider.transform.position - transform.position;
                float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

                // Eðer hedef belirtilen açý içindeyse
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
        // Büyük küre içinde diðer botlarý tetikle
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
        animator.Play(firingState); // Ateþ etme animasyonu baþlat
    }

    void TriggerIdle()
    {
        animator.Play(idleState); // Boþta durma animasyonu baþlat
    }

    public void TriggerRunning(Vector3 targetPosition)
    {
        animator.Play(runningState); // Koþma animasyonu baþlat
        StartCoroutine(MoveToPosition(targetPosition));
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float speed = 5f; // Koþma hýzý
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        // Küçük küreyi çiz
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, smallRadius);

        // Büyük küreyi çiz
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, largeRadius);

        // Görüþ açýsýný çiz
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * transform.forward * smallRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2f, 0) * transform.forward * smallRadius;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}

