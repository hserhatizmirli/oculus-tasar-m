using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControllerVR : MonoBehaviour
{
    [Header("Gun Settings")]
    public GameObject bulletPrefab; // Mermi prefab�
    public Transform firePoint; // Merminin ��k�� noktas�
    public ParticleSystem muzzleFlash; // Namludan ��kan ate� efekti
    public AudioSource fireSound; // Ate� sesi

    [Header("Firing Settings")]
    public float fireRate = 0.2f; // Ate� etme s�kl���
    public float bulletSpeed = 20f; // Mermi h�z�
    public float bulletRange = 50f; // Mermi menzili

    private bool isChamberLoaded = false; // Mermi yata�� dolu mu?
    private bool isFiring = false;

    private void Update()
    {
        // Ate�leme durumu aktifse ate� et
        if (isFiring)
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (!isChamberLoaded)
        {
            Debug.LogWarning("Mermi yata�� bo�! Ate� edilemiyor.");
            return;
        }

        // Ate�leme efektleri
        if (muzzleFlash != null) muzzleFlash.Play();
        if (fireSound != null) fireSound.Play();

        // Mermi olu�turma ve hareket
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null) rb.velocity = firePoint.forward * bulletSpeed;
        }

        isChamberLoaded = false; // Mermi yata�� bo�al�r
    }

    public void ReloadMagazine()
    {
        Debug.Log("�arj�r tak�ld�.");
        isChamberLoaded = false; // Yeni �arj�r tak�ld���nda, mermi yata�� bo� olur
    }

    public void ChamberRound()
    {
        if (!isChamberLoaded)
        {
            isChamberLoaded = true; // Mermi yata�a s�r�ld�
            Debug.Log("Mermi yata�a s�r�ld�.");
        }
        else
        {
            Debug.Log("Mermi zaten yata�a s�r�lm��.");
        }
    }

    public void SetFiringState(bool state)
    {
        isFiring = state; // Ate�leme durumu ayarla
    }
}
