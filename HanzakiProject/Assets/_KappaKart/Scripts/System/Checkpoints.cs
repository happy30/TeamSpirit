using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {
    public int checkpointNumber;
    public bool isFinishLine;
    public GameObject gameManager;

    void OnTriggerEnter (Collider trigger)
    {
        if(trigger.transform.tag == "PlayerKart")
        {
            if (isFinishLine && trigger.GetComponent<KartController>().nextCheckPoint == checkpointNumber)
            {
                trigger.GetComponent<KartController>().nextCheckPoint = 0;
                trigger.GetComponent<KartController>().currentLap++;
            }
            else if (trigger.GetComponent<KartController>().nextCheckPoint == checkpointNumber)
            {
                trigger.GetComponent<KartController>().nextCheckPoint++;
            }
            gameManager.GetComponent<KartGameManager>().SaveCheckpoints();          
        }
    }
}
