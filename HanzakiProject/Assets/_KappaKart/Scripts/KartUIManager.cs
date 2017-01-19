using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartUIManager : MonoBehaviour {

    public KartGameManager stats;

   /* public Sprite katanaIcon;
    public Sprite bombIcon;
    public Sprite hookIcon;
    public Sprite boxIcon;
    public Sprite shurikenIcon;

    public Sprite heldItem;
    public Sprite emptyItem;*/

    public Text player1Lap;
    public Text player2Lap;

    public Text rank1;
    public Text rank2;


    void DrawMinimap()
    {

    }

    public void DrawRanks()
    {
        rank1.text = stats.player1Rank.ToString();
        rank2.text = stats.player2Rank.ToString();
        player1Lap.text = stats.player1Laps.ToString();
        player2Lap.text = stats.player2Laps.ToString();
    }

    /*void DrawHeldItem(Sprite itemGot)
    {
        heldItem = itemGot;
    }

    void HeldItemUsed()
    {
        heldItem = emptyItem;
    }*/
}
