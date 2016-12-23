//Smokebomb by Jordi

using UnityEngine;
using System.Collections;

public class SmokeBomb : MonoBehaviour
{

    public StatsManager stats;
    public bool reloading;
    public float reloadTimer;

    public GameObject particleObject;
    GameObject spawnedParticleObject;

    public PlayerController _playerController;
    public UIManager ui;

    void Awake()
    {
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        _playerController = GetComponent<PlayerController>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!ui.isPaused)
        {
            if (Input.GetKey(InputManager.SmokeBomb) && stats.smokeBombUnlocked && stats.smokeBombAmount > 0 && !reloading || Input.GetKey(InputManager.JSmokeBomb) && stats.smokeBombUnlocked && stats.smokeBombAmount > 0 && !reloading)
            {
                //Animatorplay blabla
                ThrowSmokeBomb();
                ui.UseSkill(3);
                reloading = true;
            }
            if (reloading)
            {
                reloadTimer += Time.deltaTime;
                if (reloadTimer > 5)
                {
                    reloading = false;
                    reloadTimer = 0;
                }
            }
        }
        
    }

    void ThrowSmokeBomb()
    {
        stats.smokeBombAmount--;
        _playerController.GetInvulnerable();    
        Destroy(spawnedParticleObject = (GameObject)Instantiate(particleObject, _playerController.transform.position, Quaternion.identity), 4);
    }
}
