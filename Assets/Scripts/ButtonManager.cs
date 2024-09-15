using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Eğer GameManager sahneler arası kalıcıysa (DontDestroyOnLoad), skoru sıfırlayın
        if (Game_Manager.instance != null)
        {
            Game_Manager.instance.ResetScore(); // Skoru sıfırla
        }

        // Oyunu başlatacak sahneyi yükle
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame called!");
        Application.Quit(); // Uygulamayı kapat
    }

    public void Controls()
    {
        SceneManager.LoadScene("How To Play");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
