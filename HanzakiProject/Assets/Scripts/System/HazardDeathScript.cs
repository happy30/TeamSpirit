using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDeathScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player")
        {
            //col.GetComponent<PlayerController>().Die();
        }
	}
}
