using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateIntro : MonoBehaviour {

    public Image overlay;
    public CutsceneController controller;
    public float alpha;

    public int fadeOutMessageCounter;

    public bool level1;
    bool nextLevel;
    public bool reached;

    bool fadingIn;

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

            if (controller.currentText == 0 && reached)
            {
                controller.currentText = 0;
                nextLevel = true;
            }
            else if(controller.currentText != 0)
            {
                reached = true;
            }
        }
        else
        {
            if(reached)
            {
                nextLevel = true;
            }
            
            if (overlay.color.a > 0)
            {
                alpha -= Time.deltaTime * 0.3f;
                overlay.color = new Color(0, 0, 0, alpha);
            }
        }
        if(nextLevel && !level1)
        {
            fadingIn = true;
            reached = false;
            nextLevel = false;
            Invoke("GoNextLevel", 2f);

        }

        if(fadingIn)
        {
            if(alpha < 1)
            {
                alpha += Time.deltaTime;
                overlay.color = new Color(0, 0, 0, alpha);
            }
        }

    }

    void GoNextLevel()
    {
        GameObject.Find("Canvas").GetComponent<LoadController>().LoadScene("Level1");
    }
}
