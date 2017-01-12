using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFollowEnemy : MonoBehaviour {

    public Transform enemy;
    public float yOffset;

    public float alpha;
		

	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(enemy.position.x, enemy.position.y + yOffset, enemy.position.z);
        if(GetComponent<SpriteRenderer>().color.a < 1)
        {
            alpha +=  Time.deltaTime;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        }
        
	}
}
