using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI gameOverTextUI;

    public void Setup(string gameOverText)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        gameOverTextUI.text = gameOverText;
    } 
    
    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        //TO-DO change to Main Menu once main menu is implemented
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_1");
    }
    
}
