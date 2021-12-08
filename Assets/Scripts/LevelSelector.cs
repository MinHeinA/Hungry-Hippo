using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TutorialButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_Tutorial");
    }

    public void NewbieButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_1");
    }

    public void AmesButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_Ames");
    }

    public void BruhButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_Group3-1");
    }

    public void KaiButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_Kai");
    }

    public void SlButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_SL");
    }

    public void GhButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_gh");
    }

    public void ReButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_Yolo");
    }

}
