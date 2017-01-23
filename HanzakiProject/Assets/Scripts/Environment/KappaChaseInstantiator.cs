using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KappaChaseInstantiator : MonoBehaviour {

    public GameObject Kappa;
    GameObject spawnedKappa;
    public GameObject kappaSpawnPoint;

    public AudioClip chaseBGM;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            spawnedKappa = (GameObject)Instantiate(Kappa, kappaSpawnPoint.transform.position, Quaternion.identity);
            Camera.main.GetComponent<AudioSource>().clip = chaseBGM;
            Camera.main.GetComponent<AudioSource>().Play();

        }
    }
}
