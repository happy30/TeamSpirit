using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour
{
    public GameObject player;
    public float fieldOfViewAngle;
    public bool playerInView;
    public bool inTrigger;

    private SphereCollider coll;
    Collider storedCol;

	void Awake ()
    {
        coll = GetComponent<SphereCollider>();
        player = GameObject.FindWithTag("Player");
	}
    
    void Update()
    {
        if(inTrigger)
        {
            WhileInTrigger();
        }
        
    }

    void WhileInTrigger()
    {
        coll.enabled = false;
        print("trigger");
        Vector3 directionToPlayer = storedCol.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
        if (angleToPlayer < fieldOfViewAngle * 0.5f)
        {
            print("range");
            RaycastHit hit;
            Debug.DrawRay(transform.position, directionToPlayer);
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, coll.radius))
            {
                print(hit.transform.tag);
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
        if(Vector3.Distance(transform.position, player.transform.position) > 10)
        {
            inTrigger = false;
            coll.enabled = true;
            storedCol = null;
        }
    }
	
    void OnTriggerEnter(Collider trigger)
    {
        if(trigger.transform.tag == "Player")
        {
            print ("inTrigger");
            storedCol = trigger;
            inTrigger = true;   
        }
    }
}
