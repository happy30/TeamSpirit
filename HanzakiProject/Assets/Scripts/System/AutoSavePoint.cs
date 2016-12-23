using UnityEngine;
using System.Collections;

public class AutoSavePoint : MonoBehaviour
{
    public GameObject gameManager;

    void OnTriggerEnter(Collider trigger)
    {
        if(trigger.tag == "Player")
        {
            gameManager.GetComponent<DataManager>().SaveData();
        }
    }
}
