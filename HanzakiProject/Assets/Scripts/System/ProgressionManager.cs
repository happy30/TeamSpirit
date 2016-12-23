//ProgressionManager by Jordi

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressionManager : MonoBehaviour
{
    public int taskProgession;
    public int mainQuestProgression;

    /* 
        There will be 4 spawnpoints
        from     lv1 -> lv2
                 lv2 -> lv1
                 lv2 -> lv3
                 lv3 -> lv2
    */

    public List<Vector3> spawnPoints = new List<Vector3>();
    public int spawnPointNumber;
}
