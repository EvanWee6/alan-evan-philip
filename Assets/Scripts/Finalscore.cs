using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Finalscore : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Use TextMeshProUGUI for TextMeshPro

    int score = 0;
    
    void Start()
    {
        // Retrieve the score from PlayerPrefs when the scene starts
        score = PlayerPrefs.GetInt("SavedScore", 0);
        scoreText.text = "POINTS: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to receive the score when the event is triggered
    private void ReceiveScore(int newScore)
    {
        score = newScore;
        scoreText.text = "POINTS: " + score.ToString();

    }
}

