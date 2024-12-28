using UnityEngine;
using TMPro; // TextMeshPro kütüphanesi

public class CounterScript : MonoBehaviour
{
    public TMP_Text counterText; // Sayaç metni
    public TMP_Text congratsText; // "Tebrikler!" metni
    private int counter = 0; // Sayacý baþlat
    private int targetCount = 12; // Hedef sayýsý

    void Start()
    {
        UpdateCounter(); // Sayacý güncelle
        congratsText.gameObject.SetActive(false); // Tebrikler Text'ini gizle
    }

    public void IncrementCounter()
    {
        counter++; // Sayaç artýr
        UpdateCounter(); // Sayaç metnini güncelle

        if (counter >= targetCount) // Hedefe ulaþýldý mý?
        {
            ShowCongratsMessage(); // Tebrikler mesajýný göster
        }
    }

    void UpdateCounter()
    {
        counterText.text = "Hedef: " + counter + "/" + targetCount; // Sayaç metnini ayarla
    }

    void ShowCongratsMessage()
    {
        congratsText.gameObject.SetActive(true); // Tebrikler Text'ini görünür yap
        counterText.gameObject.SetActive(false); // Sayaç Text'ini gizle
    }
}
