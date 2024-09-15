using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text TimerText; // Zamanı gösterecek Text nesnesi
    private float time = 0f;

    private bool isRunning = false; // Kronometre çalışıyor mu kontrolü

    void Start()
    {
        // Oyun başladığında kronometreyi başlat
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            time += Time.deltaTime;
            string dakika = Mathf.FloorToInt(time / 60).ToString("00");
            string saniye = Mathf.FloorToInt(time % 60).ToString("00");
            string milisaniye = Mathf.FloorToInt((time * 100) % 100).ToString("00");

            TimerText.text = dakika + ":" + saniye + ":" + milisaniye;
        }
    }

    public void GameOver()
    {
        // Game Over olduğunda kronometreyi durdur ve sahneyi değiştir
        isRunning = false;

        SceneManager.LoadScene("Game Over"); // Game Over sahnesinin adını buraya yaz
    }
}