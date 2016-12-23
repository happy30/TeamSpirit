//QuestClass by Jordi

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class QuestClass
{
    public int questID;
    public string questTitle;
    public List<string> questTasks = new List<string>();

    public enum QuestState
    {
        Inactive,
        Active,
        Completed
    };
    public QuestState questState;
    public int atTask;

 
    
	
}
