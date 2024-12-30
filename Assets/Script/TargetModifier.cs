using UnityEngine;

public class TargetModifier : MonoBehaviour
{
    [Header("Target Asset")]
    public Transform target; // Hedef objesi
    public Vector3 newPosition; // Yeni pozisyon deðeri
    public Vector3 newRotation; // Yeni rotasyon deðeri

    [Header("Detection Settings")]
    public GameObject triggerObject; // Çarpýþma kontrolü için belirlenen obje veya prefab

    private void OnTriggerEnter(Collider other)
    {
        // Çarpýþma kontrolü
        if (other.gameObject == triggerObject)
        {
            // Pozisyon ve rotasyonu deðiþtirme
            if (target != null)
            {
                target.position = newPosition;
                target.rotation = Quaternion.Euler(newRotation);
                Debug.Log("Target position and rotation updated!");
            }
        }
    }
}
