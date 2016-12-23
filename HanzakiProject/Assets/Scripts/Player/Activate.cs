//Activate by Jordi

using UnityEngine;
using System.Collections;

public class Activate : MonoBehaviour
{
    public bool activated;

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
        Camera.main.gameObject.GetComponent<CameraController>().inCutscene = false;
        activated = false;
    }
}

