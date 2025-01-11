using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point5: MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // Mermiyi yok et
        DeductPoints(5);  // Artýrýlan puan miktarýný belirleyebilirsin.
    }
    void DeductPoints(int DamageAmount)
    {
        GlobalScore.CurrentScore += 5;
        Debug.Log("Score Updated: " + GlobalScore.CurrentScore + "Eklenen Deðer: " + 5);
    }
}