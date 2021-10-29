using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryStatus : MonoBehaviour
{
    public Slider slider;

    public void SetMaxBatteryLevel(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

    public void SetBatteryLevel(float batteryLevel)
    {
        slider.value = batteryLevel;
    }
}
