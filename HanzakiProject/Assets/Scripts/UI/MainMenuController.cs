//Main menu by Arne & Jordi

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //Scroll texture
    public Texture2D continueUnlockedTexture;
    public Texture2D continueLockedTexture;
    public Texture2D continueUnlockedTextureAlpha2;
    public Texture2D continueLockedTextureAlpha2;
    public bool continueUnlocked;
    public Material scrollMaterial;

    public MainMenuScroll scrollScript;
    public GameObject scroll;

    public RectTransform optionsPanel;
    public float optionsSlideTime;
    public float optionsLocationX;
    public float scrollLocationX;
    public bool optionsOpen;

    public GameObject blackScreen;

    //For the arrow
    public RectTransform[] buttons;
    public RectTransform cursorArrow;
    public float arrowSpeed;
    public float yPosArrow;

    public bool scrollActivated;
    public bool optionsScrollActivated;

    public bool fadeScreen;
    public float fadeValue;

    public float cutoutValue;

    public GameObject options;
    public LoadController _load;
    public bool vAxisInUse;

    //Any key
    public GameObject pressAnyKeyObject;

    //sounds
    public AudioSource sound;
    public AudioClip buttonHover;
    public AudioClip openMenu;



    public enum CursorAt
    {
        NewGame,
        Continue,
        Options,
        Exit
    };

    public CursorAt cursorAt;

    void Awake()
    {
        sound = GameObject.Find("MainMenuUISounds").GetComponent<AudioSource>();
        _load = GetComponent<LoadController>();
        scrollScript = GetComponent<MainMenuScroll>();
    }
	// Use this for initialization
	void Start ()
    {
        Time.timeScale = 1;
        cursorArrow.anchoredPosition = new Vector2(cursorArrow.anchoredPosition.x, buttons[0].anchoredPosition.y);
        cursorArrow.gameObject.SetActive(false);
        optionsLocationX = 1920;
        scrollLocationX = scroll.transform.position.x;

        if (continueUnlocked)
        {
            scrollMaterial.mainTexture = continueUnlockedTexture;
        }
        else
        {
            scrollMaterial.mainTexture = continueLockedTexture;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(cursorArrow.gameObject.activeSelf)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                if (!vAxisInUse && Input.GetAxisRaw("Vertical") < 0)
                {
                    vAxisInUse = true;
                    SetCursorPosition((int)cursorAt + 1, true);
                }
                else if (!vAxisInUse && Input.GetAxisRaw("Vertical") > 0)
                {
                    vAxisInUse = true;
                    SetCursorPosition((int)cursorAt - 1, false);
                }
            }

            if(Input.GetAxisRaw("Vertical") == 0)
            {
                vAxisInUse = false;
            }


            if (cursorAt == CursorAt.NewGame)
            {
                yPosArrow = buttons[0].anchoredPosition.y;
            }
            else if (cursorAt == CursorAt.Continue)
            {
                yPosArrow = buttons[1].anchoredPosition.y;
            }
            else if (cursorAt == CursorAt.Options)
            {
                yPosArrow = buttons[2].anchoredPosition.y;
            }
            else if (cursorAt == CursorAt.Exit)
            {
                yPosArrow = buttons[3].anchoredPosition.y;
            }

            cursorArrow.anchoredPosition = Vector2.Lerp(cursorArrow.anchoredPosition, new Vector2(cursorArrow.anchoredPosition.x, yPosArrow), arrowSpeed * Time.deltaTime);
        }
        else
        {
            if(Input.anyKey && !scrollActivated)
            {
                sound.PlayOneShot(openMenu);
                pressAnyKeyObject.SetActive(false);
                scrollActivated = true;
                Invoke("ArrowCursorAppear", 2);
            }
        } 

        if(Input.GetKeyDown(InputManager.Slash) && cursorArrow.gameObject.activeSelf || Input.GetKeyDown(InputManager.JSlash) && cursorArrow.gameObject.activeSelf)
        {
            if(cursorAt == CursorAt.NewGame)
            {
                NewGame();
                Invoke("InitiateFade", 1f);
                Invoke("LoadNextScene", 2.5f);
                cursorArrow.gameObject.SetActive(false);
            }
            else if(cursorAt == CursorAt.Options)
            {
                cursorArrow.gameObject.SetActive(false);
                //optionsLocationX = 0;
                //scrollLocationX = scrollLocationX - 60f;
                GetComponent<MainMenuScroll>().ActvateOptionsScroll();
                optionsOpen = true;
                Invoke("OpenOptions", 1f);
            }
        }

        if(fadeScreen)
        {
            fadeValue += 0.5f * Time.deltaTime;
            Camera.main.fieldOfView -= fadeValue;
            blackScreen.GetComponent<Image>().color = new Color(blackScreen.GetComponent<Image>().color.r, blackScreen.GetComponent<Image>().color.g, blackScreen.GetComponent<Image>().color.b, fadeValue / 0.8f);
        }

        

        //scroll.transform.position = Vector3.Lerp(scroll.transform.position, new Vector3(scrollLocationX, scroll.transform.position.y, scroll.transform.position.z), optionsSlideTime * Time.deltaTime);
    }

    public void SetCursorPosition(int pos, bool goingDown)
    {
        Debug.Log((int)cursorAt);
        if(pos == 0)
        {
            cursorAt = CursorAt.NewGame;
            sound.PlayOneShot(buttonHover);
        }
        if(pos == 1)
        {
            if(goingDown)
            {
                if(continueUnlocked)
                {
                    cursorAt = CursorAt.Continue;
                    sound.PlayOneShot(buttonHover);
                }
                else
                {
                    cursorAt = CursorAt.Options;
                    sound.PlayOneShot(buttonHover);
                }
            }
            else
            {
                if (continueUnlocked)
                {
                    cursorAt = CursorAt.Continue;
                    sound.PlayOneShot(buttonHover);
                }
                else
                {
                    cursorAt = CursorAt.NewGame;
                    sound.PlayOneShot(buttonHover);
                }
            }
        }
        if (pos == 2)
        {
            cursorAt = CursorAt.Options;
            sound.PlayOneShot(buttonHover);
        }
        if(pos == 3)
        {
            cursorAt = CursorAt.Exit;
            sound.PlayOneShot(buttonHover);
        }
        Debug.Log((int)cursorAt);
    }

    public void ArrowCursorAppear()
    {
        cursorArrow.gameObject.SetActive(true);
    }

    public void NewGame()
    {
        if (continueUnlocked)
        {
            
        }
        else
        {
            scrollScript.scroll.GetComponent<Renderer>().material.mainTexture = continueLockedTextureAlpha2;
            scrollScript.scrollObject.GetComponent<Animator>().SetBool("BurnScroll", true);
        }
    }

    public void OptionsBack()
    {
        optionsOpen = false;
        GetComponent<MainMenuScroll>().CloseOptionsScroll();
        Invoke("ArrowCursorAppear", 1.2f);
    }

    public void OpenOptions()
    {
        options.SetActive(true);
        options.GetComponent<OptionsManager>().SetKeybindingsText();
    }

    public void InitiateFade()
    {
        fadeScreen = true;
    }

    public void LoadNextScene()
    {
        _load.LoadScene("Level1");
    }


}
