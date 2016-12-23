using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilityHotkey : MonoBehaviour {

    public enum Skill
    {
        Slash,
        Shuriken,
        GrapplingHook,
        SmokeBomb,
        Dash
    };

    public Skill skill;

    public StatsManager stats;
    public OptionsSettings options;
    public Text hotkeyText;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        options = GameObject.Find("GameManager").GetComponent<OptionsSettings>();
        hotkeyText = GetComponentInChildren<Text>();

            
    }
	// Use this for initialization
	void Start ()
    {
	    if(!options.displayHotkeys)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if(skill == Skill.Slash)
            {
                hotkeyText.text = InputManager.Slash.ToString();
            }
            else if (skill == Skill.Shuriken)
            {
                hotkeyText.text = InputManager.Shuriken.ToString();
            }
            else if (skill == Skill.GrapplingHook)
            {
                hotkeyText.text = InputManager.Hook.ToString();
            }
            else if (skill == Skill.SmokeBomb)
            {
                hotkeyText.text = InputManager.SmokeBomb.ToString();
            }
            else if (skill == Skill.Dash)
            {
                //komt later maybe
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
