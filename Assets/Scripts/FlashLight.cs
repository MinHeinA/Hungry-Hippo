using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashLight : MonoBehaviour
{
    Light2D light2D;
    public float maxBrightness;
    public float minBrightness;
    public float drainRate;
    // Start is called before the first frame update
    private void Start()
    {
        light2D = GetComponent<Light2D>(); 
    }

    // Update is called once per frame
    private void Update()
    {
        //light2D.intensity = Mathf.Clamp(light2D, minBrightness, maxBrightness);
        //if (light2D.enabled)
        //{

        //}

        float testfloat = 0.0f;
        int testint = (int)testfloat;
    }
}
