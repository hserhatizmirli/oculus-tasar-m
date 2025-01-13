using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kapaliortamtargetbir : MonoBehaviour
{
    private int hitCount = 0; // Saya�: Mermi ka� kez �arpt�
    private bool isRotating = false; // Hedefin �uan devrilip devrilmedi�ini kontrol eder

    private void OnTriggerEnter(Collider other)
    {
        // E�er mermiye �arpt�ysa
        if (other.CompareTag("Bullet"))
        {
            // Saya� artt�r
            hitCount++;

            // E�er mermi 2 kez �arpm��sa ve hedef o an d�nm�yorsa
            if (hitCount >= 2 && !isRotating)
            {
                Debug.Log("Mermi hedefe 2 kez �arpt�. D�n�� ba�l�yor!");
                hitCount = 0; // Sayac� s�f�rla
                StartCoroutine(RotateSmoothly()); // Yava��a d�nd�r
            }

            // Mermiyi yok et
            Destroy(other.gameObject);
        }
    }

    private System.Collections.IEnumerator RotateSmoothly()
    {
        isRotating = true; // Rotasyon i�lemi ba�lad�

        float duration = 0.5f; // Rotasyonun tamamlanma s�resi (1 saniye)
        float elapsedTime = 0f; // Ge�en s�re
        Quaternion initialRotation = transform.rotation; // Ba�lang�� rotasyonu
        Quaternion targetRotation = initialRotation * Quaternion.Euler(-70, 0, 0); // Hedef rotasyon (90 derece)

        while (elapsedTime < duration)
        {
            // Zaman ilerledik�e rotasyonu interpolasyonla de�i�tir
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Bir sonraki frame'e kadar bekle
        }

        // Rotasyonu tam olarak hedef rotasyona ayarla
        transform.rotation = targetRotation;

        isRotating = false; // Rotasyon i�lemi bitti
    }
}