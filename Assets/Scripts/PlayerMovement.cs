using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb2D;

    public Animator animator;

    public GameObject flashLight;

    public AudioSource audioSrc;

    Vector2 movement;

    bool canMoveOnX, canMoveOnY = false;

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
            audioSrc.Stop();
        } else
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
            // rb2D.velocity = new Vector2 (movement.x, 0.0f) * moveSpeed;
            rb2D.MovePosition(rb2D.position + new Vector2(movement.x, 0.0f) * moveSpeed * Time.fixedDeltaTime);
        }
        if (canMoveOnY && movement.y != 0)
        {
            canMoveOnX = false;
            // rb2D.velocity = new Vector2(0.0f, movement.y) * moveSpeed;
            rb2D.MovePosition(rb2D.position + new Vector2(0.0f, movement.y) * moveSpeed * Time.fixedDeltaTime);
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
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
        }
        else
        {
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
        else if (movement.x < -0.1f && canMoveOnX) { 
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
        moveSpeed = 2f;
    }

    public void resetPlayerSpeed()
    {
        moveSpeed = 5f;
    }
}
