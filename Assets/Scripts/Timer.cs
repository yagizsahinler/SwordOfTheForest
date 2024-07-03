using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("timerText is not assigned!");
            return;
        }

        startTime = Time.time;
    }

    void Update()
    {
        if (timerText == null)
        {
            return;
        }

        float t = Time.time - startTime; // Zamanı başlatma anından itibaren hesaplayın

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }
}
