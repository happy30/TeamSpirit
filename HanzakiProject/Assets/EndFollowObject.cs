using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndFollowObject : MonoBehaviour {

    public Transform oldMan;
    public GameObject script;

    public float alpha;
    public Image overlay;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(GetComponent<Activate>().activated && Vector3.Distance(transform.position, oldMan.position) > 3)
        {
            transform.LookAt(oldMan);
            transform.Translate(transform.forward * Time.deltaTime);

        }
        if(script == null)
        {
            if(overlay.color.a < 1)
            {
                alpha += Time.deltaTime;
                overlay.color = new Color(0, 0, 0, alpha);
            }
            else
            {
                SceneManager.LoadScene("Credits");
            }
        }


	}
}
