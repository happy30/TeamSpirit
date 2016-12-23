//Made by Sascha Greve

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelPortal : MonoBehaviour {

    LoadController loadController;
    public string nextLevel;

    void Awake()
    {
        loadController = GameObject.Find("GameManager").GetComponent<LoadController>();
    }

	void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            loadController.LoadScene(nextLevel);
        }
	}
}
