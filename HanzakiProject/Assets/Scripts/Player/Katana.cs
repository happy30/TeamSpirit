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

    public Animator anim;

    public UIManager ui;

    void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerModel = GameObject.Find("PlayerModel").transform;
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if(!ui.isPaused)
        {
            if (Input.GetKeyDown(InputManager.Slash) && !Camera.main.GetComponent<CameraController>().inCutscene && coolDown <= 0 || Input.GetKeyDown(InputManager.JSlash) && !Camera.main.GetComponent<CameraController>().inCutscene && coolDown <= 0)
            {
                //Slash(attackPower);
                playerController.anim.SetBool("Attack", true);
                ui.UseSkill(0);
                coolDown = 1.5f;
            }

            if (coolDown > 0)
            {
                coolDown -= Time.deltaTime;
            }
        }
        

    }



    public void Slash(int attackMultiplier)
    {
        playerController.anim.SetBool("Attack", false);
        RaycastHit hit;
        if(Physics.Raycast(playerModel.position, playerModel.forward, out hit, 2))
        {
            if(hit.collider.tag == "Enemy")
            {
                SlashedObject = hit.collider.gameObject;
                hit.collider.transform.parent.GetComponent<EnemyMovement>().GetHit(attackPower);
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

}
