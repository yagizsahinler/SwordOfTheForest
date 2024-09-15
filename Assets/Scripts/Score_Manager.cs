using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = Game_Manager.instance.score.ToString();
    }
}