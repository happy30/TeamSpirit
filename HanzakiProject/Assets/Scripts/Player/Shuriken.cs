//Shuriken by Jordi

using UnityEngine;
using System.Collections;

public class Shuriken : MonoBehaviour {

    public StatsManager stats;
    public int attackPower;
    public float reloadTimer;
    public bool reloading;

    public GameObject shurikenObject;
    GameObject spawnedShurikenObject;
    public UIManager ui;
    public Animator anim;
    public Transform playerModel;
    public PlayerController player;

    public AudioSource sound;
    public AudioClip shurikenThrow;


    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        player = GetComponent<PlayerController>();
        sound = GetComponent<AudioSource>();
    }

	
	// Update is called once per frame
	void Update ()
    {
        if(!ui.isPaused && player.canMove)
        {
            if (Input.GetKey(InputManager.Shuriken) && stats.shurikenUnlocked && stats.shurikenAmount > 0 && !reloading || Input.GetKey(InputManager.JShuriken) && stats.shurikenUnlocked && stats.shurikenAmount > 0 && !reloading)
            {
                player.StopMovement(0.4f);
                anim.SetBool("Shuriken", true);
                Invoke("Delay", 0.1f);
                ui.UseSkill(1);
                reloading = true;
            }
            if (reloading)
            {
                reloadTimer += Time.deltaTime;
                if (reloadTimer > 1)
                {
                    reloading = false;
                    reloadTimer = 0;
                }
            }
        }
	    
	}

    public void Delay()
    {
        ThrowShuriken(attackPower);
    }

    public void ThrowShuriken(int attackPower)
    {
        sound.PlayOneShot(shurikenThrow);
        Destroy(spawnedShurikenObject = (GameObject)Instantiate(shurikenObject, new Vector3(transform.position.x, transform.position.y +0.9f, transform.position.z) + playerModel.transform.forward * 1, Quaternion.identity), 3);
        spawnedShurikenObject.GetComponent<ShurikenObject>().attackPower = attackPower;
        stats.shurikenAmount--;
        anim.SetBool("Shuriken", false);
    }
}
