using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    PolygonCollider2D flashLightCollider;
    [SerializeField]
    Light2D light2D;

    public float maxBrightness;
    public float minBrightness;
    public float drainRate;
    public bool batteryDrain = true;

    public float maxBatteryLevel = 100;
    public float currentBatteryLevel;
    public bool batteryDead = false;
    public BatteryStatus batteryStatus;

    // Start is called before the first frame update
    private void Start()
    {
        flashLightCollider = GetComponent<PolygonCollider2D>();
        light2D = GetComponentInChildren<Light2D>();
        light2D.intensity = maxBrightness;
        flashLightCollider.enabled = false;
        light2D.enabled = false;

        currentBatteryLevel = maxBatteryLevel;
        batteryStatus.SetMaxBatteryLevel();
    }

    // Update is called once per frame
    private void Update()
    {
        light2D.intensity = Mathf.Clamp(light2D.intensity, minBrightness, maxBrightness);

        if (batteryDead)
        {
            flashLightCollider.enabled = false;
            light2D.enabled = false;
        }

        if (light2D.enabled && batteryDrain)
        {
            if (currentBatteryLevel > 0.2) {
                depleteBattery((float)0.2);
            } 
            else
            {
                batteryDead = true;
            }

            if (light2D.intensity > minBrightness)
            {
                light2D.intensity -= Time.deltaTime * (drainRate / 1000);
            }
            else
            {
                flashLightCollider.enabled = false;
            }
        }
    }

    void depleteBattery(float batteryLevel)
    {
        currentBatteryLevel -= batteryLevel;
        batteryStatus.SetBatteryLevel(currentBatteryLevel);
    }

    // Call this method when player obtain battery. 
    void rechargeBattery(float batteryLevel)
    {
        currentBatteryLevel = maxBatteryLevel;
        batteryStatus.SetMaxBatteryLevel();
    }

    public void ToogleFlashlight()
    {
        if (!(batteryDead))
        {
            if (!light2D.enabled && light2D.intensity > minBrightness)
            {
                flashLightCollider.enabled = true;
                light2D.enabled = true;
            }
            else
            {
                flashLightCollider.enabled = false;
                light2D.enabled = false;
            }
        }
    }
}
