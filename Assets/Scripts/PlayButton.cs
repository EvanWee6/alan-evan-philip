using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void PressPlay()
    {
        SceneManager.LoadScene("NameInputWindow");
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
}
