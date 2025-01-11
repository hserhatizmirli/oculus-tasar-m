using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunControllerVR : MonoBehaviour
{
    [Header("Gun Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public AudioSource fireSound;

    [Header("Firing Settings")]
    public float fireRate = 0.2f;
    public float bulletSpeed = 20f;
    public float bulletRange = 50f;

    private float nextFireTime = 0f;

    [Header("Magazine Settings")]
    public Transform magazineSlot;

    [Header("VR Input Settings")]
    public XRController rightHandController;
    public XRController leftHandController;

    private XRController activeController;
    private bool isChamberLoaded = false; // Yata�a mermi yerle�tirilip yerle�tirilmedi�i

    void Update()
    {
        UpdateActiveController();

        if (activeController != null)
        {
            // Ate�leme i�lemi
            if (CheckInput(activeController, InputHelpers.Button.Trigger) && Time.time >= nextFireTime && isChamberLoaded)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }

            // Manuel mermi s�rme i�lemi
            if (CheckInput(activeController, InputHelpers.Button.SecondaryButton))
            {
                ChamberRound();
            }

            // �arj�r de�i�tirme i�lemi
            if (CheckInput(activeController, InputHelpers.Button.Grip))
            {
                ReloadMagazine();
            }
        }
    }

    void Fire()
    {
        if (!isChamberLoaded) return;

        if (muzzleFlash != null) muzzleFlash.Play();
        if (fireSound != null) fireSound.Play();

        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (rb != null) rb.velocity = firePoint.forward * bulletSpeed;
            if (bulletScript != null) bulletScript.SetRange(bulletRange);
        }

        isChamberLoaded = false; // Mermi yata�� bo�al�r
    }

    public void ReloadMagazine()
    {
        // �arj�r de�i�tirme i�lemi
        Debug.Log("�arj�r tak�ld�.");
        isChamberLoaded = false; // Yeni �arj�r tak�ld���nda, mermi yata�� bo� olur.
    }

    public void ChamberRound()
    {
        // Manuel olarak mermi yata�a s�rme i�lemi
        isChamberLoaded = true;
        Debug.Log("Mermi yata�a s�r�ld�.");
    }

    private bool CheckInput(XRController controller, InputHelpers.Button button)
    {
        if (controller.inputDevice.IsPressed(button, out bool isPressed) && isPressed)
        {
            return true;
        }
        return false;
    }

    private void UpdateActiveController()
    {
        if (CheckInput(rightHandController, InputHelpers.Button.Grip))
        {
            activeController = rightHandController;
        }
        else if (CheckInput(leftHandController, InputHelpers.Button.Grip))
        {
            activeController = leftHandController;
        }
    }
}
