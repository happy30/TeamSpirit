using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KappaChaseInstantiator : MonoBehaviour {

    public GameObject Kappa;
    GameObject spawnedKappa;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            spawnedKappa = (GameObject)Instantiate(Kappa, transform.position, Quaternion.identity);
        }
    }
}
