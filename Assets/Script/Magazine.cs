using UnityEngine;

public class Magazine : MonoBehaviour
{
    public int bulletsInMagazine = 30; // �arj�rdeki mermi say�s�
    public bool isEmpty = false;      // �arj�r bo� mu?
    public void DetachFromGun()
    {
        // �arj�r yere d��erken fizik etkinle�tirilir
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
