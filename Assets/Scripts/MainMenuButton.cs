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
        SceneManager.LoadScene("Level_1");
    }
}
