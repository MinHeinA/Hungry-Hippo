using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorLerp : MonoBehaviour
{
    Color lerpedColor = Color.white;
    public Color color1;
    public Color color2;
    void Update()
    { 
        lerpedColor = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, 1));
        GetComponent<Image>().color = lerpedColor;
    }
}
