using UnityEngine;
using System.Collections;

public class LocationChanger : MonoBehaviour
{
    public GameObject distanceCalculator;
    GameObject player;
    public float distance;
    public float playerZ;
    public float startPlayerZ;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        startPlayerZ = player.transform.position.z;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            distance = Vector3.Distance(distanceCalculator.transform.position, other.transform.position);
            if(distance > 2 && distance < 13)
            {
                playerZ = startPlayerZ + (distance -2) /3.5f;
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, playerZ);
            }
            else if(distance < 2)
            {
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, startPlayerZ);
            }
            



            
            
        }
    }
}
