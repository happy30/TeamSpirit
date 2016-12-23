using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPortChanger : MonoBehaviour
{

    public float rotationChange;
    public enum Type
    {
        ToTD,
        ToSS
    };

    public Type type;

	void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(type == Type.ToSS)
            {
                col.GetComponent<PlayerController>().levelType = PlayerController.LevelType.SS;
                col.GetComponent<PlayerController>().rotationOffset = rotationChange;
            }
            else
            {
                col.GetComponent<PlayerController>().levelType = PlayerController.LevelType.TD;
                col.GetComponent<PlayerController>().rotationOffset = 0;
            }
            col.GetComponent<PlayerController>().ChangeControlsDependingOnLevelType();
            Camera.main.GetComponent<CameraController>().SetUp();
        }
    }
}
