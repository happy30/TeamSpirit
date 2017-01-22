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
    Vector3 moveDirection;
    bool hasInput;

    public bool onSlipperyTile;
    public bool onSlipperyTileNearWall;

    public int buttonCount;
    public KeyCode lastKey;
    public float doubleTapTime;
    public float dashSpeed;
    public float dashCooldown;
    public Material playerMat;

    public Vector3 playerRotation;
    public float movementTimer;
    public float movementTime;
    public bool canMove;

    //Combat
    public bool invulnerable;
    public float invulnerableTime;
    float invulnerableTimer;

    public bool invulnerableEffect;
    public AudioSource sound;
    public AudioClip smokeBombSound;
    public AudioClip stepDirt;
    public AudioClip stepWood;
    public AudioClip playerGetHit;
    public AudioClip playerJump;

    public float jumpCD;

    public GameObject DashParticle;
    GameObject spawnedDashParticle;

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

    public Transform leftFoot;
    public Transform rightFoot;
    public GameObject footStepParticle;
    GameObject spawnedFootStepParticle;

    public GameObject jumpLandParticle;
    GameObject spawnedjumpLandParticle;


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
        canMove = true;
    }
	
	void Update ()
    {
        CanMove();
        if(!Camera.main.GetComponent<CameraController>().inCutscene && !onSlipperyTile && canMove)
        {
            SetMovement();
            Move();
            CheckForDash3();
        }
        else if(Camera.main.GetComponent<CameraController>().inCutscene)
        {
            anim.SetBool("Running", false);
            anim.SetBool("Walking", false);
        }
        else if(onSlipperyTile)
        {
            SlipperyTileMovement();
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


    void CanMove()
    {
        if(!canMove)
        {
            movementTimer += Time.deltaTime;
            if(movementTimer > movementTime)
            {
                canMove = true;
                movementTimer = 0;
                movementTime = 0;
            }
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

    // 3rd attempt in a functional dash for gamepad
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
                        if(angle == 0)
                        {
                            if(xvertical != 0)
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
                            Dash(dashSpeed);
                            dashCooldown = 2f;
                            ui.UseSkill(4);
                            hasTapped = false;
                            tapTimer = 0;
                        }
                        
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

    void CheckForSlipperyTile()
    {
        if (IsTouching(5) != null)
        {
            if (IsTouching(20).tag == "Slippery")
            {
                onSlipperyTile = true;
            }
        }
    }

    void SlipperyTileMovement()
    {
        anim.SetBool("Walking", false);
        anim.SetBool("Runing", false);


        if(Input.GetAxisRaw("Horizontal") > 0 && !hasInput)
        {
            moveDirection = (transform.right * stats.runSpeed);
            playerModel.transform.eulerAngles = new Vector3(0, 90, 0);
            hasInput = true;
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && !hasInput)
        {
            moveDirection = (transform.right * -stats.runSpeed);
            playerModel.transform.eulerAngles = new Vector3(0, 270, 0);
            hasInput = true;
        }
        if (Input.GetAxisRaw("Vertical") > 0 && !hasInput)
        {
            moveDirection = (transform.forward * stats.runSpeed);
            playerModel.transform.eulerAngles = new Vector3(0, 0, 0);
            hasInput = true;
        }
        if (Input.GetAxisRaw("Vertical") < 0 && !hasInput)
        {
            moveDirection = (transform.forward * -stats.runSpeed);
            playerModel.transform.eulerAngles = new Vector3(0, 180, 0);
            hasInput = true;
        }



        if (Physics.Raycast(playerModel.transform.position, playerModel.transform.forward, 1.5f))
        {
            moveDirection = new Vector3(0, 0, 0);
            hasInput = false;
        }

        transform.Translate(moveDirection * 1.3f * Time.deltaTime);

    }


    public void Dash(float distance)
    {
        spawnedDashParticle = (GameObject)Instantiate(DashParticle, transform.position, Quaternion.identity);
        spawnedDashParticle.transform.SetParent(transform);
        Destroy(spawnedDashParticle, 0.3f);
        _rb.velocity = new Vector3(playerModel.transform.forward.x * distance, 0, playerModel.transform.forward.z * distance);
    }
    public void Dash(float distance, float height)
    {
        _rb.velocity = new Vector3(distance, height, 0);
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
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + modelHeight, transform.position.z), -transform.right);

        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + modelHeight, transform.position.z), -transform.right, out hit, modelWidth) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y + (modelHeight * 1.7f), transform.position.z), -transform.right, out hit, modelWidth))
        {
            if (xMovement < 0)
            {
                print("hitsomething");
                xMovement = 0;
            }
        }
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + modelHeight, transform.position.z), transform.right, out hit, modelWidth) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y + (modelHeight * 1.7f), transform.position.z), transform.right, out hit, modelWidth))
        {
            if (xMovement > 0)
            {
                print("hitsomething");
                xMovement = 0;
            }
        }
        if(levelType == LevelType.TD)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + modelHeight, transform.position.z), transform.forward, out hit, modelWidth) || Physics.Raycast(new Vector3(transform.position.x, transform.position.y + (modelHeight * 1.7f), transform.position.z), transform.forward, out hit, modelWidth))
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
        anim.SetBool("Walking", false);
        jumpCD = 0.85f;
        sound.PlayOneShot(playerJump);
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
                if(inAir)
                {
                    inAir = false;
                }
               
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

    //get hit by enemies
    public void GetHit(int damage, Transform hitter)
    {
        if(!invulnerable)
        {
            sound.PlayOneShot(playerGetHit);
            stats.health -= damage;
            GameObject.Find("Canvas").GetComponent<HeartScript>().DrawHearts();
            if(hitter.position.x > transform.position.x)
            {
                Dash(-10f, 4);
            }
            else
            {
                Dash(10f, 4);
            }
            
            ui.BloodOverlayEffect();
        }
    }

    public void GetInvulnerable()
    {
        invulnerable = true;
    }

    public void Die()
    {
        //restart to checkpoint;
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


    public void Step(bool left)
    {
        sound.pitch = Random.Range(0.5f, 1.5f);

        if(IsTouching(2) != null)
        {
            if(IsTouching(2).name == "Terrain")
            {
                sound.PlayOneShot(stepDirt, 0.3f);
                if (left)
                {
                    spawnedFootStepParticle = (GameObject)Instantiate(footStepParticle, leftFoot.position, Quaternion.identity);
                    Destroy(spawnedFootStepParticle, 1f);
                }
                else
                {
                    spawnedFootStepParticle = (GameObject)Instantiate(footStepParticle, rightFoot.position, Quaternion.identity);
                    Destroy(spawnedFootStepParticle, 1f);
                }
            }
            else
            {
                sound.PlayOneShot(stepWood);
            }
        }
        sound.pitch = 1;
    }

    public void Land()
    {
        Step(true);
        spawnedjumpLandParticle = (GameObject)Instantiate(jumpLandParticle, rightFoot.position, Quaternion.identity);
        Destroy(spawnedjumpLandParticle, 1f);
    }

    public void StopMovement (float time)
    {
        canMove = false;
        movementTime = time;
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

