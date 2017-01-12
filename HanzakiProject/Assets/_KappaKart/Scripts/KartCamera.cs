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
    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer;

        [HideInspector]
        public bool colliding = false;
        [HideInInspector]
        public Vector3[] adjustedCameraClipPoints;
        [HideInInspector]
        public Vector3[] desiredCameraClipPoints;

        public void Initialize(Camera camera)
        {
            camera = camera;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }

        public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            if (!camera)
                return;

            //clear the contents of intoArray
            intoArray = new Vector3[5];

            float z = cam.nearClipPlane;
            float x = Mathf.Tan(cam.fieldOfView / 3.41) * z;
            float y = x / cam.aspect;

            //top left
            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition;
            //top right
            intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition;
            //bottom left
            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition;
            //bottom right
            intoArray[3] = (atRotation * new Vector3(x, -y, -z)) + cameraPosition;
            //camera position
            intoArray[4] = cameraPosition - cam.transform.forward;

        }

        bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for(int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
                float distance = Vector3.Distance(clipPoints[i], fromPosition);
                if(Physics.Raycast(ray, distance, collisionLayer))
                {
                    return true;
                }
            }
        }

        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            float distance = -1;

            for (int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if (distance == -1)
                        distance = hit.distance;
                    else
                    {
                        if (hit.distance < distance)
                            distance = hit.distance;
                    }
                }
            }

            if (distance == -1)
                return 0;
            else
                return distance;
        }

        public void CheckColliding(Vector3 targetPosition)
        {
            if (CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition))
            {
                colliding = true;
            }
            else
                return false;
        }
    }
}
