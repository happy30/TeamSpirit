//Cutscene Controller by Jordi

using UnityEngine;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    public enum CutsceneType
    {
        MainQuest,
        SideQuest,
        OneTimeCutscene
    };

    public CutsceneType cutsceneType;

    public QuestManager questManager;
    public UIManager ui;
    InteractScript _interact;
    public ProgressionManager progressionManager;


    public string npcName;
    public string[] CutsceneText;
    public string[] secondCutsceneText;

    //technical stuff
    public string[] fullLines;
    string fullDialogueLine;
    public string displayLine;
    public bool activated;

    int currentText;
    int currentChar;

    float scrollSpeed;

    void Awake()
    {
        _interact = GetComponent<InteractScript>();
        questManager = GameObject.Find("GameManager").GetComponent<QuestManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        progressionManager = GameObject.Find("GameManager").GetComponent<ProgressionManager>();
    }

    void Start()
    {
        scrollSpeed = 0.05f;
    }
    

    void Update()
    {
        if(activated)
        {
            if (displayLine != fullDialogueLine)
            {
                if (!IsInvoking("NextChar"))
                {
                    Invoke("NextChar", scrollSpeed);
                    if(scrollSpeed == 0.02f)
                    {
                        Invoke("NextChar", scrollSpeed);
                        Invoke("NextChar", scrollSpeed);
                    }
                }
            }
            //Each character will appear on screen one by one, if we click we speed up that process. If all characters are on-screen go to next line
            if (Input.GetKeyDown(InputManager.Slash) || Input.GetKeyDown(InputManager.JJumpTD))
            {
                if (displayLine != fullDialogueLine)
                {
                    scrollSpeed = 0.02f;
                }
                else
                {
                    scrollSpeed = 0.05f;
                    SetNPCNameAndText();
                }
            }
            ui.UpdateText(npcName, displayLine);
        }
        
    }

    //Initialize the cutscene
    public void Activate()
    {
        StartCoroutine(DelayBeforeText());
    }

    //End the chat
    public void DeActivate()
    {
        ui.npcNameTextObject.GetComponent<Animator>().SetBool("FadeIn", false);
        ui.npcNameText.color = new Color(ui.npcNameText.color.r, ui.npcNameText.color.g, ui.npcNameText.color.b, 0);
        currentText = 0;
        if(_interact.interactType == InteractScript.InteractType.OnTrigger)
        {
            
            if(cutsceneType == CutsceneType.MainQuest)
            {
                questManager.NextTask();
                ui.SetQuestsText();
            }
            Destroy(_interact.gameObject);
        }
        _interact.DeActivate();
        ui.chatPanel.SetActive(false);
        activated = false;
    }

    //Add a character one by one on screen
    public void NextChar()
    {
        if (currentChar < fullDialogueLine.Length)
        {
            displayLine += fullDialogueLine[currentChar];
            currentChar++;
        }
    }

    public void SetNPCNameAndText()
    {
        displayLine = "";
        currentChar = 0;
        if (currentText > CutsceneText.Length - 1)
        {
            DeActivate();
        }
        fullDialogueLine = CutsceneText[currentText];
        currentText++;
    }

    IEnumerator DelayBeforeText()
    {
        yield return new WaitForSeconds(1f);

        currentText = 0;
        ui.chatPanel.SetActive(true);
        fullDialogueLine = CutsceneText[currentText];
        SetNPCNameAndText();
        activated = true;
    }
}
