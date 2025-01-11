using UnityEngine;
using TMPro; // TextMeshPro kütüphanesi

public class Counter : MonoBehaviour
{
    public TMP_Text counterText; // Sayaç metni için TextMeshPro referansý
    public TMP_Text congratsText; // "Tebrikler!" metni için TextMeshPro referansý
    private int counter = 0; // Sayaç baþlangýç deðeri
    private int targetCount = 12; // Hedef sayý

    void Start()
    {
        UpdateCounter(); // Sayaç metnini güncelle
        congratsText.gameObject.SetActive(false); // "Tebrikler!" metnini baþlangýçta gizle
    }

    public void IncrementCounter()
    {
        counter++; // Sayaç deðerini artýr
        UpdateCounter(); // Sayaç metnini güncelle

        if (counter >= targetCount) // Sayaç hedefe ulaþtý mý?
        {
            ShowCongratsMessage(); // "Tebrikler!" mesajýný göster
        }
    }

    void UpdateCounter()
    {
        counterText.text = "Hedef: " + counter + "/" + targetCount; // Sayaç metnini güncelle
    }

    void ShowCongratsMessage()
    {
        congratsText.gameObject.SetActive(true); // "Tebrikler!" metnini görünür yap
        counterText.gameObject.SetActive(false); // Sayaç metnini gizle
    }
}
