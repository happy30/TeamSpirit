using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renamer : MonoBehaviour {
    public GameObject parentObject;
    GameObject handle;
    public string baseName;
    public int counter;
    string giveName;
    public bool run;
    public int renamedObjects;

    void OnValidate()
    {
        if (run)
        {
            foreach (Transform child in parentObject.transform)
            {
                if (counter < 10)
                {
                    giveName = baseName + "00" + counter.ToString();
                }
                if (counter > 10 && counter < 100)
                {
                    giveName = baseName + "0" + counter.ToString();
                }
                if (counter > 100)
                {
                    giveName = baseName + counter.ToString();
                }
                child.name = giveName;
                counter++;
            }
            run = false;
            renamedObjects = counter;
            counter = 0;
        }
    }
}

