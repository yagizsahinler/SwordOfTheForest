using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public GameObject player;
    public float spawnTimer;

    private float startTime = 0;
    private float currentTime;
    private bool timerActive = true;
    public static float finalTime;

    public int score = 0;
    public Text scoreText; // Skor UI referansı

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); // Sahne geçişlerinde yok olmasın
        }
        else if (instance != this)
        {
            Destroy(this.gameObject); // Başka bir GameManager varsa, yenisini yok et
        }
    }

    public void Start()
    {
        currentTime = 0;
        Debug.Log("Game started at: " + currentTime);
        startTime = Time.time;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void Update()
    {
        if (timerActive)
        {
            currentTime = Time.time - startTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString(); // UI'da skoru güncelle
        }
    }

    public void OnPlayerDeath()
    {
        SceneManager.LoadScene("GameOver"); // GameOver sahnesine geçiş yap
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Sahne yüklendiğinde tetiklenen fonksiyon
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Eğer oyun sahnesindeysek (Level sahnesi), referansları tekrar bul ve ata
        if (scene.name == "Level 1") // Sahne adını düzenle
        {
            GameObject scoreTextObj = GameObject.Find("Score_Text"); // ScoreText objesinin adı "ScoreText" olmalı
            if (scoreTextObj != null)
            {
                scoreText = scoreTextObj.GetComponent<Text>(); // ScoreText componentini al
                UpdateScoreUI(); // UI güncelle
            }
        }
        else if (scene.name == "GameOver")
        {
            timerActive = false;
            finalTime = currentTime;
            
            StartCoroutine(UpdateFinalScoreAndTime()); // GameOver sahnesine geçtiğimizde final skoru ve süreyi güncelle
        }
    }

    IEnumerator UpdateFinalScoreAndTime()
    {
        yield return new WaitForSeconds(0.1f); // Kısa bir gecikme

        Debug.Log("Game Over sahnesine geçildi.");

        // Final Score'u güncelle
        GameObject finalScoreObj = GameObject.Find("FinalScoreText");
        if (finalScoreObj != null)
        {
            Text finalScoreText = finalScoreObj.GetComponent<Text>();
            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + score.ToString();
                Debug.Log("Final Score güncellendi: " + score.ToString());
            }
            else
            {
                Debug.LogError("FinalScoreText bileşeni bulunamadı.");
            }
        }
        else
        {
            Debug.LogError("FinalScoreText objesi bulunamadı.");
        }
    }

    public void ResetScore()
    {
        score = 0; // Skoru sıfırla
        UpdateScoreUI(); // UI'ı güncelle
    }
}
