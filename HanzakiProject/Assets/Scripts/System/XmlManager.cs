using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class XmlManager {

    [XmlElement("ShurikenCount")]
    public int shurikenCount;

    [XmlElement("SavePoints")]
    public int savePoint;

    [XmlElement("PlayerPosition")]
    public Vector3 playerPos;

    [XmlElement("QuestProgression")]
    public int questProgress;

    [XmlElement("GrapplingHook")]
    public bool grapplingHook;

    [XmlElement("SmokeBomb")]
    public bool smokeBomb;

    [XmlElement("Shuriken")]
    public bool shurikenBool;

    [XmlElement("Katarna")]
    public bool katana;

    [XmlElement("DisplayHotkeys")]
    public bool displayHotkeys;



    public XmlManager()
    {

    }
    public XmlManager(int points, int shuriken, Vector3 playerPosition, int questProg, bool grappHook, bool bombUnlocked, bool shurikenUnlocked, bool katanaUnlocked, bool disHotKeys)
    {
        savePoint = points;
        shurikenCount = shuriken;
        playerPos = playerPosition;
        questProgress = questProg;
        grapplingHook = grappHook;
        smokeBomb = bombUnlocked;
        shurikenBool = shurikenUnlocked;
        katana = katanaUnlocked;
        displayHotkeys = disHotKeys; 
    }
}
