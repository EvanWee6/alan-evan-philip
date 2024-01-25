using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreListener : MonoBehaviour
{

    private int score;
    public GameObject goldenApple;
    public ScoreScript scoreScript;


    void Start()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateScore()
    {
        score += 1; 
        if (scoreScript != null)
        {
            scoreScript.UpdateScoreText(score);
        }
    }

    private void Update()
    {
        if (score >= 2 && goldenApple.GetComponent<GoldenAppleScript>().checkOnMap() == false)
        {
            goldenApple.GetComponent<GoldenAppleScript>().ChanceSpawn();
        }
    }


}
