using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private float timer = 0f;
    private bool isTimerRunning = false;
 

    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            Debug.Log("Timer: " + timer);
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
    }

    public void EndTimer() {
        isTimerRunning = false;
    }

    public float GetTime() {
        return timer;
    }

}
