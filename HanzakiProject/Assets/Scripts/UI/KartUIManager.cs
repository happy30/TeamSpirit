using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartUIManager : MonoBehaviour {

    public KartGameManager stats;

    public Sprite katanaIcon;
    public Sprite bombIcon;
    public Sprite hookIcon;
    public Sprite boxIcon;
    public Sprite shurikenIcon;

    public Sprite heldItem;
    public Sprite emptyItem;

    public Text player1Standing;
    public Text player2Standing;

    List<int> playerStandings = new List<int>();

    public Text player1Round;
    public Text player2Round;

    public string rank1;
    public string rank2;


    void DrawMinimap()
    {

    }

    public void SortRanks()
    {
        if (stats.player1Checkpoints > stats.player2Checkpoints){
           // player1Standing = 
          //  player2Standing = 
        }
        else{
           // player1Standing = 
           // player2Standing = 
        }
      //  DrawRanks();
    }

    public void DrawRanks()
    {
    }

    void DrawSpeed()
    {

    }

    void DrawHeldItem(Sprite itemGot)
    {
        heldItem = itemGot;
    }

    void HeldItemUsed()
    {
        heldItem = emptyItem;
    }
}
