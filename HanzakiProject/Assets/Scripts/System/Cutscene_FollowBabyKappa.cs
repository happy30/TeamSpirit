using UnityEngine;
using System.Collections;

public class Cutscene_FollowBabyKappa : MonoBehaviour {

    public Transform babyKappa;

	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(babyKappa.transform.position.x, transform.position.y, transform.position.z);
	}
}
