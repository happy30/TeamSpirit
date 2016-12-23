using UnityEngine;
using System.Collections;

public class EnemyHead : MonoBehaviour {


    Collision OnCollisionEnter (Collision trigger)
    {
        if(trigger.transform.tag == "Shuriken")
        {
            //GetComponent<EnemyMovement>().GetHit(trigger.GetComponent<Shuriken>().damage);
        }
        return trigger;
    }
}
