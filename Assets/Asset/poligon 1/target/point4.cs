using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point4 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // Mermiyi yok et
        DeductPoints(4);  // Artýrýlan puan miktarýný belirleyebilirsin.
    }

    void DeductPoints(int DamageAmount)
    {
        GlobalScore.CurrentScore += 4;
        Debug.Log("Score Updated: " + GlobalScore.CurrentScore + "Eklenen Deðer: " + 4);
    }
}