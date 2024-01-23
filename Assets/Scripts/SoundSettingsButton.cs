using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundSettingsButton : MonoBehaviour
{
    public void PressSound()
    {
        SceneManager.LoadScene("Soundmenu");
    }
}
