//InputManager by Jordi

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public InputManager instance;

    public static KeyCode Slash = KeyCode.Z;
    public static KeyCode Shuriken = KeyCode.X;
    public static KeyCode Hook = KeyCode.C;
    public static KeyCode SmokeBomb = KeyCode.V;
    public static KeyCode Jump;
    public static KeyCode JumpSS = KeyCode.UpArrow;
    public static KeyCode JumpTD = KeyCode.Space;

    public static KeyCode JSlash = KeyCode.Joystick1Button1;
    public static KeyCode JShuriken = KeyCode.Joystick1Button2;
    public static KeyCode JHook = KeyCode.Joystick1Button3;
    public static KeyCode JSmokeBomb = KeyCode.Joystick1Button5;
    public static KeyCode JJump;
    public static KeyCode JJumpSS = KeyCode.Joystick1Button0;
    public static KeyCode JJumpTD = KeyCode.Joystick1Button0;


    void Start()
    {
        if (instance)
        {
            return;
        }

        if (PlayerPrefs.HasKey("keya"))
        {
            Slash = (KeyCode)PlayerPrefs.GetInt("keya");
        }
        if (PlayerPrefs.HasKey("keyb"))
        {
            Shuriken = (KeyCode)PlayerPrefs.GetInt("keyb");
        }
        if (PlayerPrefs.HasKey("keyc"))
        {
            Hook = (KeyCode)PlayerPrefs.GetInt("keyc");
        }
        if (PlayerPrefs.HasKey("keyd"))
        {
            SmokeBomb = (KeyCode)PlayerPrefs.GetInt("keyd");
        }
        if(PlayerPrefs.HasKey("keyup"))
        {
            JumpSS = (KeyCode)PlayerPrefs.GetInt("keyup");
        }
        if (PlayerPrefs.HasKey("keyspace"))
        {
            JumpTD = (KeyCode)PlayerPrefs.GetInt("keyspace");
        }

        if (PlayerPrefs.HasKey("jkeya"))
        {
            JSlash = (KeyCode)PlayerPrefs.GetInt("jkeya");
        }
        if (PlayerPrefs.HasKey("jkeyb"))
        {
            JShuriken = (KeyCode)PlayerPrefs.GetInt("jkeyb");
        }
        if (PlayerPrefs.HasKey("jkeyc"))
        {
            JHook = (KeyCode)PlayerPrefs.GetInt("jkeyc");
        }
        if (PlayerPrefs.HasKey("jkeyd"))
        {
            JSmokeBomb = (KeyCode)PlayerPrefs.GetInt("jkeyd");
        }
        if (PlayerPrefs.HasKey("jkeyup"))
        {
            JJumpSS = (KeyCode)PlayerPrefs.GetInt("jkeyup");
        }
        if (PlayerPrefs.HasKey("jkeyspace"))
        {
            JJumpTD = (KeyCode)PlayerPrefs.GetInt("jkeyspace");
        }

        instance = this;
    }

    public static void SaveKeys()
    {
        PlayerPrefs.SetString("keya", Slash.ToString());
        PlayerPrefs.SetString("keyb", Shuriken.ToString());
        PlayerPrefs.SetString("keyc", Hook.ToString());
        PlayerPrefs.SetString("keyd", SmokeBomb.ToString());
        PlayerPrefs.SetString("keyspace", JumpTD.ToString());
        PlayerPrefs.SetString("jkeya", JSlash.ToString());
        PlayerPrefs.SetString("jkeyb", JShuriken.ToString());
        PlayerPrefs.SetString("jkeyc", JHook.ToString());
        PlayerPrefs.SetString("jkeyd", JSmokeBomb.ToString());
        PlayerPrefs.SetString("jkeyspace", JJumpTD.ToString());
    }
}

