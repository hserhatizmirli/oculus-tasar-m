using UnityEngine;

public class BoltHandle : MonoBehaviour
{
    private Rigidbody rb;
    public Transform boltSlot; // S�rg� kolunun ba�l� olaca�� yer
    private bool isAttached = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Ba�lang��ta yere d��mesin
    }

    public void Detach()
    {
        isAttached = false;
        rb.isKinematic = false; // Fizik etkinle�tirilir
        transform.parent = null; // Ba�lant� kopar
    }

    public void Attach()
    {
        isAttached = true;
        transform.SetParent(boltSlot); // S�rg� yuvas�na otur
        transform.localPosition = Vector3.zero; // Pozisyon s�f�rlan�r
        transform.localRotation = Quaternion.identity; // Rotasyon s�f�rlan�r
        rb.isKinematic = true; // Fizik kapat�l�r
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
