using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [Header("Recoil Settings")]
    public float recoilAmount = 5f; // Silahýn yukarý döneceði açý (derece)
    public float recoilSpeed = 10f; // Rotasyon hýzý (dönüþ hýzý)
    public float returnSpeed = 5f; // Geri dönüþ hýzý
    public float returnOffset = 1f; // Geri dönüþte Z eksenindeki sapma miktarý

    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // Mermi prefab'ý
    public Transform firePoint; // Merminin çýkýþ noktasý
    public float bulletSpread = 2f; // Mermi sapma miktarý (derece)

    private Quaternion originalRotation; // Silahýn baþlangýç rotasyonu
    private Quaternion targetRotation; // Hedef rotasyon
    private Quaternion returnRotation; // Geri dönüþ rotasyonu
    private bool isRecoiling = false;

    void Start()
    {
        // Silahýn baþlangýç rotasyonunu kaydet
        originalRotation = transform.localRotation;
        returnRotation = originalRotation; // Ýlk baþta ayný olsun
    }

    void Update()
    {
        if (isRecoiling)
        {
            // Recoil hareketi sýrasýnda hedef rotasyona yaklaþ
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * recoilSpeed);

            // Eðer hedef rotasyona ulaþýldýysa geri dönüþe baþla
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                isRecoiling = false;
            }
        }
        else
        {
            // Recoil hareketi bittiyse hafif offset eklenmiþ pozisyona dön
            transform.localRotation = Quaternion.Lerp(transform.localRotation, returnRotation, Time.deltaTime * returnSpeed);
        }
    }

    public void ApplyRecoil()
    {
        // Yukarý doðru hedef rotasyonu ayarla
        targetRotation = Quaternion.Euler(originalRotation.eulerAngles.x - recoilAmount, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z);

        // Geri dönüþte Z eksenine hafif bir fark oluþtur
        float zOffset = Random.Range(-returnOffset, returnOffset); // Z ekseninde sapma
        returnRotation = Quaternion.Euler(originalRotation.eulerAngles.x, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z + zOffset);

        isRecoiling = true;
    }

    public void Fire()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Bullet prefab veya fire point eksik!");
            return;
        }

        // Rastgele bir sapma açýsý hesapla
        float spreadX = Random.Range(-bulletSpread, bulletSpread);
        float spreadY = Random.Range(-bulletSpread, bulletSpread);

        // Sapma açýsýný uygula
        Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
        Quaternion finalRotation = firePoint.rotation * spreadRotation;

        // Mermiyi oluþtur ve yönlendir
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, finalRotation);

        // Mermiye kuvvet uygula (örneðin Rigidbody ile)
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(firePoint.forward * 1000f); // Hýz ve kuvvet
        }
    }
}
