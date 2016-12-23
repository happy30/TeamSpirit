//PauseManager by Arne & Jordi
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
	public enum CursorAt
    {
        Continue,
        MainMenu,
        ExitGame
    };
    public CursorAt cursorAt;

    public GameObject cursor;
    public Vector3 cursorPos;
    public float cursorSpeed;
    public UIManager ui;
    public bool vAxisInUse;
    public bool optionsOpen;
    public GameObject options;
    public RectTransform cursorArrow;

    public GameObject scrollObject;
    public GameObject scroll;

    public GameObject optionsScrollObject;
    public GameObject optionsScroll;

    public LoadController load;


    public Text[] itemsInList;

    void Awake()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        load = GameObject.Find("Canvas").GetComponent<LoadController>();
    }

    void Start()
    {
        cursorAt = CursorAt.Continue;
        Time.timeScale = 0;
    }

    void Update()
    {
        cursor.GetComponent<RectTransform>().anchoredPosition = cursorPos;
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (!vAxisInUse && Input.GetAxisRaw("Vertical") < 0)
            {
                vAxisInUse = true;
                if((int)cursorAt < 2)
                {
                    ui._sound.PlayOneShot(ui.buttonHover);
                    cursorAt++;
                }
            }
            else if (!vAxisInUse && Input.GetAxisRaw("Vertical") > 0)
            {
                vAxisInUse = true;
                if ((int)cursorAt > 0)
                {
                    ui._sound.PlayOneShot(ui.buttonHover);
                    cursorAt--;
                }
            }
        }
        else
        {
            vAxisInUse = false;
        }

        if(Input.GetButtonDown("Cancel"))
        {
            Continue();
        }


        if(Input.GetKeyDown(InputManager.Slash) || Input.GetKeyDown(InputManager.JSlash))
        {
            if(cursorAt == CursorAt.Continue)
            {
                Continue();
            }
            else if(cursorAt == CursorAt.MainMenu)
            {
                load.LoadScene("MainMenu2");
            }
            else if (cursorAt == CursorAt.ExitGame)
            {
                Application.Quit();
            }
        }


        if(cursorAt == CursorAt.Continue)
        {
            itemsInList[0].color = new Color(233f / 255f, 1, 131f / 255f);
            itemsInList[2].color = Color.white;
            itemsInList[1].color = Color.white;
            cursorPos = new Vector3(0, 140, 0);
        }
        if (cursorAt == CursorAt.MainMenu)
        {
            itemsInList[1].color = new Color(233f / 255f, 1, 131f / 255f);
            itemsInList[0].color = Color.white;
            itemsInList[2].color = Color.white;
            cursorPos = new Vector3(0, 60, 0);
        }
        if (cursorAt == CursorAt.ExitGame)
        {
            itemsInList[2].color = new Color(233f / 255f, 1, 131f / 255f);
            itemsInList[1].color = Color.white;
            itemsInList[0].color = Color.white;
            cursorPos = new Vector3(0, -20, 0);
        }
    }

    public void Continue()
    {
        cursorAt = CursorAt.Continue;
        ui.UnPause();
    }
}