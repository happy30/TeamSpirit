using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMover : MonoBehaviour {


    public float moveX;
    public float moveY;
    public float moveZ;

	void Update () {
        transform.Translate(moveX*Time.deltaTime, moveY*Time.deltaTime, moveZ*Time.deltaTime);
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<PlayerController>().Die();
        }
    }
}
