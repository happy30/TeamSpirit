//Katana script by Jordi

using UnityEngine;
using System.Collections;

public class Katana : MonoBehaviour
{

    public enum SwordType
    {
        PracticeSword,
        Katana
    };
    public SwordType swordType;

    public Transform playerModel;
    public PlayerController playerController;
    public GameObject SlashedObject;
    public Material katanaMaterial;
    public int attackPower = 1;
    public float dashPower;
    public float coolDown;

    AudioSource sound;
    public AudioClip swing;
    public AudioClip hitEnemy;
    public AudioClip hitDestructable;

    public Animator anim;

    public UIManager ui;
    public GameObject slashTrail;

    public GameObject hitParticle;
    GameObject spawnedhitParticle;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerModel = GameObject.Find("PlayerModel").transform;
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        //Debug.DrawRay(new Vector3(playerModel.position.x, playerModel.position.y +0.5f, playerModel.position.z), playerModel.forward);
        if(!ui.isPaused)
        {
            if (Input.GetKeyDown(InputManager.Slash) && !Camera.main.GetComponent<CameraController>().inCutscene && coolDown <= 0 || Input.GetKeyDown(InputManager.JSlash) && !Camera.main.GetComponent<CameraController>().inCutscene && coolDown <= 0)
            {
                //Slash(attackPower);
                playerController.anim.SetBool("Attack", true);
                playerController.StopMovement(1f);
                ui.UseSkill(0);
                coolDown = 1f;
            }

            if (coolDown > 0)
            {
                coolDown -= Time.deltaTime;
            }
        }
        

    }



    public void Slash(int attackMultiplier)
    {
        slashTrail.SetActive(true);
        Invoke("DeactivateTrail", 0.2f);
        sound.PlayOneShot(swing);
        playerController.anim.SetBool("Attack", false);
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(playerModel.position.x, playerModel.position.y +0.5f, playerModel.position.z), playerModel.forward, out hit, 3))
        {
            print(hit.transform.tag);
            if(hit.collider.tag == "Enemy")
            {
                sound.PlayOneShot(hitEnemy, 0.2f);
                SlashedObject = hit.collider.gameObject;
                hit.collider.transform.parent.GetComponent<EnemyMovement>().GetHit(attackPower);

                spawnedhitParticle = (GameObject)Instantiate(hitParticle, hit.collider.transform.position, Quaternion.identity);
                Destroy(spawnedhitParticle, 1f);

                /*
                if (SlashedObject.GetComponent<EnemyMovement>() != null)
                {
                    SlashedObject.GetComponent<EnemyMovement>().GetHit(attackPower * attackMultiplier);
                }
                if (SlashedObject.GetComponent<DestructibleScript>() != null)
                {
                    SlashedObject.GetComponent<DestructibleScript>().Destroy();
                }
                */
            }
            else if (hit.collider.tag == "Boss")
            {
                sound.PlayOneShot(hitEnemy, 0.2f);
                SlashedObject = hit.collider.gameObject;
                hit.collider.GetComponent<EnemyBoss>().GetHit(attackPower);

                spawnedhitParticle = (GameObject)Instantiate(hitParticle, hit.collider.transform.position, Quaternion.identity);
                Destroy(spawnedhitParticle, 1f);

                /*
                if (SlashedObject.GetComponent<EnemyMovement>() != null)
                {
                    SlashedObject.GetComponent<EnemyMovement>().GetHit(attackPower * attackMultiplier);
                }
                if (SlashedObject.GetComponent<DestructibleScript>() != null)
                {
                    SlashedObject.GetComponent<DestructibleScript>().Destroy();
                }
                */
            }

            else if (hit.collider.tag == "Destructible" && swordType == SwordType.Katana)
            {
                sound.PlayOneShot(hitDestructable, 1f);
                hit.collider.gameObject.GetComponent<DestructibleScript>().DestroyObject();
            }
        }
        
        else
        {
            SlashedObject = null;
        }
    }

    public void UpgradeWeapon()
    {
        swordType = SwordType.Katana;
        GameObject.FindWithTag("Katana").GetComponent<Renderer>().material = katanaMaterial;
        attackPower = 2;
    }

    public void DeactivateTrail()
    {
        slashTrail.SetActive(false);
    }

}
