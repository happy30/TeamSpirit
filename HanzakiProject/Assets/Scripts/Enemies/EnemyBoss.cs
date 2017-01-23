//EnemyBoss by Alieke
using UnityEngine;
using System.Collections;

public class EnemyBoss : MonoBehaviour {
    private RaycastHit hit;
    public float rayDis;
    public bool attackMode;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator anim;
    public Transform player;

    private float attackRate;
    public float attackDamage;
    public float attackRange;
    private float mayAttack;

    private float rage;

    public Transform positionToBoulder;
    private float animTimer;


    void Awake () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        if (attackMode)
        {
            Attacking();
        }
	}
    
    void Attacking()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward, out hit, rayDis))
        {
            Debug.Log("fuck");
            if(hit.collider.tag == "Player")
            {
                agent.SetDestination(transform.position);
                anim.SetBool("Attacking", true);
                anim.SetBool("Walking", false);
            }
            else
            {
                anim.SetBool("Walking", true);
            }
        }
        else
        {
            anim.SetBool("Walking", true);
        }

    }

    void GetHit(int damageGet)
    {
        rage -= damageGet;
        if(rage <= 0)
        {
            //Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.BossFightRage);
            if(player.position.x > transform.position.x || player.GetComponent<PlayerController>().invulnerable == true)
            {
                SmashBoulder();
            }
        }
    }

    void SmashBoulder()
    {
        agent.SetDestination(positionToBoulder.position);
        attackMode = false;
        float distance = Vector3.Distance(transform.position, positionToBoulder.position);
        if (distance <= 1) { 
            anim.SetBool("Attacking", true);
            if (animTimer > anim.runtimeAnimatorController.animationClips.Length)
            {
                anim.SetBool("Death", true);
            }
            else
            {
                animTimer -= Time.deltaTime;
            }
        }
    }

    public void ReturnDestination()
    {
        agent.SetDestination(player.position);
    }
    
}
