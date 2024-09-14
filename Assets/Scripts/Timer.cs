using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText; // Zamanı gösteren UI text elemanı
    private float startTime;
    private bool isRunning = true; // Zamanlayıcının çalışıp çalışmadığını kontrol eder

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("timerText is not assigned!");
            return;
        }

        startTime = Time.time; // Zamanlayıcıyı başlat
    }

    void Update()
    {
        if (!isRunning) return; // Zamanlayıcı durdurulmuşsa güncelleme yapma

        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (timerText == null) return;

        float t = Time.time - startTime; // Başlangıç zamanından itibaren geçen süre

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds; // UI üzerinde güncelleme yap
    }

    // Timer'ı sıfırla
    public void ResetTimer()
    {
        startTime = Time.time;
        isRunning = true; // Tekrar başlat
    }

    // Timer'ı durdur
    public void StopTimer()
    {
        isRunning = false;
    }

    // Timer'ı başlat
    public void StartTimer()
    {
        isRunning = true;
    }

    // Timer'dan geçen süreyi dışarıdan erişim için döner
    public float GetCurrentTime()
    {
        return Time.time - startTime;
    }
}
