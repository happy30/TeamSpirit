using UnityEngine;
using System.Collections;

public class ParallelaxScript : MonoBehaviour {

    public Vector3 startPosition;
    public Vector3 desiredPosition;
    public float distance;
    public float yOffset;

	// Use this for initialization
	void Start ()
    {
        startPosition = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!Camera.main.GetComponent<CameraController>().inCutscene)
        {
            desiredPosition = (Camera.main.transform.position - startPosition) * -distance;
            transform.position = Vector3.Lerp(transform.position, new Vector3(desiredPosition.x, desiredPosition.y + yOffset, transform.position.z), 1f);
        }
        
	}
}
