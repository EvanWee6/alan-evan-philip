using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using CodeMonkey;

public class UI_InputWindow : MonoBehaviour
{
    [SerializeField] private Highscore highscoreTable;

    private string playerName;  // Variable to store the player's name

    public void Back()
    {
        SceneManager.LoadScene("mainmenu");
    }

    public void ReadUserInput(string s)
    {
        playerName = s;  // Capture the player's name input
    }

    public void Okay(string s)
    {
        Debug.Log(playerName);

        SceneManager.LoadScene("MainScene"); 
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    void Start()
    {

    }
    void Update()
    {

    }
}