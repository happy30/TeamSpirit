using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppingStones : MonoBehaviour {

    public float sinkSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider intel)
    {
        transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(transform.eulerAngles.x, this.GameObject.eulerAngles.x, * Time.deltaTime),
            Mathf.LerpAngle(transform.eulerAngles.y, this.GameObject.eulerAngles.y, followSpeed * Time.deltaTime),
            Mathf.LerpAngle(transform.eulerAngles.z, this.GameObject.eulerAngles.z, sinkSpeed * Time.deltaTime));
    }
}
