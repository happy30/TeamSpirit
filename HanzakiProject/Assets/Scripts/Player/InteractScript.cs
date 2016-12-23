//InteractScript by Jordi

using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour
{
    public enum InteractType
    {
        OnTrigger,
        OnInput
    };

    public UIManager ui;

    public InteractType interactType;
    public GameObject linkedObject;
    CutsceneController _cutsceneController;

    public bool isHint;
    public float coolDown;
    public bool startCoolDown;
    public string interactText;

    void Awake()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        _cutsceneController = GetComponent<CutsceneController>();
    }

    void Update()
    {
        CoolDown();
    }

    //Add a small delay in which we can't interact with the same thing directly.
    void CoolDown()
    {
        if (startCoolDown)
        {
            coolDown -= Time.deltaTime;
            if (coolDown < 0)
            {
                startCoolDown = false;
                coolDown = 0;
            }
        }
    }

    //Activate the trigger we are standing in
    public void Activate()
    {
        if (!linkedObject.GetComponent<Activate>().activated && coolDown <= 0)
        {
            linkedObject.GetComponent<Activate>().FocusCamera();
            ui.EnterCutscene();
            _cutsceneController.Activate();
        }
        
    }

    //Stop the cutscene
    public void DeActivate()
    {
        linkedObject.GetComponent<Activate>().DefocusCamera();
        ui.ExitCutscene();
        coolDown = 1;
        startCoolDown = true;
    }
}
