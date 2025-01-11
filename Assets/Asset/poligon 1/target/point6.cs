using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point6 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // Mermiyi yok et
        DeductPoints(6);  // Artýrýlan puan miktarýný belirleyebilirsin.
    }
    void DeductPoints(int DamageAmount)
    {
        GlobalScore.CurrentScore += 6;
        Debug.Log("Score Updated: " + GlobalScore.CurrentScore + "Eklenen Deðer: " + 6);
    }
}