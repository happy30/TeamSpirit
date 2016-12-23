//EnemyBoss by Alieke
using UnityEngine;
using System.Collections;

public class EnemyBoss : MonoBehaviour {
    private RaycastHit hit;
    public float rayDis;
    private bool attackMode;
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
	    if(Physics.Raycast(transform.position,transform.right,out hit, rayDis))
        {
            if(hit.transform.tag == "Player")
            {
                attackMode = true;
            }
        }
        if (attackMode)
        {
            Attacking();
        }
	}
    
    void Attacking()
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            if(attackRate <= 0)
            {
                anim.SetBool("Attacking", true);
            }
            else
            {
                if(mayAttack <= anim.runtimeAnimatorController.animationClips.Length)
                {
                    anim.SetBool("Attacking", false);
                }
                attackRate -= Time.deltaTime;
                mayAttack -= Time.deltaTime;

            }
        }
    }

    void GetHit(int damageGet)
    {
        rage -= damageGet;
        if(rage <= 0)
        {
            //Camera.main.GetComponent<BGMPlayer>().changeBGM(BGMPlayer.CurrentlyPlaying.BossFightRage);
            //if(player.position.x > transform.position.x || player.GetComponent<PlayerController>().invulnerable == true)
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
    
}
