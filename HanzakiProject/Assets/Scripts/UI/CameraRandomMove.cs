using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRandomMove : MonoBehaviour
{

    Vector3 destPos;
    Vector3 startPos;
    float moveSpeed;

	// Use this for initialization
	void Start ()
    {
        destPos = transform.position;
        startPos = transform.position;
        moveSpeed = 0.2f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Vector3.Distance(transform.position, destPos) < 0.2f)
        {
            destPos = new Vector3(
                Random.Range(-0.3f, 0.3f),
                Random.Range(-0.3f, 0.3f),
                Random.Range(-0.3f, 0.3f))
                
                +
                
                startPos;
        }


        transform.position = Vector3.Slerp(transform.position, destPos, moveSpeed * Time.deltaTime);	
	}
}
