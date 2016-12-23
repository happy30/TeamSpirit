using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class DataManager : MonoBehaviour {

    XmlManager xmlSaved = new XmlManager();
    XmlManager xmlToSave;
    public Transform player;
    public GameObject gameManager;

    public StatsManager statMan;
    public ProgressionManager progMan;
    public OptionsSettings optionMan;

    int shurikenCount;
    int currSavePoint;
    int questProgress;
    bool grappHook;
    bool smokeBomb;
    bool shuriken;
    bool katana;
    bool displayHotkeys;


    void Awake()
    {
        statMan = gameManager.GetComponent<StatsManager>();
        progMan = gameManager.GetComponent<ProgressionManager>();
        optionMan = gameManager.GetComponent<OptionsSettings>();

        currSavePoint = progMan.spawnPointNumber;
        shurikenCount = statMan.shurikenAmount;
        questProgress = gameManager.GetComponent<ProgressionManager>().taskProgession;
        grappHook = statMan.grapplingHookUnlocked;
        smokeBomb = statMan.smokeBombUnlocked;
        shuriken = statMan.shurikenUnlocked;
        katana = statMan.katanaUnlocked;
        displayHotkeys = optionMan.displayHotkeys;


         xmlToSave = new XmlManager(currSavePoint, shurikenCount, player.position, questProgress, grappHook, smokeBomb, shuriken, katana, displayHotkeys);
    }

	public void LoadData ()
    {
        xmlSaved = StreamData();
        progMan.spawnPointNumber = xmlSaved.savePoint;
        statMan.shurikenAmount = xmlSaved.shurikenCount;
        player.position = xmlSaved.playerPos;
        gameManager.GetComponent<ProgressionManager>().taskProgession = xmlSaved.questProgress;
        statMan.grapplingHookUnlocked = xmlSaved.grapplingHook;
        statMan.smokeBombUnlocked = xmlSaved.smokeBomb;
        statMan.shurikenUnlocked = xmlSaved.shurikenBool;
        statMan.katanaUnlocked = xmlSaved.katana;
        optionMan.displayHotkeys = xmlSaved.displayHotkeys;
    }

    public void SaveData()
    {
        xmlSaved = WriteData(xmlToSave);
        xmlSaved.savePoint = progMan.spawnPointNumber;
        xmlSaved.shurikenCount = statMan.shurikenAmount;
        xmlSaved.playerPos = player.position;
        xmlSaved.questProgress = gameManager.GetComponent<ProgressionManager>().taskProgession;
        xmlSaved.grapplingHook = statMan.grapplingHookUnlocked;
        xmlSaved.smokeBomb = statMan.smokeBombUnlocked;
        xmlSaved.shurikenBool = statMan.shurikenUnlocked;
        xmlSaved.katana = statMan.katanaUnlocked;
        xmlSaved.displayHotkeys = optionMan.displayHotkeys;
    }

    public XmlManager StreamData()
    {
        StreamReader reader = new StreamReader(Application.dataPath + "/Scripts/XML_File.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(XmlManager));
        XmlManager xmlManager = serializer.Deserialize(reader) as XmlManager;
        reader.Close();
        return xmlManager;
    }

    public XmlManager WriteData(XmlManager manager)
    {
        StreamWriter writer = new StreamWriter(Application.dataPath + "/Scripts/XML_File.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(XmlManager));
        serializer.Serialize(writer, manager);
        writer.Close();
        return manager;
    }
}
