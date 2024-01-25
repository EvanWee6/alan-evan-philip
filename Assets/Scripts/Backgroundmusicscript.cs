using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundmusicscript : MonoBehaviour
{
    public static Backgroundmusicscript instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
