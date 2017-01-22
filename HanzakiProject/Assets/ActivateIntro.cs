using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateIntro : MonoBehaviour {

    public Image overlay;
    public CutsceneController controller;
    public float alpha;

    public int fadeOutMessageCounter;

	// Use this for initialization
	void Start ()
    {
        GameObject.Find("Canvas").GetComponent<UIManager>().EnterCutscene();
        controller.Activate();
        alpha = 1;
        overlay.color = new Color(0, 0, 0, 1);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (controller != null)
        {
            if (controller.currentText > fadeOutMessageCounter)
            {
                if (overlay.color.a > 0)
                {
                    alpha -= Time.deltaTime * 0.3f;
                }
            }

            overlay.color = new Color(0, 0, 0, alpha);

            if (controller.currentText > 13)
            {
                GameObject.Find("Canvas").GetComponent<LoadController>().LoadScene("Level1");
                controller.currentText = 0;
            }
        }
        else
        {
                if (overlay.color.a > 0)
                {
                    alpha -= Time.deltaTime * 0.3f;
                overlay.color = new Color(0, 0, 0, alpha);
            }
        }


    }
}
