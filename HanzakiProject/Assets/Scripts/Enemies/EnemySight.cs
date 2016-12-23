using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    public GameObject player;
    public float fieldOfViewAngle;
    public bool playerInView;

    private SphereCollider coll;

	void Awake ()
    {
        coll = GetComponent<SphereCollider>();
	}
	
    void OnTriggerStay(Collider trigger)
    {
        if(trigger.transform.tag == "Player")
        {
            Vector3 directionToPlayer = trigger.transform.position - transform.position;
            float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
            if (angleToPlayer < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, coll.radius))
                {
                    if (hit.transform.tag == "Player")
                    {
                        playerInView = true;
                    }
                }
            }
            else
            {
                playerInView = false;
            }
        }
    }
}
