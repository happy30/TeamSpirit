//Made by Faf
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{
    public bool raceStarted;
    public int nextCheckPoint;
    public int playerPos;
    public int currentLap;

    public float speed;
    public float normalSpeed;
    public float rampSpeed;
    public float boostSpeed;

    public float backSpeed;
    private bool mayTurn;

    public float rotateSpeed;
    public float restoreRotationSpeed;

    public float hoverHeight;
    public float hoverForce;
    public float hoverDamp;

    public string verticalSpeed;
    public string horizontalRot;

    public string gasButton;
    public string reverseButton;

    private RaycastHit hit;
    public float rayDis;

    private Rigidbody _rb;
    public Transform startPos;

    void Start()
    {
        speed = normalSpeed;
        _rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
    void FixedUpdate()
    {
        if (raceStarted)
        {
            VehicleMove();
        }

        RaycastHit hit;
        if (!Physics.Raycast(transform.position, -transform.up, out hit, 1.5f))
        {
            transform.eulerAngles = new Vector3(
                Mathf.LerpAngle(transform.eulerAngles.x, 0, restoreRotationSpeed * Time.deltaTime),
                transform.eulerAngles.y,
                0);
        }
        else
        {
            transform.eulerAngles = new Vector3(
             Mathf.LerpAngle(transform.eulerAngles.x, 0, restoreRotationSpeed * 0.2f * Time.deltaTime),
             transform.eulerAngles.y,
             0);
        }
            Ray downRay = new Ray(transform.position, -Vector3.up);
            if (Physics.Raycast(downRay, out hit))
            {
                float hoverError = hoverHeight - hit.distance;
                if (hoverError > 0)
                {
                    float upwardSpeed = _rb.velocity.y;
                    float lift = hoverError * hoverForce - upwardSpeed * hoverDamp;
                    _rb.AddForce(lift * Vector3.up);
                }
            }
    }

    void VehicleMove()
    {
        float ySpeed = Input.GetAxis(verticalSpeed);
        float xRot = Input.GetAxis(horizontalRot);
        bool aButton = Input.GetButton(gasButton);
        bool bButton = Input.GetButton(reverseButton);

        if(Physics.Raycast(transform.position,-transform.up,out hit, rayDis))
        {
            if(hit.transform.tag == "Ramp")
            {
                _rb.AddForce(rampSpeed * transform.forward);
            }
        }

        // Front Back
        if (aButton)
        { 
            _rb.velocity += transform.forward * speed * Time.deltaTime;
            if(!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
            
        }
        else
        {
            GetComponent<AudioSource>().Pause();
        }
        if (bButton)
        {
            _rb.velocity -= transform.forward * speed * Time.deltaTime;
        }


        // Turning
        Vector3 a = transform.eulerAngles;

            float rotation = 0;
            if (xRot > 0)
            {
                rotation = a.y + rotateSpeed * Input.GetAxis(horizontalRot);
                transform.eulerAngles = new Vector3(a.x, rotation, a.z);
            }
            else if (xRot < 0)
            {
                rotation = a.y - rotateSpeed * -Input.GetAxis(horizontalRot);
                transform.eulerAngles = new Vector3(a.x, rotation, a.z);
            }
    }
    public void ResetKart()
    {
        transform.position = startPos.position;
        transform.rotation = startPos.rotation;
        nextCheckPoint = 0;
        currentLap = 0;
    }
}




