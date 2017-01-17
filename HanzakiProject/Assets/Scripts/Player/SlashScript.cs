using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour {

    Katana player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Katana>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Slash(int power)
    {
        player.Slash(power);
    }

    public void Dash()
    {
        player.playerController.Dash(10f);
    }
}
