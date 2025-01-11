using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform magazineSlot;       // Þarjörün takýlacaðý yer
    public GameObject currentMagazine;  // Takýlý olan þarjör
    public int currentBullets = 0;      // Mevcut mermi sayýsý
    public int magazineCapacity = 30;   // Þarjör kapasitesi
    public int totalAmmo = 90;          // Toplam yedek mermi sayýsý
    private bool isMagazineAttached = false;

    void Update()
    {
        // Þarjör takýlýysa ve ateþ edilebilecek mermi yoksa, þarjör çýkarýlýr.
        if (currentBullets <= 0 && isMagazineAttached)
        {
            DetachMagazine();
        }
    }

    public void DetachMagazine()
    {
        if (currentMagazine != null)
        {
            // Þarjör çýkarýlýr
            currentMagazine.transform.parent = null;
            Rigidbody rb = currentMagazine.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Yerçekimi aktif
            }
            currentMagazine.GetComponent<Magazine>().isEmpty = true; // Þarjör boþ olarak iþaretlenir
            isMagazineAttached = false;
            currentMagazine = null; // Mevcut þarjör sýfýrlanýr
        }
    }


    public void AttachMagazine(GameObject newMagazine)
    {
        Magazine magazineScript = newMagazine.GetComponent<Magazine>();

        if (magazineScript != null && !magazineScript.isEmpty)
        {
            currentMagazine = newMagazine;
            currentMagazine.transform.SetParent(magazineSlot); // Þarjör yerine oturur
            currentMagazine.transform.localPosition = Vector3.zero; // Doðru konuma oturur
            currentMagazine.transform.localRotation = Quaternion.identity; // Doðru rotasyona oturur
            Rigidbody rb = currentMagazine.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Yerçekimi kapatýlýr
            }
            isMagazineAttached = true;

            // Þarjör içindeki mermiyi yükle
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
