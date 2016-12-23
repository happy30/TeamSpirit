using UnityEngine;
using System.Collections;

public class MainMenuCameraScript : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;


	// Use this for initialization
	void Start ()
    {
        pos1 = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(pos1.position, pos2.position, 0.1f * Time.deltaTime);
        //transform.eulerAngles = Vector3.Lerp(pos1.eulerAngles, pos2.eulerAngles, 3 * Time.deltaTime);
	}
}
