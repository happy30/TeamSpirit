using UnityEngine;
using System.Collections;

public class EndDemo : MonoBehaviour {

    public GameObject end;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            end.SetActive(true);
        }
    }
}
