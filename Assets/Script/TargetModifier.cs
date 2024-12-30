using UnityEngine;

public class TargetModifier : MonoBehaviour
{
    [Header("Target Asset")]
    public Transform target; // Hedef objesi
    public Vector3 newPosition; // Yeni pozisyon de�eri
    public Vector3 newRotation; // Yeni rotasyon de�eri

    [Header("Detection Settings")]
    public GameObject triggerObject; // �arp��ma kontrol� i�in belirlenen obje veya prefab

    private void OnTriggerEnter(Collider other)
    {
        // �arp��ma kontrol�
        if (other.gameObject == triggerObject)
        {
            // Pozisyon ve rotasyonu de�i�tirme
            if (target != null)
            {
                target.position = newPosition;
                target.rotation = Quaternion.Euler(newRotation);
                Debug.Log("Target position and rotation updated!");
            }
        }
    }
}
