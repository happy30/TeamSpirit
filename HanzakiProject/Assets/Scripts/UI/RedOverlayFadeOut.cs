using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedOverlayFadeOut : MonoBehaviour {

    public float alpha;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(alpha > 0)
        {
            alpha -= Time.deltaTime;
        }

        GetComponent<Image>().color = new Color(1, 1, 1, alpha);
	}

    public void Activate()
    {
        alpha = 1;
    }
}
