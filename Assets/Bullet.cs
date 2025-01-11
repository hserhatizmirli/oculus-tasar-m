using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float range; // Merminin maksimum menzili
    private Vector3 startPosition; // Merminin ba�lang�� pozisyonu

    public void SetRange(float bulletRange)
    {
        range = bulletRange; // Menzili ayarla
        startPosition = transform.position; // Merminin ba�lang�� pozisyonunu kaydet
    }

    void Update()
    {
        // Mermi menzili a�t�ysa yok et
        if (Vector3.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }
}
