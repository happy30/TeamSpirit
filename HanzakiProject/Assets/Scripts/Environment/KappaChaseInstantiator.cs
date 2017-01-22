using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KappaChaseInstantiator : MonoBehaviour {

    public GameObject Kappa;
    GameObject spawnedKappa;
    public GameObject kappaSpawnPoint;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            spawnedKappa = (GameObject)Instantiate(Kappa, kappaSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
