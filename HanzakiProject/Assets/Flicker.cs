using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    Light light;
    float timer;
    float switchTime;
    float newIntensity;


	// Use this for initialization
	void Start ()
    {
        light = GetComponent<Light>();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if(timer > switchTime)
        {
            newIntensity = Random.Range(0, 8);
            timer = 0;
            switchTime = 1f;
        }

        if(light.intensity < newIntensity)
        {
            light.intensity += Time.deltaTime * 2;
        }
        else
        {
            light.intensity -= Time.deltaTime * 2;
        }

        
	}
}
