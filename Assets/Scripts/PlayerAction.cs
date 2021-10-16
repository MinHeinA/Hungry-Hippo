using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerAction : MonoBehaviour
{
    FlashLight flashlight;

    // Update is called once per frame
    private void Start()
    {
        flashlight = FindObjectOfType<FlashLight>();
    }
    void Update()
    {
        //Spacebar to toggle flashlight on and off
        if (Input.GetKeyDown(KeyCode.Space) && !GameOverScreen.gameIsOver && !PauseMenu.gameIsPaused)
        {
            flashlight.ToogleFlashlight();
        }
    }
}
