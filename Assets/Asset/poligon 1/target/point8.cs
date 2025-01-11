using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point8 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // Mermiyi yok et
        DeductPoints(8);  // Artýrýlan puan miktarýný belirleyebilirsin.
    }
    void DeductPoints(int DamageAmount)
    {
        GlobalScore.CurrentScore += 8;
        Debug.Log("Score Updated: " + GlobalScore.CurrentScore+ "Eklenen Deðer: " + 8 );
    }
}