using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    Color lerpedColor = Color.white;
    public Color color1;
    public Color color2;
    bool isChasing = false;
    void Update()
    {
        isChasing = false;
        var enemies = FindObjectsOfType<EnemyMovement>();
        foreach (var enemy in enemies)
        {
            if (enemy.hippostate >= 2)
            {
                isChasing = true;
            } 
        }
        if (isChasing)
        {
            lerpedColor = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
            GetComponent<Image>().color = lerpedColor;
        } 
        else
        {
            GetComponent<Image>().color = Color.clear;
        }
        
    }
}
