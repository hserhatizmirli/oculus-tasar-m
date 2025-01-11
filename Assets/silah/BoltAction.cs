using UnityEngine;

public class BoltAction : MonoBehaviour
{
    [Header("Positions")]
    public Transform startPosition; // Eski pozisyona dönmek için referans
    public Transform pullPosition; // Çekildiði pozisyon

    [Header("Settings")]
    public float pullThreshold = 0.1f; // Çekme mesafesi

    private bool isPulled = false; // Kurma kolunun durumu
    public GunControllerVR gunController; // GunControllerVR referansý

    void Update()
    {
        // Baþlangýç ve çekilme pozisyonlarýný kontrol et
        if (pullPosition == null || startPosition == null)
        {
            Debug.LogWarning("StartPosition veya PullPosition atanmadý!");
            return;
        }

        // Kurma kolunun çekildiðini kontrol et
        float distance = Vector3.Distance(transform.localPosition, pullPosition.localPosition);

        if (distance <= pullThreshold && !isPulled)
        {
            isPulled = true;
            Debug.Log("Kurma kolu çekildi.");

            // Þarjörü yeniden doldur
            gunController?.ChamberRound(); // Mermiyi yataða sür
        }
    }

    public void ResetPosition()
    {
        // Baþlangýç pozisyonu kontrolü
        if (startPosition == null)
        {
            Debug.LogWarning("StartPosition atanmadý!");
            return;
        }

        // Kolun pozisyonu eski haline getirilsin
        if (isPulled)
        {
            transform.localPosition = startPosition.localPosition; // Eski pozisyona dön
            isPulled = false;
            Debug.Log("Kurma kolu eski pozisyona döndü.");
        }
    }
}
