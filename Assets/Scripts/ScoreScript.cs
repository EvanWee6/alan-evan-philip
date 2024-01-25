using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import the TextMeshPro namespace

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Use TextMeshProUGUI for TextMeshPro
    public TextMeshProUGUI CoinsText; // Use TextMeshProUGUI for TextMeshPro
    public TextMeshProUGUI speedText; // Use TextMeshProUGUI for TextMeshPro



    int score = 0;
    int Coins = 0;
    int speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "POINTS: " + score.ToString();
        CoinsText.text = "COINS: " + Coins.ToString();
        speedText.text = "SPEED: " + speed.ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScoreText(int newScore)
    {
        score = newScore;
        scoreText.text = "POINTS: " + score.ToString();
    }
    public void UpdateCoinsText(int newCoins)
    {
        Coins = newCoins;
        CoinsText.text = "COINS: " + Coins.ToString();

    }
    public void UpdateSpeedText(int newSpeed)
    {
        speed = newSpeed - 1;
        speedText.text = "SPEED: " + speed.ToString();
    }
}
