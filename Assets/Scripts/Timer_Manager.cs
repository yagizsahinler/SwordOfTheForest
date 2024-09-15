using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Manager : MonoBehaviour
{
    public Text timerText;

    public static float finalTime;
    void Start()
    {
        timerText = GetComponent<Text>();
        timerText.text = Game_Manager.finalTime.ToString() + " seconds";
    }
}
