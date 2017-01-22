//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartGameManager : MonoBehaviour {

    public int player1Rank;
    public int player2Rank;
    public GameObject rankPanel;

    public GameObject player1;
    public GameObject player2;

    public int player1Laps;
    public int player2Laps;
    public int maxLaps;

    public int player1Checkpoints;
    public int player2Checkpoints;
    public bool startCountDown;

    public bool raceStart;

    KartUIManager _UI;
    private float countDown = 3;

    void Awake(){
        _UI = GetComponent<KartUIManager>();
    }

    void Update()
    {
        if (startCountDown)
        {
            StartRace();
        }
    }

    public void SaveCheckpoints(){
        player1Checkpoints = player1.GetComponent<KartController>().nextCheckPoint;
        player2Checkpoints = player2.GetComponent<KartController>().nextCheckPoint;
        player1Laps = player1.GetComponent<KartController>().currentLap;
        player2Laps = player2.GetComponent<KartController>().currentLap;
        CheckRanks();
    }

    public void StartRace()
    {
        countDown -=  Time.deltaTime;
        int count = (int)countDown; 
        _UI.countDown.text = count.ToString();
        if(countDown <= 0)
        {
            startCountDown = false;
            player1.GetComponent<KartController>().raceStarted = true;
            player2.GetComponent<KartController>().raceStarted = true;
            raceStart = true;
           _UI.countDown.text = " ";
        }
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
        if(player1Laps == maxLaps)
        {
           player1.GetComponent<KartController>().raceStarted = false;
            _UI.player1EndScore.text = "Winner";
            _UI.player2EndScore.text = "Loser";
        }
        if (player2Laps == maxLaps)
        {
            player2.GetComponent<KartController>().raceStarted = false;
            _UI.player1EndScore.text = "Loser";
            _UI.player2EndScore.text = "Winner";
        }
        if(player1Laps == maxLaps || player2Laps == maxLaps)
        {
            raceStart = false;
            rankPanel.SetActive(true);
            GetComponent<KartMainMenu>().menuPanel.SetActive(true);
        }
    }
}
