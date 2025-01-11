using UnityEngine;

public class BoltAction : MonoBehaviour
{
    [Header("Positions")]
    public Transform startPosition; // Eski pozisyona d�nmek i�in referans
    public Transform pullPosition; // �ekildi�i pozisyon

    [Header("Settings")]
    public float pullThreshold = 0.1f; // �ekme mesafesi

    private bool isPulled = false; // Kurma kolunun durumu
    public GunControllerVR gunController; // GunControllerVR referans�

    void Update()
    {
        // Ba�lang�� ve �ekilme pozisyonlar�n� kontrol et
        if (pullPosition == null || startPosition == null)
        {
            Debug.LogWarning("StartPosition veya PullPosition atanmad�!");
            return;
        }

        // Kurma kolunun �ekildi�ini kontrol et
        float distance = Vector3.Distance(transform.localPosition, pullPosition.localPosition);

        if (distance <= pullThreshold && !isPulled)
        {
            isPulled = true;
            Debug.Log("Kurma kolu �ekildi.");

            // �arj�r� yeniden doldur
            gunController?.ChamberRound(); // Mermiyi yata�a s�r
        }
    }

    public void ResetPosition()
    {
        // Ba�lang�� pozisyonu kontrol�
        if (startPosition == null)
        {
            Debug.LogWarning("StartPosition atanmad�!");
            return;
        }

        // Kolun pozisyonu eski haline getirilsin
        if (isPulled)
        {
            transform.localPosition = startPosition.localPosition; // Eski pozisyona d�n
            isPulled = false;
            Debug.Log("Kurma kolu eski pozisyona d�nd�.");
        }
    }
}
