using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BlackOverlayFadeOut : MonoBehaviour
{
    public bool auto;
    Image overlay;
    float alpha;

    void Awake()
    {
        overlay = GetComponent<Image>();
        alpha = 1;
    }

    void Update ()
    {
		if(auto && overlay.color.a > 0)
        {
            alpha -= Time.deltaTime;
            overlay.color = new Color(0, 0, 0, alpha);
        }
	}
}
