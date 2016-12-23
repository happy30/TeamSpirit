//Made by Arne
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartShuriken : MonoBehaviour {

    public int shurikenCount;
    public bool throwShuriken;

    public float shurikenSpeed;
    public GameObject shurikenObject;

    public float nextShot = 0.0f;
    public float interval = 0.8f;


    public float distance;
    public float groundDistance = 2.0f;
    public Vector3 downScan;    //Scans downwards for ground distance


	// Use this for initialization
	void Start () {
     
    }
	// Update is called once per frame
	void FixedUpdate () {
        Move();
        ShurikenAttack();
	}
    public void Move()
    {
       
    }
    public void ShurikenAttack ()
    {
        if (Input.GetKey(InputManager.Shuriken))
        {
            if(shurikenCount <= 0)
            {
                print("cant touch this");
            }
            else
            {
                if (Time.time >= nextShot)
                {
                    nextShot = Time.time + interval;
                    GameObject clone;
                    clone = Instantiate(shurikenObject, transform.position + (transform.forward * 2), transform.rotation);
                    clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * shurikenSpeed);
                    shurikenCount = shurikenCount - 1;
                }
            }
        }
    }
}
/*if (throwShuriken == false)
            {
                GameObject clone;
                clone = Instantiate(shurikenObject, transform.position, transform.rotation);
                clone.rigidbody.AddForce(clone.transform.forward * shurikenSpeed);
                shurikenCount = shurikenCount - 1;
                throwShuriken = true;
            }
            else if (throwShuriken = true)
            {
                if (shurikenCount <= 0)
                {
                    //empty item box
                    print("cant touch this");
                }
                else
                {
                   throwShuriken = false;
                }
            }
            */
