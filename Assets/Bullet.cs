using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float range; // Merminin maksimum menzili
    private Vector3 startPosition; // Merminin baþlangýç pozisyonu

    public void SetRange(float bulletRange)
    {
        range = bulletRange; // Menzili ayarla
        startPosition = transform.position; // Merminin baþlangýç pozisyonunu kaydet
    }

    void Update()
    {
        // Mermi menzili aþtýysa yok et
        if (Vector3.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }
}
