using UnityEngine;

public class BoltHandle : MonoBehaviour
{
    private Rigidbody rb;
    public Transform boltSlot; // Sürgü kolunun baðlý olacaðý yer
    private bool isAttached = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Baþlangýçta yere düþmesin
    }

    public void Detach()
    {
        isAttached = false;
        rb.isKinematic = false; // Fizik etkinleþtirilir
        transform.parent = null; // Baðlantý kopar
    }

    public void Attach()
    {
        isAttached = true;
        transform.SetParent(boltSlot); // Sürgü yuvasýna otur
        transform.localPosition = Vector3.zero; // Pozisyon sýfýrlanýr
        transform.localRotation = Quaternion.identity; // Rotasyon sýfýrlanýr
        rb.isKinematic = true; // Fizik kapatýlýr
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoltHandle") && !isAttached)
        {
            BoltHandle bolt = other.GetComponent<BoltHandle>();
            if (bolt != null)
            {
                bolt.Attach();
            }
        }
    }

}
