using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public static bool gameIsOver = false;
    public TextMeshProUGUI gameOverTextUI;
    public GameObject gameOverUI;
    public GameObject playerUI;
    public AudioSource deathSound;
    public AudioSource bgSound;

    void Start()
    {
        gameIsOver = false;
    }
    public void Setup(string gameOverText)
    {
        gameOverUI.SetActive(true);
        playerUI.SetActive(false);
        gameOverTextUI.text = gameOverText;
        bgSound.Stop();
        gameIsOver = true;
        Invoke("StopTime", 0.1f);
    }
    void StopTime()
    {
        Time.timeScale = 0f;
    }
    public bool isGameOver()
    {
        return gameIsOver;
    }
    
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayDeathSound()
    {
        deathSound.Play(0);
    }
}
