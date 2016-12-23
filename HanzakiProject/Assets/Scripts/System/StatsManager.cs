//StatsManager by Jordi

using UnityEngine;
using System.Collections;

public class StatsManager : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public int health;
    public int maxHealth;

    public int hookParts;
    public bool katanaUnlocked;
    public bool grapplingHookUnlocked;
    public bool shurikenUnlocked;
    public bool smokeBombUnlocked;

    public int shurikenAmount;
    public int smokeBombAmount;

    public void GetLife()
    {
        maxHealth += 2;
        health += 2;
        GameObject.Find("Canvas").GetComponent<HeartScript>().DrawHearts();
    }

    public void AddHookPart()
    {
        hookParts++;
        if(hookParts >= 2)
        {
            grapplingHookUnlocked = true;
            GameObject.Find("Canvas").GetComponent<UIManager>().UnlockIcons();
            GameObject.Find("Canvas").GetComponent<UIManager>().UnlockAbility();
            GetComponent<QuestManager>().NextTask();
            GameObject.Find("Canvas").GetComponent<UIManager>().SetQuestsText();
        }
    }

    public void GetHealth()
    {
        if(health < maxHealth)
        {
            health++;
            GameObject.Find("Canvas").GetComponent<HeartScript>().DrawHearts();
        }
    }

    public void AddShuriken()
    {
        shurikenUnlocked = true;
        shurikenAmount++;
        GameObject.Find("Canvas").GetComponent<UIManager>().UnlockIcons();
        
		
    }

    public void AddSmokeBomb()
    {
        smokeBombUnlocked = true;
        smokeBombAmount++;
        GameObject.Find("Canvas").GetComponent<UIManager>().UnlockIcons();
    }

    public void AddKatana()
    {
        katanaUnlocked = true;
        GameObject.Find("Player").GetComponent<Katana>().UpgradeWeapon();
        GameObject.Find("Canvas").GetComponent<UIManager>().UnlockIcons();
    }
}
