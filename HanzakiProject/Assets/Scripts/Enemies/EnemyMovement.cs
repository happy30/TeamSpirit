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

    public int maxHealth;
    public int health;
    private Image image;

    GameObject spawnedHealthSprite;
    public Vector3 healthSpriteRotation;
    public float spriteYOffset;
    public GameObject healthSprite;
    public List<Sprite> spriteArray = new List<Sprite>();

    public float distance;
    public float playerDistance;

    public Component[] colliders;

    public GameObject deathParticle;
    GameObject spawnedDeathparticle;



    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStates = States.Patrol;
        anim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        image = healthSprite.GetComponent<Image>();
        maxHealth = health;
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
                    Idle();
                    break;
                case States.Patrol:
                    agent.speed = walkSpeed;
                    anim.SetBool("Attacking", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walking", true);
                    Patrol();
                    break;
                case States.Chasing:
                    anim.SetBool("Attacking", false);
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walking", true);
                    agent.speed = runSpeed;
                    Chase();
                    break;
                case States.Attacking:
                    anim.SetBool("Idle", false);
                    anim.SetBool("Walking", false);
                    anim.SetBool("Attacking", true);
                    agent.speed = 0;
                    Attacking();
                    break;
            }
        }
        else
        {
            Invoke("Die", 0.5f);
        }
        playerDistance = Vector3.Distance(player.position, transform.position);
        if (gameObject.GetComponent<EnemySight>().playerInView == true && playerDistance < outOfRange && enemyStates != States.Attacking && player.GetComponent<PlayerController>().invulnerable == false)
        {
            enemyStates = States.Chasing;
        }

        if(playerDistance < 10 && spawnedHealthSprite == null && isAlive)
        {
            spawnedHealthSprite = (GameObject)Instantiate(healthSprite);
            spawnedHealthSprite.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            spawnedHealthSprite.GetComponent<HealthFollowEnemy>().enemy = transform;
            spawnedHealthSprite.transform.eulerAngles = healthSpriteRotation;
            spawnedHealthSprite.GetComponent<HealthFollowEnemy>().yOffset = spriteYOffset;
            UpdatedHealth();

        }
        else if(playerDistance > 10)
        {
            Destroy(spawnedHealthSprite);
        }

    }

    void Die()
    {
        colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        GetComponent<Rigidbody>().useGravity = false;
            
        agent.enabled = false;

        transform.position -= new Vector3(0, 1f * Time.deltaTime, 0);
    }

    void SpawnDeathParticle()
    {
        spawnedDeathparticle = (GameObject)Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(spawnedDeathparticle, 5f);
    }

    void Patrol()
    {
        if(currentWayPoint >= wayPoints.Count)
        {
            currentWayPoint = 0;
        }
        if(agent.enabled)
        {
            agent.SetDestination(wayPoints[currentWayPoint].position);
        }
        
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
        Camera.main.GetComponent<CameraController>().ShakeEffect();
        if(player.position.x - transform.position.x > 0)
        {
            _rb.velocity = new Vector3(-5f, 8f, 0);
        }
        else
        {
            _rb.velocity = new Vector3(5f, 8f, 0);
        }
        
    }

    void Chase()
    {
        if (playerDistance > outOfRange)
        {
            enemyStates = States.Patrol;
        }

        Debug.Log(playerDistance + " playerdistance," + attackRange + " Attackrange"); 
        if (playerDistance <= attackRange)
        {
            enemyStates = States.Attacking;
        }
        else
        {
            if(agent.enabled)
            {
                agent.SetDestination(player.position);
                agent.speed = runSpeed;
            }
            anim.SetBool("Idle", true);

        }
    }

    void AttackAnimation()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDis))
        {
            if (hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<PlayerController>().GetHit(attackDamage, transform);
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
        Vector3 targetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        this.transform.LookAt(targetPostition);
    }

    public void GetHit(int damageGet)
    {
        agent.enabled = false;
        Invoke("EnableAgent", 0.5f);
        health -= damageGet;
        Knockback();
        if (health <= 0)
        {
            Invoke("SpawnDeathParticle", 1f);
            anim.SetBool("Death", true);
            GetComponent<EnemyMovement>().isAlive = false;
            if (spawnedHealthSprite != null)
            {
                Destroy(spawnedHealthSprite);
            }
        }
        else
        {
            UpdatedHealth();
        }
    }

    void UpdatedHealth()
    {
        if(spawnedHealthSprite != null)
        {
            if((float)health / (float)maxHealth == 1)
            {
                spawnedHealthSprite.GetComponent<SpriteRenderer>().sprite = spriteArray[0];
                print("max" + health / maxHealth);
            }
            else if ((float)health / (float)maxHealth > 0.7f)
            {
                spawnedHealthSprite.GetComponent<SpriteRenderer>().sprite = spriteArray[1];
                print("1" + health / (float)maxHealth);
            }
            else if ((float)health / (float)maxHealth > 0.3f)
            {
                spawnedHealthSprite.GetComponent<SpriteRenderer>().sprite = spriteArray[2];
                print("2" + health / (float)maxHealth);
            }
            else if((float)health / (float)maxHealth < 0.3f)
            {
                print("3" + health / maxHealth);
                spawnedHealthSprite.GetComponent<SpriteRenderer>().sprite = spriteArray[3];
            }
        }
    }

    void EnableAgent()
    {
        agent.enabled = true;
    }
}
