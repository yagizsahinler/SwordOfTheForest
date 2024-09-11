using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public Game_Manager instance;
    
    public GameObject player;

    public Timer timer;
    public float spawnTimer;

    public int score = 0;
    public Text scoreText;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // GameManager sahne geçişlerinde yok olmaz
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }

    public void OnPlayerDeath()
    {
        SceneManager.LoadScene("GameOver"); // "GameOver" sahnesine geçiş yap
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOver")
        {
            GameObject finalScoreObj = GameObject.Find("FinalScoreText");
            if (finalScoreObj != null)
            {
                Text finalScoreText = finalScoreObj.GetComponent<Text>();
                if (finalScoreText != null)
                {
                    finalScoreText.text = "Final Score: " + score.ToString();
                }
            }
        }
    }
}
