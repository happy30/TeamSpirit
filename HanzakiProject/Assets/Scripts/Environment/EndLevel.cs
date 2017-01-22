﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{

    public string levelToLoad;
    public GameObject end;

	void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            end.SetActive(true);
            Invoke("LoadNextScene", 3f);
        }
    }

    void LoadNextScene()
    {
        GameObject.Find("GameManager").GetComponent<QuestManager>().CompleteMainQuest();
        GameObject.Find("Canvas").GetComponent<LoadController>().LoadScene(levelToLoad);
    }
}