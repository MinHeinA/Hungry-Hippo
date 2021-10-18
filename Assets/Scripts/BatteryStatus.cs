using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryStatus : MonoBehaviour
{
    public Slider slider;

    public void SetMaxBatteryLevel()
    {
        slider.maxValue = 100;
        slider.value = 100;
    }

    public void SetBatteryLevel(float batteryLevel)
    {
        slider.value = batteryLevel;
    }
}
