using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour {
    public int checkpointNumer;
    public bool isFinishLine;
    public GameObject gameManager;

    void OnTriggerEnter (Collider trigger)
    {
        if(trigger.transform.tag == "PlayerKart")
        {
            if (isFinishLine && trigger.GetComponent<KartController>().nextCheckPoint == checkpointNumer)
            {
                trigger.GetComponent<KartController>().nextCheckPoint = 0;
            }
            else if (trigger.GetComponent<KartController>().nextCheckPoint == checkpointNumer)
            {
                trigger.GetComponent<KartController>().nextCheckPoint++;
            }
            gameManager.GetComponent<KartGameManager>().SaveCheckpoints();          
        }
    }
}
