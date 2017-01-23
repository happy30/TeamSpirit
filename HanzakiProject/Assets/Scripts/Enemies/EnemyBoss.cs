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
    float timer;
    public bool startSmashing;

    public bool start;

    public float rage;

    public Transform positionToBoulder;
    private float animTimer;

    public DestructibleScript boulder;
    public float x;
    public BossCollider bossCol;
    public GameObject particles;

    AudioSource sound;
    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip stepSound;

    float runSpeed;
    bool death;

    bool hasPlayed;



    void Awake () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        runSpeed = 10;
        sound = GetComponent<AudioSource>();
	}
	
	void Update () {

        if(startSmashing)
        {
            SmashBoulder();
        }
        else
        {
            if (rage <= 0 && player.position.x > transform.position.x && player.GetComponent<PlayerController>().invulnerable == true)
            {
                if(!startSmashing)
                {
                    sound.PlayOneShot(deathSound);
                    GetComponent<CapsuleCollider>().enabled = false;
                    anim.SetBool("Rage", true);
                    startSmashing = true;
                }
                
            }
            else if(start)
            {
                if (attackMode)
                {
                    Attacking();
                }
                else
                {
                    Walking();
                }
            }
        }

        if(transform.position.x > x && !death)
        {
            boulder.DestroyBoulder();
            runSpeed = 0;
            anim.SetBool("Die", true);
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
            death = true;
        }
        
        if(death)
        {
            if(!hasPlayed)
            {
                sound.PlayOneShot(deathSound);
                hasPlayed = true;
                Camera.main.gameObject.GetComponent<AudioSource>().Stop();
            }
            particles.SetActive(false);
            if(transform.eulerAngles.x < 90)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x + 40 * Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);
                Camera.main.GetComponent<CameraController>().distance = 10;
            }
            
        }
	}
    
    void Attacking()
    {
        if (agent.enabled)
        {
            agent.SetDestination(transform.position);
        }
        anim.SetBool("Attacking", true);
        anim.SetBool("Walking", false);
    }
    void Walking()
    {
        anim.SetBool("Attacking", false);
        anim.SetBool("Walking", true);
        if( anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (agent.enabled)
            {
                agent.SetDestination(player.position);
            }
        }
    }

    public void GetHit(int damageGet)
    {
        sound.PlayOneShot(hitSound);
        rage -= damageGet;
        if(rage <= 0)
        {
            particles.SetActive(true);
            
        }
    }

    void SmashBoulder()
    {
        agent.enabled = false;
        
        timer += Time.deltaTime;
       {
            if(timer > 1.5f)
            {
                transform.position = new Vector3(transform.position.x + runSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }
        
    }

    public void AttackAnimation()
    {
        if(bossCol.hasPlayer)
        {
            player.GetComponent<PlayerController>().GetHit(2, transform);
        }
    }


    public void ReturnDestination()
    {
        if(agent.enabled)
        {
            agent.SetDestination(player.position);
        }
        
    }

    public void ShakeCamera()
    {
        Camera.main.GetComponent<CameraController>().ShakeEffect();
        sound.PlayOneShot(stepSound, 0.5f);
    }
    
}
