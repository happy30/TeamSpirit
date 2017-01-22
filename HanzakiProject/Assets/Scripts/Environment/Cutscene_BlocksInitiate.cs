using UnityEngine;
using System.Collections;

public class Cutscene_BlocksInitiate : MonoBehaviour {

    public Cutscene_Blocks[] blocks;

    public AudioSource sound;
    public AudioClip clip;
        


	
	public void Initiate()
    {
        sound.PlayOneShot(clip);
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].Activate();
        }
    }
}
