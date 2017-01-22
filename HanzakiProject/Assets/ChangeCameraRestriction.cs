using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeCameraRestriction : MonoBehaviour
{

    float oldRes;
    public float newRes;

    public bool triggered;


    // Use this for initialization
    void Start()
    {
        oldRes = Camera.main.GetComponent<CameraController>().restrictXn;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (triggered)
            {
                Camera.main.GetComponent<CameraController>().restrictXn = oldRes;
                triggered = false;
            }
            else
            {
                Camera.main.GetComponent<CameraController>().restrictXn = newRes;
                triggered = true;
            }
        }
    }
}
