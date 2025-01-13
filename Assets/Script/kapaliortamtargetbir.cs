using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kapaliortamtargetbir : MonoBehaviour
{
    private int hitCount = 0; // Sayaç: Mermi kaç kez çarptý
    private bool isRotating = false; // Hedefin þuan devrilip devrilmediðini kontrol eder

    private void OnTriggerEnter(Collider other)
    {
        // Eðer mermiye çarptýysa
        if (other.CompareTag("Bullet"))
        {
            // Sayaç arttýr
            hitCount++;

            // Eðer mermi 2 kez çarpmýþsa ve hedef o an dönmüyorsa
            if (hitCount >= 2 && !isRotating)
            {
                Debug.Log("Mermi hedefe 2 kez çarptý. Dönüþ baþlýyor!");
                hitCount = 0; // Sayacý sýfýrla
                StartCoroutine(RotateSmoothly()); // Yavaþça döndür
            }

            // Mermiyi yok et
            Destroy(other.gameObject);
        }
    }

    private System.Collections.IEnumerator RotateSmoothly()
    {
        isRotating = true; // Rotasyon iþlemi baþladý

        float duration = 0.5f; // Rotasyonun tamamlanma süresi (1 saniye)
        float elapsedTime = 0f; // Geçen süre
        Quaternion initialRotation = transform.rotation; // Baþlangýç rotasyonu
        Quaternion targetRotation = initialRotation * Quaternion.Euler(-70, 0, 0); // Hedef rotasyon (90 derece)

        while (elapsedTime < duration)
        {
            // Zaman ilerledikçe rotasyonu interpolasyonla deðiþtir
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Bir sonraki frame'e kadar bekle
        }

        // Rotasyonu tam olarak hedef rotasyona ayarla
        transform.rotation = targetRotation;

        isRotating = false; // Rotasyon iþlemi bitti
    }
}