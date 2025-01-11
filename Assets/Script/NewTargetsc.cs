// GlobalScore Script with TMPro
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalScore : MonoBehaviour
{
    public static int CurrentScore;
    int InternalScore;
    public TMP_Text ScoreText; // Change from Text to TMP_Text

    void Update()
    {
        InternalScore = CurrentScore;
        ScoreText.text = InternalScore.ToString(); // Updated to use TMP_Text
    }
}