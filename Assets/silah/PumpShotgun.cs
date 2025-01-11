using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PumpShotgun : MonoBehaviour
{
    [Header("Shotgun Settings")]
    public int maxBullets = 8;             // Pompal�n�n alabilece�i maksimum mermi
    public int currentBullets = 0;         // �u anki mermi say�s�
    public GameObject bulletPrefab;        // Y�klenen mermi prefab�
    public Transform firePoint;            // Merminin ��k�� noktas�
    public float bulletSpeed = 30f;        // Mermi h�z�

    [Header("Interaction Settings")]
    public XRSocketInteractor bulletSocket; // Mermilerin y�klendi�i XR Socket
    public ParticleSystem muzzleFlash;     // Ate�leme efekti
    public AudioSource fireSound;          // Ate�leme sesi (iste�e ba�l�)

    private bool isReloading = false;      // �arj�r y�kleniyor mu?

    void Update()
    {
        // Ate�leme i�lemi
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentBullets > 0 && !isReloading)
        {
            Fire();
        }
    }

    public void Reload(GameObject bullet)
    {
        if (currentBullets < maxBullets && !isReloading)
        {
            isReloading = true;

            // Yeni mermiyi y�kle
            currentBullets++;
            Destroy(bullet); // Mermiyi XR Socket'ten kald�r

            Debug.Log($"Mermi y�klendi. �u anki mermi: {currentBullets}/{maxBullets}");

            // Reload i�lemini k�sa bir gecikmeyle tamamla
            Invoke(nameof(EndReload), 0.5f);
        }
    }

    void EndReload()
    {
        isReloading = false;
    }

    void Fire()
    {
        // Mermi ��kar
        currentBullets--;

        // Ate�leme efekti
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Ate�leme sesi
        if (fireSound != null)
        {
            fireSound.Play();
        }

        // Mermiyi spawn et
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * bulletSpeed;
            }
        }
    }
}
