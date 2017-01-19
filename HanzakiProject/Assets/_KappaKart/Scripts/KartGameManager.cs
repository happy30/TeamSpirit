//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartGameManager : MonoBehaviour {
    public List<int> playerLaps = new List<int>();
    public List<float> raceTimes = new List<float>();

    public int player1Rank;
    public int player2Rank;

    public GameObject player1;
    public GameObject player2;

    public int player1Laps;
    public int player2Laps;

    public int player1Checkpoints;
    public int player2Checkpoints;

    public void SaveCheckpoints(){
        player1Checkpoints = player1.GetComponent<KartController>().nextCheckPoint;
        player2Checkpoints = player2.GetComponent<KartController>().nextCheckPoint;
        player1Laps = player1.GetComponent<KartController>().currentLap;
        player2Laps = player2.GetComponent<KartController>().currentLap;
        CheckRanks();
    }

    public void CheckRanks(){
        if(player1Laps > player2Laps)
        {
            player1Rank = 1;
            player2Rank = 2;
        }
        else if(player1Laps > player2Laps)
        {
            player1Rank = 2;
            player2Rank = 1;
        }
        else if(player1Laps == player2Laps)
        {
            if(player1Checkpoints > player2Checkpoints)
            {
                player1Rank = 1;
                player2Rank = 2;
            }
            else if(player1Checkpoints < player2Checkpoints)
            {
                player1Rank = 2;
                player2Rank = 1;
            }
            else if(player1Checkpoints == player2Checkpoints)
            {
                player1Rank = 1;
                player2Rank = 1;
            }
        }
        GetComponent<KartUIManager>().DrawRanks();
    }
}
