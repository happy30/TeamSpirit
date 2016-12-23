using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class OptionsManager : MonoBehaviour
{

    public enum KeyBinding
    {
        Jump,
        Slash,
        Shuriken,
        GrapplingHook,
        SmokeBomb
    };

    public Text jumpKey;
    public Text slashKey;
    public Text shurikenKey;
    public Text grapplingHookKey;
    public Text smokeBombKey;

    public Text jjumpKey;
    public Text jslashKey;
    public Text jshurikenKey;
    public Text jgrapplingHookKey;
    public Text jsmokeBombKey;

    public Text keyToChangeText;

    public KeyBinding keyBinding;
    public GameObject PressAnyKeyPanel;

    public RectTransform[] cursorPositions;
    public GameObject checkmark;
    public bool cursorCantMove;

    public enum CursorPositions
    {
        Hints,
        Music,
        BGM,
        SFX,
        Jump,
        Slash,
        Shuriken,
        GrapplingHook,
        SmokeBomb,
        Back
    };

    public GameObject cursorArrow;
    public CursorPositions cursorPos;
    public float cursorArrowSpeed;
    public MainMenuController mainMenuController;
    CanvasGroup _canvasGroup;
    public float optionsAlpha;
    bool vAxisInUse;
    bool hAxisInUse;

    public OptionsSettings optionSettings;

	// Use this for initialization
	void Awake()
    {
        optionSettings = GameObject.Find("GameManager").GetComponent<OptionsSettings>();
        mainMenuController = GameObject.Find("MainMenuCanvas").GetComponent<MainMenuController>();
        _canvasGroup = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(InputManager.Slash) || Input.GetKeyDown(InputManager.JSlash))
        {
            if(cursorPos == CursorPositions.Jump)
            {
                KeyBindingButton(0);
            }
            else if (cursorPos == CursorPositions.Slash)
            {
                KeyBindingButton(1);
            }
            else if (cursorPos == CursorPositions.Shuriken)
            {
                KeyBindingButton(2);
            }
            else if (cursorPos == CursorPositions.GrapplingHook)
            {
                KeyBindingButton(3);
            }
            else if (cursorPos == CursorPositions.SmokeBomb)
            {
                KeyBindingButton(4);
            }
            else if (cursorPos == CursorPositions.Hints)
            {
                if(optionSettings.displayHints)
                {
                    checkmark.GetComponent<Image>().enabled = false;
                    optionSettings.displayHints = false;
                }
                else
                {
                    checkmark.GetComponent<Image>().enabled = true;
                    optionSettings.displayHints = true;
                }
            }
            


            else if (cursorPos == CursorPositions.Back)
            {
                mainMenuController.optionsOpen = false;
            }
        }


        if(mainMenuController.optionsOpen)
        {
            _canvasGroup.alpha = optionsAlpha;
            if(optionsAlpha < 1)
            {
                optionsAlpha += Time.deltaTime * 3;
            }
            else if(Input.GetButtonDown("Cancel"))
            {
                mainMenuController.optionsOpen = false;
            }
        }
        else
        {
            _canvasGroup.alpha = optionsAlpha;
            if (optionsAlpha > 0)
            {
                optionsAlpha -= Time.deltaTime * 3;
            }
            else
            {
                mainMenuController.OptionsBack();
                gameObject.SetActive(false);
            }
        }
        cursorArrow.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(cursorArrow.GetComponent<RectTransform>().anchoredPosition, new Vector2(cursorPositions[(int)cursorPos].anchoredPosition.x, cursorPositions[(int)cursorPos].anchoredPosition.y), cursorArrowSpeed * Time.deltaTime);

        if(Input.GetAxisRaw("Vertical") != 0  && !cursorCantMove)
        {
            if(!vAxisInUse && Input.GetAxisRaw("Vertical") < 0)
            {
                vAxisInUse = true;
                if ((int)cursorPos < 9)
                {
                    mainMenuController.sound.PlayOneShot(mainMenuController.buttonHover);
                    if ((int)cursorPos == 3)
                    {
                        cursorPos = (CursorPositions)9;
                    }
                    else
                    {
                        cursorPos++;
                    }

                }
            }
            else if (!vAxisInUse && Input.GetAxisRaw("Vertical") > 0)
            {
                vAxisInUse = true;
                if ((int)cursorPos > 0)
                {
                    mainMenuController.sound.PlayOneShot(mainMenuController.buttonHover);
                    if ((int)cursorPos == 9)
                    {
                        cursorPos = (CursorPositions)3;
                    }
                    else if ((int)cursorPos == 4)
                    {
                        cursorPos = (CursorPositions)0;
                    }
                    else
                    {
                        cursorPos--;
                    }
                }
            }

        }
        else if(Input.GetAxisRaw("Vertical") == 0)
        {
            vAxisInUse = false;
        }

        

        if (Input.GetAxisRaw("Horizontal") != 0 && !cursorCantMove)
        {
            if(!hAxisInUse && Input.GetAxisRaw("Horizontal") > 0)
            {
                hAxisInUse = true;
                if ((int)cursorPos < 4)
                {
                    mainMenuController.sound.PlayOneShot(mainMenuController.buttonHover);
                    cursorPos += 4;
                }
            }
            else if (!hAxisInUse && Input.GetAxisRaw("Horizontal") < 0)
            {


                hAxisInUse = true;
                if ((int)cursorPos > 3 && (int)cursorPos < 8)
                {
                    mainMenuController.sound.PlayOneShot(mainMenuController.buttonHover);
                    if ((int)cursorPos == 4)
                    {
                        cursorPos = (CursorPositions)1;
                    }
                    else
                    {
                        cursorPos -= 4;
                    }

                }
                else if ((int)cursorPos == 8)
                {
                    mainMenuController.sound.PlayOneShot(mainMenuController.buttonHover);
                    cursorPos -= 5;
                }
            }

        }
        else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            hAxisInUse = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !cursorCantMove)
        {
            
        }


        if (PressAnyKeyPanel.activeSelf)
        {
            bool joyKey = false;
            if(Input.anyKeyDown)
            {
                
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    for(int i = 0; i < 20; i++)
                    {
                        //print(kcode.ToString());
                        if(Input.GetKeyDown("joystick 1 button "+i))
                        {
                            joyKey = true;
                        }
                    }



                    if (Input.GetKeyDown(kcode))
                    {
                        if(keyBinding == KeyBinding.Jump)
                        {
                            if(!joyKey)
                            {
                                jumpKey.text = kcode.ToString();
                                InputManager.JumpTD = kcode;
                            }
                            else
                            {
                                jjumpKey.text = kcode.ToString().Remove(0, 9);
                                InputManager.JJumpTD = kcode;
                            }
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                        else if (keyBinding == KeyBinding.Slash)
                        {
                            if(!joyKey)
                            {
                                slashKey.text = kcode.ToString();
                                InputManager.Slash = kcode;
                            }
                            else
                            {
                                jslashKey.text = kcode.ToString().Remove(0, 9);
                                InputManager.JSlash = kcode;
                            }
                            
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                        else if (keyBinding == KeyBinding.Shuriken)
                        {
                            if(!joyKey)
                            {
                                shurikenKey.text = kcode.ToString();
                                InputManager.Shuriken = kcode;
                            }
                            else
                            {
                                jshurikenKey.text = kcode.ToString().Remove(0, 9);
                                InputManager.JShuriken = kcode;
                            }
                            
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                        else if (keyBinding == KeyBinding.GrapplingHook)
                        {
                            if(!joyKey)
                            {
                                grapplingHookKey.text = kcode.ToString();
                                InputManager.Hook = kcode;
                            }
                            else
                            {
                                jgrapplingHookKey.text = kcode.ToString().Remove(0, 9);
                                InputManager.JHook = kcode;
                            }
                            
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                        else if (keyBinding == KeyBinding.SmokeBomb)
                        {
                            if(!joyKey)
                            {
                                smokeBombKey.text = kcode.ToString();
                                InputManager.SmokeBomb = kcode;
                            }
                            else
                            {
                                jsmokeBombKey.text = kcode.ToString().Remove(0, 9);
                                InputManager.JSmokeBomb = kcode;
                            }
                            
                            InputManager.SaveKeys();
                            Invoke("SmallDelayToClose", 0.05f);
                        }
                    } 
                }
            }
        }
	}

    public void KeyBindingButton (int binding)
    {
        Invoke("SmallDelayToOpen", 0.05f);
        keyBinding = (KeyBinding)binding;
        keyToChangeText.text = "Press the key to set " + keyBinding.ToString();
    }

    public void SmallDelayToOpen()
    {
        PressAnyKeyPanel.SetActive(true);
    }

    public void SmallDelayToClose()
    {
        PressAnyKeyPanel.SetActive(false);
    }

    public void SetKeybindingsText()
    {
        jumpKey.text = InputManager.JumpTD.ToString();
        slashKey.text = InputManager.Slash.ToString();
        shurikenKey.text = InputManager.Shuriken.ToString();
        grapplingHookKey.text = InputManager.Hook.ToString();
        smokeBombKey.text = InputManager.SmokeBomb.ToString();

        jjumpKey.text = InputManager.JJumpTD.ToString().Remove(0, 9); 
        jslashKey.text = InputManager.JSlash.ToString().Remove(0, 9);
        jshurikenKey.text = InputManager.JShuriken.ToString().Remove(0, 9);
        jgrapplingHookKey.text = InputManager.JHook.ToString().Remove(0, 9);
        jsmokeBombKey.text = InputManager.JSmokeBomb.ToString().Remove(0, 9);

        if (optionSettings.displayHints)
        {
            checkmark.GetComponent<Image>().enabled = true;
        }
        else
        {
            checkmark.GetComponent<Image>().enabled = true;
        }
    }

    public void EditSlider()
    {
        
    }


}
