// ShurikenObject by jordi

using UnityEngine;
using System.Collections;

public class ShurikenObject : MonoBehaviour
{
    public int attackPower;
    public float projectileSpeed;
    public Transform player;
    public PlayerController playerController;

    public Transform model;
    public Transform model2;

    public bool hit;

    public EnemyMovement enemy;

    public Vector3 shurikenDirection;

    void Awake()
    {
        player = GameObject.Find("PlayerModel").transform;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        if(playerController.levelType == PlayerController.LevelType.SS)
        {
            if (playerController.playerRotation.y < 100)
            {
                shurikenDirection = new Vector3(1, 0, 0);
            }
            if (playerController.playerRotation.y > 200)
            {
                shurikenDirection = new Vector3(-1, 0, 0);
            }
        }
        else
        {
            shurikenDirection = player.transform.forward;
        }
        
        
        transform.eulerAngles = new Vector3(90, 0, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(!hit)
        {
            transform.Translate(new Vector3(shurikenDirection.x * projectileSpeed * Time.deltaTime, shurikenDirection.z * projectileSpeed * Time.deltaTime, shurikenDirection.y * projectileSpeed * Time.deltaTime));
            model2.Rotate(0, 0, 2000 * Time.deltaTime);
            model.Rotate(200 * Time.deltaTime, 0, 0);
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Destructible")
        {
            projectileSpeed = 0;
            transform.SetParent(col.gameObject.transform);
            model2.Rotate(0, 0, 0);
            model.Rotate(0, 0, 0);
            hit = true;
        } 
    }
}
