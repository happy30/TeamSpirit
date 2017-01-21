using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

public class Numberer : MonoBehaviour
{
    public GameObject parentObject;
    public int counter;
    public bool run;
    public int numberedObjects;
#if UNITY_EDITOR
    void OnValidate()
    {
        if (run)
        {
            foreach (Transform child in parentObject.transform)
            {
                child.transform.GetComponent<Checkpoints>().checkpointNumber = counter;
                counter++;
            }
            run = false;
            numberedObjects = counter;
            counter = 0;
        }
    }
#endif
}