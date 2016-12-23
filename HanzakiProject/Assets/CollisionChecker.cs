using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour {

    public PlayerController player;
    public float factor;
    public int ID;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + Input.GetAxisRaw("Horizontal") * factor, player.transform.position.y, player.transform.position.z + Input.GetAxisRaw("Vertical") * factor), 10f * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Ground")
        {
            player.hasCollided[ID] = true;
        }
        else
        {
            player.hasCollided[ID] = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            player.hasCollided[ID] = false;
        }
    }


}
