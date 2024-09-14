using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public GameObject player;
    public Timer timer; // Timer referansı
    public float spawnTimer;

    public int score = 0;
    public Text scoreText; // Skor UI referansı

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne geçişlerinde yok olmasın
        }
        else
        {
            Destroy(gameObject); // Başka bir GameManager varsa, yenisini yok et
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
        Debug.Log("OnEnable: sceneLoaded event eklendi.");
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("OnDisable: sceneLoaded event kaldırıldı.");
    }

    // Sahne yüklendiğinde tetiklenen fonksiyon
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Eğer oyun sahnesindeysek (Level sahnesi), referansları tekrar bul ve ata
        if (scene.name == "Level 1") // Sahne adını düzenle
        {
            // Timer ve ScoreText referanslarını sahnedeki objelerden al
            GameObject timerObj = GameObject.Find("Timer"); // Timer objesinin adı "Timer" olmalı
            if (timerObj != null)
            {
                timer = timerObj.GetComponent<Timer>(); // Timer componentini al
            }

            GameObject scoreTextObj = GameObject.Find("Score_Text"); // ScoreText objesinin adı "ScoreText" olmalı
            if (scoreTextObj != null)
            {
                scoreText = scoreTextObj.GetComponent<Text>(); // ScoreText componentini al
                UpdateScoreUI(); // UI güncelle
            }
        }
        else if (scene.name == "Game Over")
        {
            StartCoroutine(UpdateFinalScore()); // GameOver sahnesine geçtiğimizde final skoru güncelle
        }
    }

    IEnumerator UpdateFinalScore()
    {
        yield return new WaitForSeconds(0.1f); // Kısa bir gecikme

        Debug.Log("Game Over sahnesine geçildi.");
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
