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
    
 
    public GameObject InputButtons;
    private string input;

    // Start is called before the first frame update


    public void Back()
    {
        SceneManager.LoadScene("mainmenu");
    }
    
    public void ReadUserInput(string s)
    {
        input = s;
    }

    public void Okay(string s)
    {
        input = s;
        Debug.Log(input);
        SceneManager.LoadScene("MainScene");
    }
    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
