using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    //Puase
    public bool isPaused;
    public GameObject pauseMenu;

    public OptionsSettings options;

    public GameObject letterboxes;
    public GameObject topLetterbox;
    public GameObject bottomLetterbox;
    public GameObject chatPanel;

    public Text npcNameText;
    public Text cutsceneText;
    public Text interactText;

    public GameObject interactTextObject;
    public GameObject npcNameTextObject;

    public AudioSource _sound;
	public StatsManager stats;
    public QuestManager quests;
    public ProgressionManager prog;

    public Text shurikenAmountText;
    public Text smokeBombAmountText;
    public GameObject shurikenAmountCircle;
    public GameObject smokeBombAmountCircle;

    public GameObject slashHotkeyObject;
    public GameObject shurikenHotkeyObject;
    public GameObject grapplingHookHotkeyObject;
    public GameObject smokeBombHotkeyObject;
    public GameObject dashHotkeyObject;

    public GameObject slashIcon;
    public GameObject shurikenIcon;
    public GameObject grapplingHookIcon;
    public GameObject smokeBombIcon;
    public GameObject dashIcon;

    public Animator[] refreshIcons;
    public bool[] refreshed;
    

    public GameObject[] lockedIcons;

    public Text mainQuestTitleText;
    public Text[] mainQuestTasksText;
    public RectTransform questArrow;

    public Text sideQuestText;

    public AudioClip questCompleted;
    public AudioClip unlockAbilitySound;
    public AudioClip pickUpSound;
    public AudioClip buttonHover;
    public AudioClip openMenu;
    public AudioClip closeMenu;
    public AudioClip scrollSound;

    public GameObject pickUpText;
    public GameObject unlockAbility;

    public Image[] activeIcons;
    public GameObject player;

    float katanaCD;
    float shurikenCD;
    float hookCD;
    float bombCD;
    float dashCD;

    float storeSFXVolume;


	
	void Awake()
	{
		stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        quests = GameObject.Find("GameManager").GetComponent<QuestManager>();
        prog = GameObject.Find("GameManager").GetComponent<ProgressionManager>();
        _sound = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        options = GameObject.Find("GameManager").GetComponent<OptionsSettings>();

    
	}

    void Start()
    {
        UnlockIcons();
        SetQuestsText();
    }

    void Update()
    {
        CountConsumeables();
        SetSkillIcon();
        CheckPause();
    }

    void CheckPause()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(!pauseMenu.activeSelf)
            {
                Camera.main.GetComponent<AudioSource>().Pause();
                storeSFXVolume = options.SFXVolume;
                options.SFXVolume = 0;
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                _sound.PlayOneShot(openMenu);
            }
            
        }
    }

    public void UnPause()
    {
        options.SFXVolume = storeSFXVolume;
        Camera.main.GetComponent<AudioSource>().Play();
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        _sound.PlayOneShot(closeMenu);
    }

    void SetSkillIcon()
    {
        //Katana
        if(activeIcons[0].fillAmount < 1f)
        { 
            activeIcons[0].fillAmount += 1f / 1.5f * Time.deltaTime;

            if(katanaCD < 1.5f)
            {
                katanaCD += Time.deltaTime;
            }
            else
            {
                activeIcons[0].fillAmount = 1;
                
            }
        }
        else if(!refreshed[0])
        {
            refreshIcons[0].SetTrigger("Refresh");
            refreshed[0] = true;
        }


        //Shuriken
        if (activeIcons[1].fillAmount < 1f && stats.shurikenAmount > 0)
        {
            activeIcons[1].fillAmount = shurikenCD;
            if(shurikenCD < 1)
            {
                shurikenCD += Time.deltaTime;
            }
            else
            {
                activeIcons[1].fillAmount = 1;
            }
        }
        else if(stats.shurikenAmount == 0)
        {
            activeIcons[1].fillAmount = 0;
        }
        else if (!refreshed[1])
        {
            refreshIcons[1].SetTrigger("Refresh");
            refreshed[1] = true;
        }

        //GrapplingHook
        if (activeIcons[2].fillAmount < 1f && player.GetComponent<GrapplingHook>().canHook)
        {
            Debug.Log("recharge");
            activeIcons[2].fillAmount = (hookCD / 3);
            
        }
        else
        {
            if (!refreshed[2] && player.GetComponent<GrapplingHook>().canHook)
            {
                refreshIcons[2].SetTrigger("Refresh");
                refreshed[2] = true;
            }
        }
        

        if (hookCD < 3)
        {
            hookCD += Time.deltaTime;
        }
        else
        {
            activeIcons[2].fillAmount = 1;
        }
        

        if (!player.GetComponent<GrapplingHook>().canHook)
        {
            activeIcons[2].fillAmount = 0;
            refreshed[2] = false;
        }
        

        //SmokeBomb
        if (activeIcons[3].fillAmount < 1f && stats.smokeBombAmount > 0)
        {
            activeIcons[3].fillAmount = bombCD / 5;
            if (bombCD < 5)
            {
                bombCD += Time.deltaTime;
            }
            else
            {
                activeIcons[3].fillAmount = 1;
            }
        }
        else if (stats.smokeBombAmount == 0)
        {
            activeIcons[3].fillAmount = 0;
        }
        else if (!refreshed[3])
        {
            refreshIcons[3].SetTrigger("Refresh");
            refreshed[3] = true;
        }

        //Dash
        if (activeIcons[4].fillAmount < 1f)
        {
            activeIcons[4].fillAmount += 1f / 2f * Time.deltaTime;
        }
        else if (!refreshed[4])
        {
            refreshIcons[4].SetTrigger("Refresh");
            refreshed[4] = true;
        }

    }


	
    public void SetQuestsText()
    {
        mainQuestTitleText.text = quests.mainQuests[prog.mainQuestProgression].questTitle;
        for(int i = 0; i < mainQuestTasksText.Length; i++)
        {
            if (i <= quests.mainQuests[prog.mainQuestProgression].atTask)
            {
                mainQuestTasksText[i].text = quests.mainQuests[prog.mainQuestProgression].questTasks[i];
                if(i > 0)
                {
                    mainQuestTasksText[i - 1].text = StrikeThrough(quests.mainQuests[prog.mainQuestProgression].questTasks[i - 1]);
                }
                

                questArrow.anchoredPosition = new Vector2(questArrow.anchoredPosition.x, 56 - (56 * quests.mainQuests[prog.mainQuestProgression].atTask));
                
            }
            else
            {
                mainQuestTasksText[i].text = "";
            }
        }
        if(quests.mainQuests[prog.mainQuestProgression].atTask > 0)
        {
            _sound.PlayOneShot(questCompleted);
        }
    }

    public string StrikeThrough(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }


    //Keeps count of shurikens on screen
    void CountConsumeables()
	{
        if(shurikenAmountCircle.activeSelf)
        {
            shurikenAmountText.text = stats.shurikenAmount.ToString();
        }
        
        if(smokeBombAmountCircle.activeSelf)
        {
            smokeBombAmountText.text = stats.smokeBombAmount.ToString();
        }
        
		
	}

    //Play PickUp Animation
    public void PickUp(string text)
    {
        pickUpText.GetComponent<Text>().text = "You Picked Up a " + text + "!";
        pickUpText.GetComponent<Animator>().SetTrigger("PickUp");
        _sound.PlayOneShot(pickUpSound);
    }

    //Play ability Unlocked Animation
    public void UnlockAbility()
    {
        unlockAbility.GetComponent<Animator>().SetTrigger("Unlock");
        _sound.PlayOneShot(unlockAbilitySound, 0.7f);
    }


    //Play the letterbox animation
    public void EnterCutscene()
    {
        topLetterbox.GetComponent<Animator>().SetBool("SlideIn", true);
        bottomLetterbox.GetComponent<Animator>().SetBool("SlideIn", true);
    }

    //Play the letterbox animation
    public void ExitCutscene()
    {
        topLetterbox.GetComponent<Animator>().SetBool("SlideIn", false);
        bottomLetterbox.GetComponent<Animator>().SetBool("SlideIn", false);
    }

    //Set the text in the chatpanel
    public void UpdateText(string name, string text)
    {
        if(name != "")
        {
            npcNameTextObject.GetComponent<Animator>().SetBool("FadeIn", true);
        }
        npcNameText.text = name;
        cutsceneText.text = text;
    }

    //Change the text that shows how to interact
    public void ChangeInteractText(InteractScript interactObject)
    {
        if(!Camera.main.GetComponent<CameraController>().inCutscene)
        {
            interactTextObject.GetComponent<Animator>().SetBool("FadeIn", true);
            interactText.text = "Press Z to " + interactObject.interactText;
        }
        else
        {
            interactText.text = "";
        }
    }

    //Hide the interact text
    public void RemoveInteractText()
    {
        //interactText.text = "";
        interactTextObject.GetComponent<Animator>().SetBool("FadeIn", false);
    }

    public void UnlockIcons()
    {
        if (stats.katanaUnlocked)
        {
            
        }
        if (stats.shurikenUnlocked)
        {
            lockedIcons[0].SetActive(false);
            shurikenAmountCircle.SetActive(true);
            shurikenHotkeyObject.SetActive(true);
        }
        if (stats.grapplingHookUnlocked)
        {
            lockedIcons[1].SetActive(false);
            grapplingHookHotkeyObject.SetActive(true);
        }
        if (stats.smokeBombUnlocked)
        {
            smokeBombAmountCircle.SetActive(true);
            lockedIcons[2].SetActive(false);
            smokeBombHotkeyObject.SetActive(true);
        }
    }

    public void UseSkill(int skill)
    {
        activeIcons[skill].fillAmount = 0;
        if(skill == 0)
        {
            refreshed[0] = false;
            katanaCD = 0f;
        }
        else if(skill == 1)
        {
            refreshed[1] = false;
            shurikenCD = 0f;
        }
        else if(skill ==2)
        {
            refreshed[2] = false;
            hookCD = 0f;
        }
        else if (skill == 3)
        {
            refreshed[3] = false;
            bombCD = 0f;
        }
        else if (skill == 4)
        {
            refreshed[4] = false;
            dashCD = 0f;
        }
    }

}


