using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateIntro : MonoBehaviour {

    public Image overlay;
    public CutsceneController controller;
    public float alpha;

	// Use this for initialization
	void Start ()
    {
        GameObject.Find("Canvas").GetComponent<UIManager>().EnterCutscene();
        controller = GetComponent<CutsceneController>();
        GetComponent<CutsceneController>().Activate();
        alpha = 1;
        overlay.color = new Color(0, 0, 0, 1);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(controller.currentText > 3)
        {
            if(overlay.color.a > 0)
            {
                alpha -= Time.deltaTime * 0.3f;
            }
        }

        overlay.color = new Color(0, 0, 0, alpha);

        if(controller.currentText > 6)
        {
            GameObject.Find("Canvas").GetComponent<LoadController>().LoadScene("Level1");
            controller.currentText = 0;
        }

    }
}
