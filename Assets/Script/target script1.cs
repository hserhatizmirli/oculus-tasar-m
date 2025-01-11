using System.Collections.Generic;
using UnityEngine;

public class Target_System : MonoBehaviour
{
    [Header("Daire Ayarlarý")]
    public Transform targetCenter; // Hedefin merkezi
    public float innerCircleRadius = 3f; // Küçük dairenin yarýçapý
    public float outerCircleRadius = 6f; // Büyük dairenin yarýçapý

    [Header("Capsule Ayarlarý")]
    public int capsuleCount = 25; // Kapsül sayýsý
    public float capsuleRadius = 10f; // Kapsüllerin yerleþtirileceði yarýçap

    [Header("Puan Ayarlarý")]
    public int innerCircleScore = 9; // Küçük daire puaný
    public int outerCircleScore = 8; // Büyük daire puaný
    public int maxCapsuleScore = 10; // Kapsüller için maksimum puan

    [Header("Ýstatistikler")]
    public int totalShots = 0; // Toplam atýþ sayýsý
    public int successfulHits = 0; // Baþarýlý isabet sayýsý

    private List<CapsuleCollider> capsules = new List<CapsuleCollider>(); // Kapsüllerin collider listesi

    void Start()
    {
        // Daireleri ve kapsülleri kur
        SetupCapsules();
    }

    private void SetupCapsules()
    {
        for (int i = 0; i < capsuleCount; i++)
        {
            // Kapsülü dairesel bir düzene yerleþtir
            float angle = i * (360f / capsuleCount); // Eþit açýlarla yerleþtir
            Vector3 position = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * capsuleRadius,
                0,
                Mathf.Sin(angle * Mathf.Deg2Rad) * capsuleRadius
            );

            // Kapsül collider oluþtur ve ekle
            GameObject capsuleObject = new GameObject($"Capsule {i + 1}");
            capsuleObject.transform.SetParent(transform);
            capsuleObject.transform.localPosition = position;
            capsuleObject.transform.LookAt(targetCenter.position);

            CapsuleCollider capsuleCollider = capsuleObject.AddComponent<CapsuleCollider>();
            capsuleCollider.direction = 1; // Y ekseni boyunca hizala
            capsuleCollider.height = 2f;
            capsuleCollider.radius = 0.5f;

            // Kapsülün puanýný ayarla
            int capsuleScore = Mathf.Max(1, maxCapsuleScore - i); // Kapsül puanlarýný azaltarak ata
            CapsuleScore capsuleScoreComponent = capsuleObject.AddComponent<CapsuleScore>();
            capsuleScoreComponent.score = capsuleScore;

            capsules.Add(capsuleCollider);
        }

        Debug.Log("Kapsüller baþarýyla oluþturuldu.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Atýþ kontrolü
        totalShots++;

        // Çarpýþma noktasý
        Vector3 hitPoint = collision.GetContact(0).point;
        float distanceFromCenter = Vector3.Distance(hitPoint, targetCenter.position);

        // Puan hesaplama
        if (distanceFromCenter <= innerCircleRadius)
        {
            successfulHits++;
            Debug.Log($"Ýç daire isabet! Puan: {innerCircleScore}");
        }
        else if (distanceFromCenter <= outerCircleRadius)
        {
            successfulHits++;
            Debug.Log($"Dýþ daire isabet! Puan: {outerCircleScore}");
        }
        else
        {
            foreach (CapsuleCollider capsule in capsules)
            {
                if (capsule.bounds.Contains(hitPoint))
                {
                    successfulHits++;
                    int capsuleScore = capsule.GetComponent<CapsuleScore>().score;
                    Debug.Log($"Kapsül isabet! Puan: {capsuleScore}");
                    break;
                }
            }
        }

        Debug.Log($"Baþarý oraný: {(float)successfulHits / totalShots:P2}");
    }
}

// Kapsülün puanýný tutan yardýmcý sýnýf
public class Capsule_Score : MonoBehaviour
{
    public int score;
}
