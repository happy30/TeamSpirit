using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour
{
    public float rotateSpeed;
    public bool ignoreTimeScale;

	
	
	// Update is called once per frame
	void Update ()
    {
        if(!ignoreTimeScale)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, rotateSpeed);
        }
        
	}
}
