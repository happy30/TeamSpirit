//LocationEnter by Arne
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocationEnter : MonoBehaviour {

	public GameObject levelName;
	public string currentLVL;
	
	// Use this for initialization
	void Awake () 
	{
		levelName.GetComponent<Text>().text = currentLVL;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
