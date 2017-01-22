//Activate by Jordi

using UnityEngine;
using System.Collections;

public class Activate : MonoBehaviour
{
    public bool activated;
    public bool intro;

    //Focus the camera on the object
    public void FocusCamera()
    {
        Camera.main.gameObject.GetComponent<CameraController>().followObject = gameObject;
        Camera.main.gameObject.GetComponent<CameraController>().inCutscene = true;
        activated = true;
    }

    //Focus the camera back to the player
    public void DefocusCamera()
    {
        if(!intro)
        {
            Camera.main.gameObject.GetComponent<CameraController>().inCutscene = false;
            activated = false;
        }
        
    }
}

