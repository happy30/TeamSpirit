//Made by Arne
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartCamera : MonoBehaviour {

	public Transform camTransform;
	public Transform kartNumber;
	
	Camera cam;
	
	public float distance;
	float currentX = 0.0f;
	float currentY = 0.0f;
	public float height;
	
	float sensitivityX = 4.0f;
	float sensitivityY = 1.0f;
	public float followSpeed;
	
	
	// Use this for initialization
	private void Start () 
	{
		camTransform = transform;
		cam = Camera.main;
	}
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(Input.GetKey(InputManager.Hook))
        {
			LookBehindKart();
		}
        if (!Input.GetKey(InputManager.Hook))
        {
            FollowKart();
        }
	}
	public void FollowKart () 
	{
		Vector3 lookPos = new Vector3(
			kartNumber.position.x + (kartNumber.forward.x * -distance),
			kartNumber.position.y + height,
			kartNumber.position.z + (kartNumber.forward.z * -distance));
		
		transform.position = Vector3.Slerp(transform.position, lookPos, followSpeed * Time.deltaTime);
		
		
		transform.eulerAngles = new Vector3(
			Mathf.LerpAngle(transform.eulerAngles.x, kartNumber.eulerAngles.x + 20, followSpeed * Time.deltaTime),
			Mathf.LerpAngle(transform.eulerAngles.y, kartNumber.eulerAngles.y, followSpeed * Time.deltaTime), 
			Mathf.LerpAngle(transform.eulerAngles.z, kartNumber.eulerAngles.z, followSpeed * Time.deltaTime));
	}	
	public void LookBehindKart () 
	{
        Vector3 lookPos = new Vector3(
            kartNumber.position.x + (kartNumber.forward.x * +distance),
            kartNumber.position.y + height,
            kartNumber.position.z + (kartNumber.forward.z * +distance));

        transform.position = Vector3.Lerp(transform.position, lookPos, followSpeed * Time.deltaTime);


        transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(transform.eulerAngles.x, kartNumber.eulerAngles.x + 20, followSpeed * Time.deltaTime),
            Mathf.LerpAngle(transform.eulerAngles.y, kartNumber.eulerAngles.y + 180, followSpeed * Time.deltaTime),
            Mathf.LerpAngle(transform.eulerAngles.z, kartNumber.eulerAngles.z, followSpeed * Time.deltaTime));
    }
}
