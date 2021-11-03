using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class PlayerInteraction : MonoBehaviour
{
    private int mandrakeCollected = 0;
    private int noOfMandrakes = 0;
    public TextMeshProUGUI score;
    public GameOverScreen gameOverScreen;

    private void Start()
    {
        noOfMandrakes = GameObject.FindGameObjectsWithTag("Mandrake").Length;
        score.text = mandrakeCollected.ToString() + " / " + noOfMandrakes.ToString();
    }

    private void GameOver(string txt, bool isDead)
    {
        gameOverScreen.Setup(txt);

        if (isDead)
        {
            gameOverScreen.PlayDeathSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish" && mandrakeCollected == noOfMandrakes)
        {
            GameOver("YOU WIN!", false);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            GameOver("YOU DIED!", true);
        }

        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Mandrake")
        {
            mandrakeCollected++;
            score.text = mandrakeCollected.ToString() + " / " + noOfMandrakes.ToString();
            collision.gameObject.GetComponent<Mandrake>().PlayShriek();
            GameObject.Find("One shot audio").GetComponent<AudioSource>().spatialBlend = 0.0f;

            EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
            if (mandrakeCollected == noOfMandrakes)
            {
                foreach (EnemyMovement enemy in enemies)
                {
                    enemy.Crazy();
                }
            }
            else
            {
                // make only the nearest enemy go to mandrake
                int minindex = 0;
                float mindist = -1f;
                float mandrakex = collision.gameObject.transform.position.x, mandrakey = collision.gameObject.transform.position.y;

                for (int i = 0; i < enemies.Length; i++)
                {
                    float enemyx = enemies[i].gameObject.transform.position.x, enemyy = enemies[i].gameObject.transform.position.y;
                    // calculate distance of hippo to crystal
                    float dist = (mandrakex - enemyx) * (mandrakex - enemyx) + (mandrakey - enemyy) * (mandrakey - enemyy);
                    if (mindist == -1f || dist < mindist)
                    {
                        minindex = i;
                        mindist = dist;
                    }
                }

                enemies[minindex].ChaseCrystal((int)mandrakex, (int)mandrakey);
            }

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Battery")
        {
            FindObjectOfType<FlashLight>().rechargeBattery();
            FindObjectOfType<FlashLight>().batteryDead = false;
            Destroy(collision.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StickyTrap")
        {
            FindObjectOfType<PlayerMovement>().slowPlayer();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        FindObjectOfType<PlayerMovement>().resetPlayerSpeed();
    }

}