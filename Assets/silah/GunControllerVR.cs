using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunControllerVR : XRGrabInteractable
{
    [Header("Gun Settings")]
    public GameObject bulletPrefab; // Mermi prefabý
    public Transform firePoint; // Merminin çýkýþ noktasý
    public ParticleSystem muzzleFlash; // Namludan çýkan ateþ efekti
    public AudioSource fireSound; // Ateþ sesi

    [Header("Firing Settings")]
    public float fireRate = 0.2f; // Ateþ etme sýklýðý
    public float bulletSpeed = 20f; // Mermi hýzý
    public float bulletRange = 50f; // Mermi menzili

    private bool isChamberLoaded = false; // Mermi yataðý dolu mu?
    private bool isFiring = false;

    private void Update()
    {
        // VR kontrolcüsünde ateþleme butonuna basýldýðýnda ateþ et
        if (isSelected && isFiring) // Silah seçili olduðunda ve tetikleme yapýldýðýnda
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (!isChamberLoaded)
        {
            Debug.LogWarning("Mermi yataðý boþ! Ateþ edilemiyor.");
            return;
        }

        // Ateþleme efektleri
        if (muzzleFlash != null) muzzleFlash.Play();
        if (fireSound != null) fireSound.Play();

        // Mermi oluþturma ve hareket
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null) rb.velocity = firePoint.forward * bulletSpeed;
        }

        isChamberLoaded = false; // Mermi yataðý boþalýr
    }

    public void ReloadMagazine()
    {
        Debug.Log("Þarjör takýldý.");
        isChamberLoaded = false; // Yeni þarjör takýldýðýnda, mermi yataðý boþ olur
    }

    public void ChamberRound()
    {
        if (!isChamberLoaded)
        {
            isChamberLoaded = true; // Mermi yataða sürüldü
            Debug.Log("Mermi yataða sürüldü.");
        }
        else
        {
            Debug.Log("Mermi zaten yataða sürülmüþ.");
        }
    }

    public void SetFiringState(bool state)
    {
        isFiring = state; // Ateþleme durumu ayarla
    }
}