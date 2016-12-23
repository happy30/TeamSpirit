// PlayerController by Jordi

using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody _rb;
    StatsManager stats;
    UIManager ui;
    GameObject playerModel;
    OptionsSettings optionsSettings;

    public Animator anim;

    //What Level
    public enum LevelType
    {
        SS,
        TD
    };
    public LevelType levelType;

    //Movement
    float speed;
    public float jumpHeight;
    public float xMovement;
    public float zMovement;
    public float modelWidth;
    public float modelHeight;
    bool inAir;
    public bool[] hasCollided;

    public float rotationOffset;

    public Vector3 walkTowards;
    public Vector3 lookPos;

    public bool onSlipperyTile;
    public bool onSlipperyTileNearWall;

    public int buttonCount;
    public KeyCode lastKey;
    public float doubleTapTime;
    public float dashSpeed;
    public float dashCooldown;
    public Material playerMat;

    public Vector3 playerRotation;

    //Combat
    public bool invulnerable;
    public float invulnerableTime;
    float invulnerableTimer;

    public bool invulnerableEffect;
    public AudioSource sound;
    public AudioClip smokeBombSound;

    public float jumpCD;

    /*
    float lastTapFwdTime = 0;  // the time of the last tap that occurred
    bool dblTapFwdReady = false;  // whether you you will execute a double-tap upon the next tap
    bool walkingRight = false;
    bool walkingLeft = false;
    bool walkingUp = false;
    bool walkingDown = false;
    bool dblTapbwdReady = false;
    float dblTapFwdTime  = .35f;
    */

    public bool hasTapped;
    public int tapCounter;
    public float tapTimer2;
    public float tapTimer;
    public float angle;
    public float lastDirection;
    public bool isMoving;
    bool nogeen;


    //Gather components
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        stats = GameObject.Find("GameManager").GetComponent<StatsManager>();
        optionsSettings = GameObject.Find("GameManager").GetComponent<OptionsSettings>();
        playerModel = GameObject.Find("PlayerModel");
        sound = GetComponent<AudioSource>();
    }

    //Set the right controls
    void Start()
    {
        ChangeControlsDependingOnLevelType();
    }
	
	void Update ()
    {
        if(!Camera.main.GetComponent<CameraController>().inCutscene)
        {
            SetMovement();
            Move();
            //CheckForDash();
            //CheckForDash2();
            CheckForDash3();
        }
        
        CheckForWall();
        CheckForSlipperyTile();
        CheckVulnerability();
    }


    public void ChangeControlsDependingOnLevelType()
    {
        if (levelType == LevelType.SS)
        {
            InputManager.Jump = InputManager.JumpSS;
        }
        else if (levelType == LevelType.TD)
        {
            InputManager.Jump = InputManager.JumpTD;
        }
    }

    //Change speed to runspeed if Shift is pressed
    void SetMovement()
    {
        
        if(Input.GetAxisRaw("RunTrigger") != 0)
        {
            speed = stats.runSpeed;
        }
        else
        {
            speed = Input.GetKey(KeyCode.LeftShift) ? stats.runSpeed : stats.walkSpeed;
        }
        if(!onSlipperyTile)
        {
            if(levelType == LevelType.SS)
            {
                xMovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
                if (xMovement > 0)
                {
                    playerRotation = new Vector3(0, 90 + rotationOffset, 0);
                    zMovement = 0;
                }
                if (xMovement < 0)
                {
                    playerRotation = new Vector3(0, 270 + rotationOffset, 0);
                    zMovement = 0;
                }
                playerModel.transform.eulerAngles = Vector3.Lerp(playerModel.transform.eulerAngles, playerRotation, 9f * Time.deltaTime);

                //playerModel.transform.eulerAngles = new Vector3(
                //    Mathf.LerpAngle(transform.eulerAngles.x, playerRotation.x, 9f * Time.deltaTime),
                //   Mathf.LerpAngle(transform.eulerAngles.y, playerRotation.y, 9f * Time.deltaTime),
                //    Mathf.LerpAngle(transform.eulerAngles.z, playerRotation.z, 9f * Time.deltaTime));
            }
            
            else if (levelType == LevelType.TD)
            {
                xMovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
                //zMovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
                if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
                {
                    walkTowards = transform.position;
                }
                else
                {
                    walkTowards = new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal"), transform.position.y, transform.position.z + Input.GetAxisRaw("Vertical"));
                }
                
            }
        }
        
    }


    void CheckForDash3()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float xhorizontal = Input.GetAxisRaw("Horizontal");
        float xvertical = Input.GetAxisRaw("Vertical");

        angle = Mathf.Atan2(xhorizontal, xvertical) * Mathf.Rad2Deg;

        if (dashCooldown <= 0)
        {
            if (vertical != 0 || horizontal != 0)
            {
                if (!isMoving && !hasTapped)
                {
                    lastDirection = angle;
                    isMoving = true;
                    hasTapped = true;
                    tapTimer = 0.3f;
                    nogeen = false;
                }
            }
            else
            {
                isMoving = false;
                nogeen = true;
            }

            if(hasTapped)
            {
                if(tapTimer > 0)
                {
                    tapTimer -= Time.deltaTime;
                    if(angle < lastDirection + 30 && angle > lastDirection - 30 && nogeen)
                    {
                        Dash(dashSpeed);
                        dashCooldown = 2f;
                        ui.UseSkill(4);
                        hasTapped = false;
                        tapTimer = 0;
                    }
                }
                else
                {
                    hasTapped = false;
                }
                
            }
            
        }
        else
        {
            if (dashCooldown > 0)
            {
                dashCooldown -= Time.deltaTime;
                tapTimer = 0;
                isMoving = true;
            }
        }
        
    }


        /*
        if(dashCooldown <= 0)
        {
            tapTimer += Time.deltaTime;
            if (hasTapped)
            {
                
                if (horizontal > 0.8f || vertical > 0.8f || horizontal < -0.8f || vertical < -0.8f)
                {
                    if (angle > lastDirection - 30f && angle < lastDirection + 30f)
                    {
                        if (tapTimer < 0.4f)
                        {
                            Dash(dashSpeed);
                            dashCooldown = 2f;
                            ui.UseSkill(4);
                            hasTapped = false;
                            tapTimer = 0;
                        }
                        else
                        {
                            tapTimer = 0;
                            hasTapped = false;
                        }

                    }
                    else
                    {
                        hasTapped = false;
                        tapTimer = 0;
                    }

                }
            }
            else
            {
                if (horizontal > 0.8f || vertical > 0.8f || horizontal < -0.8f || vertical < -0.8f)
                {
                    lastDirection = angle;
                    print(lastDirection);
                    isMoving = true;
 
                }
                else if(isMoving)
                {
                    hasTapped = true;
                    tapTimer = 0;
                }
            }
        }
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            dashCooldown = 0;
        }

    }

    void CheckForDash2()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Time.time > lastTapFwdTime + dblTapFwdTime)
        {
            dblTapFwdReady = false;
        }

        if (dashCooldown <= 0)
        {
            if (horizontal > 0)
            {
                if (!walkingRight)
                {
                    walkingRight = true;
                    lastTapFwdTime = Time.time;
                    if (dblTapFwdReady)
                    {
                        // Stop the other animations if necessary.
                        Dash(dashSpeed);
                        dashCooldown = 2f;
                        ui.UseSkill(4);
                    }
                    else
                    {
                        dblTapFwdReady = true;
                    }
                }
            }
            if (horizontal < 0)
            {
                if (!walkingLeft)
                {
                    walkingLeft = true;
                    lastTapFwdTime = Time.time;
                    if (dblTapFwdReady)
                    {
                        // Stop the other animations if necessary.
                        Dash(dashSpeed);
                        dashCooldown = 2f;
                        ui.UseSkill(4);
                    }
                    else
                    {
                        dblTapFwdReady = true;
                    }
                }
            }

            if (vertical > 0)
            {
                if (!walkingUp)
                {
                    walkingUp = true;
                    lastTapFwdTime = Time.time;
                    if (dblTapFwdReady)
                    {
                        // Stop the other animations if necessary.
                        Dash(dashSpeed);
                        dashCooldown = 2f;
                        ui.UseSkill(4);
                    }
                    else
                    {
                        dblTapFwdReady = true;
                    }
                }
            }
            if (vertical < 0)
            {
                if (!walkingUp)
                {
                    walkingDown = true;
                    lastTapFwdTime = Time.time;
                    if (dblTapFwdReady)
                    {
                        // Stop the other animations if necessary.
                        
                    }
                    else
                    {
                        dblTapFwdReady = true;
                    }
                }
            }


            if (horizontal == 0)
            {
            	walkingRight = false;
                walkingLeft = false;
            	// ^^ Idle animation Here
            }

            if (vertical == 0)
            {
                walkingUp = false;
                walkingDown = false;
                // ^^ Idle animation Here
            }

            if (walkingRight)
            {
            	// ^^ walk animation Here
            }
        }	
        else
        {
            	print ("lunging!");
        }
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            dashCooldown = 0;
        }
    }


 
   void CheckForDash()
   {
       if(levelType == LevelType.TD)
       {
           if (Input.GetKeyDown(KeyCode.UpArrow))
           {
               CheckForDoubleTap(KeyCode.UpArrow);
               lastKey = KeyCode.UpArrow;
           }

           if (Input.GetKeyDown(KeyCode.DownArrow))
           {
               CheckForDoubleTap(KeyCode.DownArrow);
               lastKey = KeyCode.DownArrow;
           }
       }

       if (Input.GetKeyDown(KeyCode.RightArrow))
       {
           CheckForDoubleTap(KeyCode.RightArrow);
           lastKey = KeyCode.RightArrow;
       }

       if (Input.GetKeyDown(KeyCode.LeftArrow))
       {
           CheckForDoubleTap(KeyCode.LeftArrow);
           lastKey = KeyCode.LeftArrow;
       }

       if (doubleTapTime < 0)
       {
           doubleTapTime = 0;
           buttonCount = 0;
       }
       else
       {
           doubleTapTime -= Time.deltaTime;
       }

       if(dashCooldown > 0)
       {
           dashCooldown -= Time.deltaTime;
       }
       else
       {
           dashCooldown = 0;
       }
   }





    void CheckForDoubleTap(KeyCode key)
{
    Debug.Log(key);
    if(buttonCount == 1)
    {
        if(lastKey == key)
        {
            Debug.Log("dash!!");
            if(dashCooldown <= 0)
            {
                Dash(dashSpeed);
                dashCooldown = 2f;
                ui.UseSkill(4);
            }
            buttonCount = 0;
        }
        else
        {
            buttonCount = 0;
        }
    }
    else
    {
        buttonCount++;
        doubleTapTime = 0.5f;
    }
}
*/

    void CheckForSlipperyTile()
    {
        if (IsTouching(20) != null)
        {
            if (IsTouching(20).tag == "Slippery")
            {
                onSlipperyTile = true;
            }
            else
            {
                if(IsTouching(2) != null)
                {
                    if (IsTouching(2).tag == "Ground")
                    {
                        onSlipperyTile = false;
                    }
                } 
            }
        }
        if(onSlipperyTile && !onSlipperyTileNearWall)
        {
            if (Mathf.Round(playerModel.transform.eulerAngles.y) == 90)
            {
                xMovement =  stats.runSpeed * 1.5f * Time.deltaTime;
            }
            else if (Mathf.Round(playerModel.transform.eulerAngles.y) == 270)
            {
                xMovement = -stats.runSpeed * 1.5f * Time.deltaTime;
            }
            else if (Mathf.Round(playerModel.transform.eulerAngles.y) == 0)
            {
                zMovement = stats.runSpeed * 1.5f * Time.deltaTime;
            }
            else if (Mathf.Round(playerModel.transform.eulerAngles.y) == 180)
            {
                zMovement = -stats.runSpeed * 1.5f * Time.deltaTime;
            }
        }
        if(onSlipperyTile && onSlipperyTileNearWall)
        {
            onSlipperyTile = false;
            if(xMovement != 0 || zMovement != 0)
            {
                onSlipperyTile = true;
                onSlipperyTileNearWall = false;
            }
        }

    }

    public void Dash(float distance)
    {
        _rb.velocity = new Vector3(playerModel.transform.forward.x * distance, 0, playerModel.transform.forward.z * distance);
    }
    public void Dash(float distance, float height)
    {
        _rb.velocity = playerModel.transform.forward * distance + new Vector3(0, height, 0);
    }

    //Move the player and let it jump
    void Move()
    {

        //ANIMATION
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (speed == stats.runSpeed)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Running", true);
            }
            else if (speed == stats.walkSpeed)
            {
                anim.SetBool("Running", false);
                anim.SetBool("Walking", true);
            }
        }
        else if(levelType == LevelType.SS)
        {
            anim.SetBool("Running", false);
            anim.SetBool("Walking", false);
        }

        //ANIMATION
        if (Input.GetAxisRaw("Vertical") != 0 && levelType == LevelType.TD)
        {
            if (speed == stats.runSpeed)
            {
                anim.SetBool("Walking", false);
                anim.SetBool("Running", true);
            }
            else if (speed == stats.walkSpeed)
            {
                anim.SetBool("Running", false);
                anim.SetBool("Walking", true);
            }
        }
        else if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0 && levelType == LevelType.TD)
        {
            anim.SetBool("Running", false);
            anim.SetBool("Walking", false);
        }




        if (!Camera.main.gameObject.GetComponent<CameraController>().inCutscene)
        {
            if(levelType == LevelType.SS)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x,  rotationOffset, transform.eulerAngles.z);
                transform.Translate(new Vector3(xMovement, 0, zMovement));
            }
            else
            {
                if(!CheckIfCollided())
                {
                    transform.position = Vector3.MoveTowards(transform.position, walkTowards, speed * Time.deltaTime);
                }
                
                if(Input.GetAxis("Horizontal") > 0.05f || Input.GetAxis("Horizontal") < -0.05f || Input.GetAxis("Vertical") > 0.05 || Input.GetAxis("Vertical") < -0.05)
                {
                    lookPos = walkTowards - transform.position + playerModel.transform.forward * 0.05f;
                }
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, rotation, 9f * Time.deltaTime);
            }
            
            if(!CheckIfJumping())
            {
                anim.SetBool("Jump", false);
            }

            if(jumpCD <= 0)
            {
                if (Input.GetKey(InputManager.Jump) || Input.GetKey(InputManager.JJump) || Input.GetKey(InputManager.JJumpTD))
                {
                    //Check if player is standing on Ground
                    if (IsTouching(2) != null)
                    {
                        if (IsTouching(2).tag == "Ground")
                        {
                            Debug.Log("jump");
                            if (!CheckIfJumping() && !inAir)
                            {
                                Jump();
                            }
                        }
                    }
                }
            }
            else
            {
                jumpCD -= Time.deltaTime;
            }
            
        }
    }

    //Make the player stop moving if it's humping a wall
    void CheckForWall()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - modelHeight, transform.position.z), -transform.right);

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - modelHeight, transform.position.z), -transform.right, out hit, modelWidth) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y + (modelHeight * 1.7f), transform.position.z), -transform.right, out hit, modelWidth))
        {
            if (xMovement < 0)
            {
                xMovement = 0;
            }
        }
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - modelHeight, transform.position.z), transform.right, out hit, modelWidth) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y + (modelHeight * 1.7f), transform.position.z), transform.right, out hit, modelWidth))
        {
            if (xMovement > 0)
            {
                xMovement = 0;
            }
        }
        if(levelType == LevelType.TD)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - modelHeight, transform.position.z), transform.forward, out hit, modelWidth) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y + (modelHeight * 1.7f), transform.position.z), transform.forward, out hit, modelWidth))
            {
                if (zMovement > 0)
                {
                    zMovement = 0;
                }
            }
        }
    }

    //Check what object is beneath the player
    public GameObject IsTouching(int range)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, range))
        {
            return hit.collider.gameObject;
        }
        else
        {
            inAir = true;
        }
        return null;
    }

    //Throw the player in the air
    public void Jump()
    {
        _rb.velocity = new Vector3(0, jumpHeight, 0);
        anim.SetBool("Jump", true);
        jumpCD = 0.7f;
    }

    public bool CheckIfJumping()
    {
        if(_rb.velocity.y > jumpHeight -0.1)
        {
            return true;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1.1f))

        {
            if(hit.collider.tag == "Ground")
            {
                inAir = false;
            }
        }
        return false;
    }

    void CheckVulnerability()
    {
        if(invulnerable)
        {
            playerMat.color = Color.black;
            Camera.main.GetComponent<Grayscale>().enabled = true;
            if (!invulnerableEffect)
            {
                sound.PlayOneShot(smokeBombSound);
                Camera.main.GetComponent<Grayscale>().rampOffset = 1;
                invulnerableEffect = true;
            }
            if(Camera.main.GetComponent<Grayscale>().rampOffset > 0)
            {
                Camera.main.GetComponent<Grayscale>().rampOffset -= .5f * Time.deltaTime;
            }
            invulnerableTimer += Time.deltaTime;
            if(invulnerableTimer > invulnerableTime)
            {
                invulnerable = false;
                invulnerableTimer = 0;
            }
        }
        else
        {
            Camera.main.GetComponent<Grayscale>().enabled = false;
            invulnerableEffect = false;
            playerMat.color = Color.white;
        }
    }

    public void GetHit(int damage)
    {
        if(!invulnerable)
        {
            stats.health -= damage;
            GameObject.Find("Canvas").GetComponent<HeartScript>().DrawHearts();
            Dash(-10f, 4);
        }
    }

    public void GetInvulnerable()
    {
        invulnerable = true;
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Interact")
        {
            if(col.gameObject.GetComponent<InteractScript>().interactType == InteractScript.InteractType.OnTrigger)
            {
                if(col.gameObject.GetComponent<InteractScript>().isHint)
                {
                    if (optionsSettings.displayHints)
                    {
                        col.gameObject.GetComponent<InteractScript>().Activate();
                    }
                }
                else
                {
                    col.gameObject.GetComponent<InteractScript>().Activate();
                }
                
            }
            else if(col.gameObject.GetComponent<InteractScript>().interactType == InteractScript.InteractType.OnInput)
            {
                ui.ChangeInteractText(col.gameObject.GetComponent<InteractScript>());
                if(Input.GetKey(InputManager.Slash))
                {
                    col.gameObject.GetComponent<InteractScript>().Activate();
                }
                if(col.gameObject.GetComponent<InteractScript>().linkedObject.GetComponent<Activate>().activated)
                {
                    ui.RemoveInteractText();
                }
            }
            
        }
    }

    bool CheckIfCollided()
    {
        for (int i = 0; i < hasCollided.Length; i++)
        {
            if (hasCollided[i])
            {
                return true;
            }
        }
        return false;
                
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Ground")
        {
            inAir = false;
        }
    }

    void OnTriggerExit()
    {
        ui.RemoveInteractText();
    }
}
