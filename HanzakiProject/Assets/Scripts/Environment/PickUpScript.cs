//Made by Sascha Greve

using UnityEngine;
using System.Collections;

public class PickUpScript : MonoBehaviour
{
    public StatsManager stats;
    public QuestManager quests;
    public int progressionNeeded;
    public GameObject particles;
    public string itemName;
    public UIManager ui;

    public enum PickUpTypes
    {
        HookPart,
        Heart,
        Health,
        Shuriken,
        SmokeBomb,
        Katana
    };

    public PickUpTypes pickUpTypes;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        quests = GameObject.Find("GameManager").GetComponent<QuestManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {

        if (particles != null && quests.mainQuests[quests._progression.mainQuestProgression].atTask >= progressionNeeded)
        {
            particles.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && quests.mainQuests[quests._progression.mainQuestProgression].atTask >= progressionNeeded)
        {
            ui.PickUp(itemName);
            if(pickUpTypes == PickUpTypes.HookPart)
            {
                stats.AddHookPart();
                Destroy(gameObject);
            }
            if (pickUpTypes == PickUpTypes.Heart)
            {
                stats.GetLife();
                Destroy(gameObject);
            }
            if(pickUpTypes == PickUpTypes.Health)
            {
                stats.GetHealth();
                Destroy(gameObject);
            }
            if (pickUpTypes == PickUpTypes.Shuriken)
            {
                stats.AddShuriken();
                Destroy(gameObject);
            }
            if (pickUpTypes == PickUpTypes.SmokeBomb)
            {
                stats.AddSmokeBomb();
                Destroy(gameObject);
            }
            if (pickUpTypes == PickUpTypes.Katana)
            {
                stats.AddKatana();
                quests.NextTask();
                ui.SetQuestsText();
                Destroy(gameObject);
                
            }
        }
    }
}
