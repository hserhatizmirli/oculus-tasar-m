using System.Collections;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    [Header("Gun Settings")]
    public Transform firePoint;  // Ateşleme noktası
    public Transform gripPoint; // İkinci tutma noktası
    public AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip emptyClickSound;
    public AudioClip reloadSound;

    [Header("Magazine Settings")]
    public Transform magazineSlot; // Şarjör yuvası
    public GameObject magazinePrefab; // Şarjör modeli
    public int maxAmmo = 30; // Şarjördeki mermi sayısı
    private int currentAmmo;

    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isReloading)
        {
            if (currentAmmo > 0)
            {
                Fire();
            }
            else
            {
                PlayEmptyClick();
            }
        }
    }

    void Fire()
    {
        // Mermi ateşleme işlemi
        currentAmmo--;
        Debug.Log("Firing! Ammo left: " + currentAmmo);
        audioSource.PlayOneShot(fireSound);

        // Ateşleme efektleri ve görsellik eklenecek
        // Örneğin mermi izi ve silah animasyonu
    }

    void PlayEmptyClick()
    {
        Debug.Log("Empty magazine!");
        audioSource.PlayOneShot(emptyClickSound);
    }

    public void Reload()
    {
        if (isReloading) return;

        isReloading = true;
        Debug.Log("Reloading...");
        audioSource.PlayOneShot(reloadSound);

        // Şarjör değişim animasyonu için zaman tanıyoruz
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(2); // Şarjör değiştirme süresi
        currentAmmo = maxAmmo;
        Debug.Log("Reloaded!");
        isReloading = false;
    }
}
