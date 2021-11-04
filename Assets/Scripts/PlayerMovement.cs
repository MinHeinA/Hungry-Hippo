using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float normalSpeed = 5f;
    public float slowSpeed = 3f;
    float moveSpeed;

    public Rigidbody2D rb2D;

    public Animator animator;

    public GameObject flashLight;
    public bool isSlow = false;

    public AudioSource audioSrc;
    public AudioSource mudAudio;

    Vector2 movement;

    bool canMoveOnX, canMoveOnY = false;

    private void Start()
    {
        moveSpeed = normalSpeed;
    }

    // Handle User Input and animator variable assignments
    void Update()
    {
        // Debug.Log(GameOverScreen.gameIsOver);
        if (!GameOverScreen.gameIsOver && !PauseMenu.gameIsPaused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // set variables to play appropriate directional move animation
            if (canMoveOnX) { animator.SetFloat("moveX", movement.x); }
            if (canMoveOnY) { animator.SetFloat("moveY", movement.y); }

            // set variable for animation transition between idle and move animation states 
            animator.SetFloat("speed", movement.sqrMagnitude);

            // set variables to play appropriate directional idle animation
            // this is needed to "remember" the where the character was facing
            if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
            {
                animator.SetFloat("lastMoveX", movement.x);
                animator.SetFloat("lastMoveY", movement.y);
            }
        }
    }

    //Handle Player movement
    void FixedUpdate()
    {
        if (FindObjectOfType<GameOverScreen>().isGameOver())
        {
            mudAudio.Stop();
            audioSrc.Stop();
        }
        else
        {
            PlayerMove();
            RotateFlashlight();
        }
    }

    void PlayerMove()
    {
        //Locks player movement to only one direction
        if (canMoveOnX && movement.x != 0)
        {
            canMoveOnY = false;
            //rb2D.velocity = new Vector2 (movement.x, 0.0f) * moveSpeed;
            rb2D.MovePosition(rb2D.position + new Vector2(movement.x, 0.0f) * moveSpeed * Time.fixedDeltaTime);
        }
        else if (canMoveOnY && movement.y != 0)
        {
            canMoveOnX = false;
            //rb2D.velocity = new Vector2(0.0f, movement.y) * moveSpeed;
            rb2D.MovePosition(rb2D.position + new Vector2(0.0f, movement.y) * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb2D.MovePosition(rb2D.position + new Vector2(0.0f, 0.0f));
        }
        if (movement.y == 0)
        {
            canMoveOnX = true;
        }
        if (movement.x == 0)
        {
            canMoveOnY = true;
        }

        if (!canMoveOnX || !canMoveOnY)
        {
            if (isSlow)
            {
                if (!mudAudio.isPlaying)
                {
                    audioSrc.Stop();
                    mudAudio.Play();
                }
            }
            else
            {
                if (!audioSrc.isPlaying)
                {
                    mudAudio.Stop();
                    audioSrc.Play();
                }
            }
        }
        else
        {
            mudAudio.Stop();
            audioSrc.Stop();
        }

    }

    void RotateFlashlight()
    {
        //rotates child flashlight gameobject according to player movement direction 
        var rotationVector = transform.rotation.eulerAngles;
        if (movement.x > 0.1f && canMoveOnX)
        {
            rotationVector.z = 90;
            flashLight.transform.rotation = Quaternion.Euler(rotationVector);
        }
        else if (movement.x < -0.1f && canMoveOnX)
        {
            rotationVector.z = -90;
            flashLight.transform.rotation = Quaternion.Euler(rotationVector);
        }
        else if (movement.y > 0.1f && canMoveOnY)
        {
            rotationVector.z = 180;
            flashLight.transform.rotation = Quaternion.Euler(rotationVector);
        }
        else if (movement.y < -0.1f && canMoveOnY)
        {
            rotationVector.z = 0;
            flashLight.transform.rotation = Quaternion.Euler(rotationVector);
        }
    }

    public void slowPlayer()
    {
        moveSpeed = slowSpeed;
        isSlow = true;
    }

    public void resetPlayerSpeed()
    {
        moveSpeed = normalSpeed;
        isSlow = false;
    }
}