using UnityEngine;
using TMPro; // TextMeshPro k�t�phanesi

public class Counter : MonoBehaviour
{
    public TMP_Text counterText; // Saya� metni i�in TextMeshPro referans�
    public TMP_Text congratsText; // "Tebrikler!" metni i�in TextMeshPro referans�
    private int counter = 0; // Saya� ba�lang�� de�eri
    private int targetCount = 12; // Hedef say�

    void Start()
    {
        UpdateCounter(); // Saya� metnini g�ncelle
        congratsText.gameObject.SetActive(false); // "Tebrikler!" metnini ba�lang��ta gizle
    }

    public void IncrementCounter()
    {
        counter++; // Saya� de�erini art�r
        UpdateCounter(); // Saya� metnini g�ncelle

        if (counter >= targetCount) // Saya� hedefe ula�t� m�?
        {
            ShowCongratsMessage(); // "Tebrikler!" mesaj�n� g�ster
        }
    }

    void UpdateCounter()
    {
        counterText.text = "Hedef: " + counter + "/" + targetCount; // Saya� metnini g�ncelle
    }

    void ShowCongratsMessage()
    {
        congratsText.gameObject.SetActive(true); // "Tebrikler!" metnini g�r�n�r yap
        counterText.gameObject.SetActive(false); // Saya� metnini gizle
    }
}
