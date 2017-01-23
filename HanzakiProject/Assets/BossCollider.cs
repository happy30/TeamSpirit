using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollider : MonoBehaviour
{
    public EnemyBoss enemyBoss;

    public bool hasPlayer;


    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            enemyBoss.attackMode = true;
            hasPlayer = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            hasPlayer = false;
            enemyBoss.attackMode = false;
        }
    }
}
