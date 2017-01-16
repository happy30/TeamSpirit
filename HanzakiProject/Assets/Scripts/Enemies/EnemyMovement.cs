//EnemyMovement by Alieke
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    UnityEngine.AI.NavMeshAgent agent;
    enum States {Idle, Patrol, Chasing, Attacking}
    States enemyStates;
    Rigidbody _rb;

    public List<Transform> wayPoints = new List<Transform>();
    public float walkSpeed;
    public float runSpeed;

    public int currentWayPoint;
    public float rangeOffSet;
    public float maxWayPoints;
    private Animator anim;
    public bool isAlive;
    public float lookOutTimer;
    public float minLookOutTime;
    public float maxLookOutTime;

    public float attackRange;
    public int attackDamage;
    public float mayAttack;
    public RaycastHit hit;
    public float rayDis;
    public float outOfRange;

    public int health;
    private Image image;
    public GameObject healthSprite;
    public List<Sprite> spriteArray = new List<Sprite>();
    public float distance;
    public float playerDistance;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStates = States.Patrol;
        anim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        image = healthSprite.GetComponent<Image>();
    }

    void Update()
    {
        if (isAlive == true)
        {
            switch (enemyStates)
            {
                case States.Idle:
                    agent.speed = 0;
                    anim.SetBool("Attacking", false);
                    anim.SetBool("Walking", false);
                    anim.SetBool("Idle", true);
                    anim.SetBool("Running", false);
                    Idle();
                    break;
                case States.Patrol:
                    agent.speed = walkSpeed;
                    anim.SetBool("Attacking", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walking", true);
                    anim.SetBool("Running", false);
                    Patrol();
                    break;
                case States.Chasing:
                    anim.SetBool("Attacking", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walking", false);
                    anim.SetBool("Running", true);
                    agent.speed = runSpeed;
                    Chase();
                    break;
                case States.Attacking:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walking", false);
                    anim.SetBool("Attacking", true);
                    anim.SetBool("Running", false);
                    agent.speed = 0;
                    Attacking();
                    break;
            }
        }
        playerDistance = Vector3.Distance(player.position, transform.position);
        if (gameObject.GetComponent<EnemySight>().playerInView == true && playerDistance < outOfRange && enemyStates != States.Attacking && player.GetComponent<PlayerController>().invulnerable == false)
        {
            enemyStates = States.Chasing;
        }
    }

    void Patrol()
    {
        if(currentWayPoint >= wayPoints.Count)
        {
            currentWayPoint = 0;
        }
        agent.SetDestination(wayPoints[currentWayPoint].position);
        distance = Vector3.Distance(wayPoints[currentWayPoint].position, transform.position);
        if(distance < 3)
        {
            enemyStates = States.Idle;
        }
        else
        {
            enemyStates = States.Patrol;               
        }
        if(player.GetComponent<PlayerController>().invulnerable)
        {
            enemyStates = States.Patrol;
        }
    }

    void Idle()
    {
        lookOutTimer -= Time.deltaTime;
        if (lookOutTimer <= 0)
        {
            enemyStates = States.Patrol;
            currentWayPoint++;
            lookOutTimer = Random.Range(minLookOutTime, maxLookOutTime);
        }
    }

    public void Knockback()
    {
        print("knockback!!!!!!");
        _rb.velocity = new Vector3(transform.forward.x * -10f, 20f, 0);
    }

    void Chase()
    {
        if (playerDistance > outOfRange)
        {
            enemyStates = States.Patrol;
        }

        if (playerDistance <= attackRange)
        {
            enemyStates = States.Attacking;
        }
        else
        {
            agent.SetDestination(player.position);
            agent.speed = runSpeed;
           // anim.SetBool("Idle", true);
        }
    }

    void AttackAnimation()
    {
        print("Attack");
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDis))
        {
            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<PlayerController>().GetHit(attackDamage);
            }
        }
    }

    void Attacking()
    {
        if (playerDistance > attackRange)
        {
            enemyStates = States.Chasing;
        }
        if(player.GetComponent<PlayerController>().invulnerable)
        {
            enemyStates = States.Patrol;
        }
    }

    public void GetHit(int damageGet)
    {
        health -= damageGet;
        Knockback();
        if (health <= 0)
        {
            anim.SetBool("Death", true);
            GetComponent<EnemyMovement>().isAlive = false;
        }
        else
        {
            UpdatedHealth();
        }
    }

    void UpdatedHealth()
    {
        //image.sprite = spriteArray[health];
    }
}
