using UnityEngine;
using System.Collections;

public class Cutscene_BlocksInitiate : MonoBehaviour {

    public Cutscene_Blocks[] blocks;


	
	public void Initiate()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].Activate();
        }
    }
}
