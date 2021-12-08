using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    public GameObject popUpBox;
    public Animator animator;
    public TMP_Text popUpText;
    // Start is called before the first frame update
    public void PopUp(string text)
    {
        popUpBox.SetActive(true);
        popUpText.text = text;
        animator.SetTrigger("open");
        Invoke("StopTime", 0.3f);
    }
    void StopTime()
    {
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && animator.GetCurrentAnimatorStateInfo(0).IsName("PopUp_Open"))
        {
            Time.timeScale = 1f;
            animator.SetTrigger("close");
        }
    }
}
