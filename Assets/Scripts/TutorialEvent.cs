using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    PopUpSystem pop;

    void Start()
    {
        //Empty Battery
        pop = FindObjectOfType<PopUpSystem>();
        pop.gameObject.SetActive(true);
        Invoke("EmptyBattery", 0.5f);
        //Show WASD Instructions
        pop.PopUp("W,A,S,D - Move Up, Left, Down and Right, Space - Toggle Flashlight");
    }
    void EmptyBattery()
    {
        FindObjectOfType<FlashLight>().EmptyBattery();
        FindObjectOfType<FlashLight>().batteryDead = true;  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trigger")
        {
            if (collision.gameObject.name == "Trg_Battery")
            {
                pop.PopUp("You have just picked up a battery! Batteries are scattered across the map and is used to replenish your flashlight power.");
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "Trg_Mandrake")
            {
                pop.PopUp("Your objective is to collect all the mandrakes in the map and get to the safe house. Flashlight can be used to stun the hippos if you encounter them.");
                enemy.SetActive(true);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "Trg_Mandrake_2")
            {
                pop.PopUp("Once all mandrakes have been collected, RUN! Flashlight will no longer be able to stun the hippo.");
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "Trg_Water")
            {
                pop.PopUp("Water puddles will slow your movement speed, so avoid them if possible!");
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "Trg_House")
            {
                pop.PopUp("Look! There is a safehouse ahead! Get in quick!");
                Destroy(collision.gameObject);
            }
        }
    }
        
}
