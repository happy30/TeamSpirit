using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMover : MonoBehaviour {


    public float moveX;
    public float moveY;
    public float moveZ;

	void Update () {
        transform.Translate(moveX, moveY, moveZ);
	}

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().Die();
        }
    }
}
