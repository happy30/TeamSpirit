//CameraController by Jordi

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public PlayerController playerController;
    
    public float cameraOffsetX;
    public float cameraOffsetY;
    public float cameraOffsetZ;
    public float distance;

    public float followTime;
    public bool inCutscene;
    public bool inPuzzle;
    public enum CameraStance
    {
        Right,
        Left,
        Idle
    };

    public Vector3 additionalCameraOffset;

    public CameraStance stance;
    public bool cameraProg;

    float timer;

    public GameObject followObject;
    public GameObject hookObject;

    public float cutsceneZ;

    public Vector3 cameraRot;
    Vector3 cameraPos;


	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cutsceneZ = transform.position.z - 1f;

        followTime = 1.5f;

        SetUp();
    }
	
    public void SetUp()
    {
        if (playerController.levelType == PlayerController.LevelType.TD)
        {
            cameraOffsetY = 14;
            cameraRot = new Vector3(45, 0, 0);
        }
        else
        {
            cameraOffsetY = 1;
            //transform.eulerAngles = new Vector3(0, 0, 0);
            cameraRot = new Vector3(0, player.GetComponent<PlayerController>().rotationOffset, 0);
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if(!inCutscene)
        {
            FollowPlayer();
        }
        else
        {
            if(followObject != null)
            {
                FollowObject(followObject);
            }
        }

        
    }

    //Make the camera follow the player. If the player moves the camera offset will change so that it gives a better vision of what's in front of the player.
    public void FollowPlayer()
    {
        if(playerController.xMovement > 0.01)
        {
            if(stance != CameraStance.Right)
            {
                timer = 0;
                //cameraOffsetX = 5;
                additionalCameraOffset = player.transform.right * 5f;
                followTime = 0;
                stance = CameraStance.Right;
            }
            else
            {
                if (timer < 0.7f)
                {
                    timer += Time.deltaTime;
                    followTime = timer * 3;
                }
            }
        }
        else if (playerController.xMovement < -0.01)
        {
            if (stance != CameraStance.Left)
            {
                timer = 0;
                //cameraOffsetX = -5;
                additionalCameraOffset = player.transform.right * -5f;
                followTime = 0;
                stance = CameraStance.Left;
            }
            else
            {
                if (timer < 0.7f)
                {
                    timer += Time.deltaTime;
                    followTime = timer * 3;
                }
            }

        }
        else
        {
            if (stance != CameraStance.Idle)
            {
                timer = 0;
                //cameraOffsetX = 0;
                additionalCameraOffset = new Vector3(0, 0, 0);
                followTime = 0;
                stance = CameraStance.Idle;

            }
            else
            {
                if (timer < 0.7f)
                {
                    timer += Time.deltaTime;
                    followTime = 1 + timer * 1.5f;
                }
            }

        }
        if(!inPuzzle)
        {
            if(hookObject == null)
            {
                //transform.position = Vector3.Slerp(transform.position, new Vector3(player.transform.position.x + cameraOffsetX, player.transform.position.y + cameraOffsetY, player.transform.position.z - 10f), followTime * Time.deltaTime);
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, cameraRot, followTime * Time.deltaTime);

                if(player.GetComponent<PlayerController>().levelType == PlayerController.LevelType.SS)
                {
                    cameraPos = new Vector3(
                    player.transform.position.x + (player.transform.forward.x * -distance),
                    player.transform.position.y + cameraOffsetY,
                    player.transform.position.z + (player.transform.forward.z * -distance));

                    transform.position = Vector3.Slerp(transform.position, cameraPos + additionalCameraOffset, followTime * Time.deltaTime);
                    transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, cameraRot.x, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.y, cameraRot.y, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.z, cameraRot.z, followTime * Time.deltaTime));
                }
                else
                {
                    transform.position = Vector3.Slerp(transform.position, new Vector3(player.transform.position.x + cameraOffsetX, player.transform.position.y + cameraOffsetY, player.transform.position.z -14f), followTime * Time.deltaTime);
                    transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, cameraRot, followTime * Time.deltaTime);
                }

            }
            else
            {
                transform.position = Vector3.Slerp(transform.position, hookObject.GetComponent<GrapplingHookScript>().cameraPos.position, followTime * Time.deltaTime);
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, hookObject.GetComponent<GrapplingHookScript>().cameraPos.eulerAngles, followTime * Time.deltaTime);
                transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, hookObject.GetComponent<GrapplingHookScript>().cameraPos.eulerAngles.x, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.y, hookObject.GetComponent<GrapplingHookScript>().cameraPos.eulerAngles.y, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.z, hookObject.GetComponent<GrapplingHookScript>().cameraPos.eulerAngles.z, followTime * Time.deltaTime));
            }
            
            
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + cameraOffsetX, player.transform.position.y + 20, player.transform.position.z), followTime * Time.deltaTime);
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(80, 0, 0), followTime * Time.deltaTime);
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, 80, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.y, 0, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.z, 0, followTime * Time.deltaTime));
           
        }
        
        //transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x + cameraOffsetX, player.transform.position.y + cameraOffsetY, player.transform.position.z - 20f), followTime * Time.deltaTime);

    }

    //Focus the camera on an object
    public void FollowObject(GameObject followThis)
    {
        followTime = 1.5f;
        if(playerController.levelType == PlayerController.LevelType.SS)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(followThis.transform.position.x, transform.position.y, cutsceneZ), followTime * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, followThis.transform.position, followTime * Time.deltaTime);
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, followThis.transform.eulerAngles, followTime * Time.deltaTime);
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, followThis.transform.eulerAngles.x, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.y, followThis.transform.eulerAngles.y, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.z, followThis.transform.eulerAngles.z, followTime * Time.deltaTime));
        }
        else
        {
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(30, 0, 0), followTime * Time.deltaTime);
            transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, 30, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.y, 0, followTime * Time.deltaTime), Mathf.LerpAngle(transform.eulerAngles.z, 0, followTime * Time.deltaTime));
            transform.position = Vector3.Lerp(transform.position, new Vector3(followThis.transform.position.x, followThis.transform.position.y + 5, followThis.transform.position.z -8f), followTime * Time.deltaTime);
        }      
    }
}
