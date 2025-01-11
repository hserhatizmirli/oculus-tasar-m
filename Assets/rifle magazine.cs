using UnityEngine;

public class RifleMagazine : MonoBehaviour
{
    public int maxBullets = 10; // Þarjör kapasitesi
    public int currentBullets; // Mevcut mermi sayýsý

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
