using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KappaShurikenTrigger : MonoBehaviour
{
    public EnemyMovement enemyMov;

    void Awake()
    {
        enemyMov = transform.parent.GetComponent<EnemyMovement>();
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Shuriken")
        {
            Debug.Log("Hit by shuriken");
            enemyMov.GetHitByShuriken(col.GetComponent<ShurikenObject>().attackPower);
        }
    }
}
