using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreListener : MonoBehaviour
{

    private int score;

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
    }

    
}
