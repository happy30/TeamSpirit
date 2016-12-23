//MainMenuScroll by Jordi

using UnityEngine;
using System.Collections;

public class MainMenuScroll : MonoBehaviour {

    Rigidbody _rb;
    public float fallSpeed;

    public MainMenuController mainMenuController;
    public bool hasPlayedSFX;
    public AudioSource sound;
    public AudioClip scrollSound;
    public float cutOffValue;

    public float timer;

    public GameObject scrollObject;
    public GameObject scroll;

    public GameObject optionsScrollObject;
    public GameObject optionsScroll;

    public bool optionsBack;


    // Use this for initialization
    void Awake ()
    {
        sound = GameObject.Find("MainMenuUISounds").GetComponent<AudioSource>();
        mainMenuController = GameObject.Find("MainMenuCanvas").GetComponent<MainMenuController>();
        cutOffValue = 1;
	}

    // Update is called once per frame
    void Update()
    {
        if (mainMenuController.scrollActivated)
        {
            scrollObject.GetComponent<Animator>().SetBool("StartScrolling", true);
            scroll.GetComponent<Cloth>().externalAcceleration = new Vector3(1f, 0, 1f);
            Invoke("ResetAcceleration", 1.5f);
        }
    }

    public void ActvateOptionsScroll()
    {
        optionsScrollObject.GetComponent<Animator>().SetBool("StartScrolling", true);

        Invoke("OptionsResetAcceleration", 1.5f);
    }

    public void CloseOptionsScroll()
    {
        optionsScrollObject.GetComponent<Animator>().SetBool("StartScrolling", false);

        Invoke("OptionsResetAcceleration", 1.5f);
    }


    public void ResetAcceleration()
    {
        scroll.GetComponent<Cloth>().externalAcceleration = new Vector3(0.4f, 0, 0.4f);
        if (!hasPlayedSFX)
        {
            sound.PlayOneShot(scrollSound);
            hasPlayedSFX = true;
        }
    }

    public void OptionsResetAcceleration()
    {
        scroll.GetComponent<Cloth>().externalAcceleration = new Vector3(0.4f, 0, 0.4f);
        if (!hasPlayedSFX)
        {
            sound.PlayOneShot(scrollSound);
            hasPlayedSFX = true;
        }
    }
    public void OptionsBack()
    {
        scroll.GetComponent<Cloth>().externalAcceleration = new Vector3(0.4f, 0, 0.4f);
        if (!hasPlayedSFX)
        {
            sound.PlayOneShot(scrollSound);
            hasPlayedSFX = true;
            optionsBack = false;
        }
    }
}
