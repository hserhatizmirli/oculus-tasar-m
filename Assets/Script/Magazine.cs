using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int bulletsInMagazine = 30; // Þarjördeki mermi sayýsý
    public bool isEmpty = false;      // Þarjör boþ mu?
    public void DetachFromGun()
    {
        // Þarjör yere düþerken fizik etkinleþtirilir
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }

    public void UseBullet()
    {
        if (bulletsInMagazine > 0)
        {
            bulletsInMagazine--;
            if (bulletsInMagazine <= 0)
            {
                isEmpty = true;
            }
        }
    }
}
