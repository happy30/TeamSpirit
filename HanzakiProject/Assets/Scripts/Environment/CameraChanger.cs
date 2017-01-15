//CameraChanger by Jordi

using UnityEngine;
using System.Collections;

public class CameraChanger : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(Camera.main.gameObject.GetComponent<CameraController>().inPuzzle)
            {
                Camera.main.gameObject.GetComponent<CameraController>().inPuzzle = false;
                if(col.gameObject.GetComponent<PlayerController>().onSlipperyTile)
                {
                    col.gameObject.GetComponent<PlayerController>().onSlipperyTile = false;
                }
            }
            else
            {
                Camera.main.gameObject.GetComponent<CameraController>().inPuzzle = true;
            }
        }
    }


}
