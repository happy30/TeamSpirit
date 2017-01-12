using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteppingStones : MonoBehaviour
{
    float startYPos;
    public float fallSpeed;
    public float riseSpeed;

    public bool sinking;

    void Start()
    {
        startYPos = transform.position.y;
    }

    void Update()
    {
        if(sinking)
        {
            Sink();
        }
        else
        {
            Rise();
        }
    }

    void Sink()
    {
        transform.position -= new Vector3(0, fallSpeed * Time.deltaTime, 0);
    }

    void Rise()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, startYPos, transform.position.z), riseSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            sinking = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            sinking = false;
        }
    }
}