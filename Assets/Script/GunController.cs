using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform magazineSlot;       // �arj�r�n tak�laca�� yer
    public GameObject currentMagazine;  // Tak�l� olan �arj�r
    public int currentBullets = 0;      // Mevcut mermi say�s�
    public int magazineCapacity = 30;   // �arj�r kapasitesi
    public int totalAmmo = 90;          // Toplam yedek mermi say�s�
    private bool isMagazineAttached = false;

    void Update()
    {
        // �arj�r tak�l�ysa ve ate� edilebilecek mermi yoksa, �arj�r ��kar�l�r.
        if (currentBullets <= 0 && isMagazineAttached)
        {
            DetachMagazine();
        }
    }

    public void DetachMagazine()
    {
        if (currentMagazine != null)
        {
            // �arj�r ��kar�l�r
            currentMagazine.transform.parent = null;
            Rigidbody rb = currentMagazine.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Yer�ekimi aktif
            }
            currentMagazine.GetComponent<Magazine>().isEmpty = true; // �arj�r bo� olarak i�aretlenir
            isMagazineAttached = false;
            currentMagazine = null; // Mevcut �arj�r s�f�rlan�r
        }
    }


    public void AttachMagazine(GameObject newMagazine)
    {
        Magazine magazineScript = newMagazine.GetComponent<Magazine>();

        if (magazineScript != null && !magazineScript.isEmpty)
        {
            currentMagazine = newMagazine;
            currentMagazine.transform.SetParent(magazineSlot); // �arj�r yerine oturur
            currentMagazine.transform.localPosition = Vector3.zero; // Do�ru konuma oturur
            currentMagazine.transform.localRotation = Quaternion.identity; // Do�ru rotasyona oturur
            Rigidbody rb = currentMagazine.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Yer�ekimi kapat�l�r
            }
            isMagazineAttached = true;

            // �arj�r i�indeki mermiyi y�kle
            currentBullets = magazineScript.bulletsInMagazine;
        }
    }


    void Reload()
    {
        int bulletsNeeded = magazineCapacity - currentBullets;
        if (totalAmmo >= bulletsNeeded)
        {
            currentBullets = magazineCapacity;
            totalAmmo -= bulletsNeeded;
        }
        else
        {
            currentBullets += totalAmmo;
            totalAmmo = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Magazine") && !isMagazineAttached)
        {
            Magazine mag = other.GetComponent<Magazine>();
            if (mag != null && !mag.isEmpty)
            {
                AttachMagazine(other.gameObject);
            }
        }
    }

}
