using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb2D;

    public Animator animator;

    public GameObject flashLight;

    Vector2 movement;

    bool canMoveOnX, canMoveOnY = false;

    // Handle User Input
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1)
        {
            animator.SetFloat("lastMoveX", movement.x);
            animator.SetFloat("lastMoveY", movement.y);
        }
    }

    //Handle Movement
    void FixedUpdate()
    {
        PlayerMove();
        RotateFlashlight();
    }
    void PlayerMove()
    {
        if (canMoveOnX && movement.x != 0)
        {
            canMoveOnY = false;
            rb2D.MovePosition(rb2D.position + new Vector2(movement.x, 0.0f) * moveSpeed * Time.fixedDeltaTime);
        }
        if (canMoveOnY && movement.y != 0)
        {
            canMoveOnX = false;
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

    }

    void RotateFlashlight()
    {
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
}
