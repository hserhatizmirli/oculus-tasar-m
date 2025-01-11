using UnityEngine;

public class RifleMagazine : MonoBehaviour
{
    public int maxBullets = 10; // �arj�r kapasitesi
    public int currentBullets; // Mevcut mermi say�s�

    void Start()
    {
        currentBullets = maxBullets;
    }

    public bool IsEmpty() => currentBullets <= 0;

    public void ConsumeBullet()
    {
        if (currentBullets > 0)
        {
            currentBullets--;
        }
    }

    public void Reload()
    {
        currentBullets = maxBullets;
    }
}
