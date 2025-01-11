using UnityEngine;
using TMPro; // TextMeshPro kullanýmý için gerekli

public class TargetScoring : MonoBehaviour
{
    public int totalScore = 0; // Toplam puan
    public TextMeshProUGUI scoreText; // Skoru göstermek için kullanýlan UI bileþeni
    private bool specificZoneHit = false; // Belirli bir bölgeye isabet edildiðini izler

    void Start()
    {
        Debug.Log("Game Started. Initial Score: " + totalScore);
        UpdateScoreText(); // Baþlangýçta skor metnini güncelle
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.name);

        // Çarpýlan nesnenin TargetZone bileþenini al
        TargetZone zone = other.GetComponent<TargetZone>();
        if (zone != null)
        {
            Debug.Log("Target zone points: " + zone.points);

            // Eðer çarpýlan bölge 7 puanlýysa ve baþka bir spesifik bölgeye çarpýlmadýysa
            if (zone.points == 7 && !specificZoneHit)
            {
                totalScore += zone.points;
                Debug.Log("Hit 7-point zone. Total Score: " + totalScore);
            }
            else if (zone.points != 7)
            {
                specificZoneHit = true; // Spesifik bir bölgeye çarpýldý
                totalScore += zone.points;
                Debug.Log("Hit specific zone worth " + zone.points + " points. Total Score: " + totalScore);
            }

            UpdateScoreText(); // Skor metnini güncelle
        }
        else
        {
            Debug.LogWarning("The object hit does not have a TargetZone component.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited collision with: " + other.name);

        // Çarpýþma sona erdiðinde bayraðý sýfýrla
        TargetZone zone = other.GetComponent<TargetZone>();
        if (zone != null && zone.points != 7)
        {
            specificZoneHit = false;
            Debug.Log("specificZoneHit flag reset.");
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
            Debug.Log("Score updated to: " + totalScore);
        }
        else
        {
            Debug.LogError("ScoreText is not assigned in the inspector.");
        }
    }
}
