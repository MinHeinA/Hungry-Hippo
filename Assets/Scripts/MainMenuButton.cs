using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    void Start() {
    }

    // When start button is pressed
    public void StartButton() {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelector");
    }

    public void NextButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelector2");
    }

    public void PreviousButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelector");
    }
}
