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
        pop.PopUp("W,A,S,D - Move Up, Left, Down and Right");
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
                pop.PopUp("Hello World");
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "Trg_Mandrake")
            {
                pop.PopUp(collision.gameObject.name);
                enemy.SetActive(true);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "Trg_Mandrake_2")
            {
                pop.PopUp(collision.gameObject.name);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "Trg_Water")
            {
                pop.PopUp(collision.gameObject.name);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.name == "Trg_House")
            {
                pop.PopUp(collision.gameObject.name);
                Destroy(collision.gameObject);
            }
        }
    }
        
}
