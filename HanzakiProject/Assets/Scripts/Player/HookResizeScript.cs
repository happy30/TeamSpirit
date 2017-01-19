using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookResizeScript : MonoBehaviour {

    public Transform camTrans;
    public Vector3 lookPos;


	// Use this for initialization
	void Start ()
    {
        camTrans = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(camTrans);
	}
}
