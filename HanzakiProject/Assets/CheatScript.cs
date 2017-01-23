using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is ca lled once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("Player").transform.position = transform.position;
        }
	}
}
