using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PumpShotgun : MonoBehaviour
{
    [Header("Shotgun Settings")]
    public int maxBullets = 8;             // Pompalýnýn alabileceði maksimum mermi
    public int currentBullets = 0;         // Þu anki mermi sayýsý
    public GameObject bulletPrefab;        // Yüklenen mermi prefabý
    public Transform firePoint;            // Merminin çýkýþ noktasý
    public float bulletSpeed = 30f;        // Mermi hýzý

    [Header("Interaction Settings")]
    public XRSocketInteractor bulletSocket; // Mermilerin yüklendiði XR Socket
    public ParticleSystem muzzleFlash;     // Ateþleme efekti
    public AudioSource fireSound;          // Ateþleme sesi (isteðe baðlý)

    private bool isReloading = false;      // Þarjör yükleniyor mu?

    void Update()
    {
        // Ateþleme iþlemi
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

            // Yeni mermiyi yükle
            currentBullets++;
            Destroy(bullet); // Mermiyi XR Socket'ten kaldýr

            Debug.Log($"Mermi yüklendi. Þu anki mermi: {currentBullets}/{maxBullets}");

            // Reload iþlemini kýsa bir gecikmeyle tamamla
            Invoke(nameof(EndReload), 0.5f);
        }
    }

    void EndReload()
    {
        isReloading = false;
    }

    void Fire()
    {
        // Mermi çýkar
        currentBullets--;

        // Ateþleme efekti
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Ateþleme sesi
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
