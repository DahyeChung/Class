using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public static ThirdPersonMovement instance;



    [Header("PlayerState")] //player state machine
    public PlayerState currentState;

    public enum PlayerState
    {
        idle,
        walking,
        sprinting,
        crouching,
        crouchwalk,
        jumping,
        grappling,
        dashing,
        attacking

    }

    [Header("Character Controller and animator")]
    public CharacterController controller; //player character controller
    public Animator playerAnimator;

    [Header("Camera")]
    public Transform cam; //refernces camera. Used to move in regards to camera direction


    [Header("Movement")]
    public bool canMove = true;
    public float timeUntilMove;
    public float horizontalInput;
    public float verticalInput;

    [SerializeField] private float moveSpeed = 12f; //movement speed used at any given time
    public float walkspeed = 12f;  //movement speed for walking
    public float crouchSpeed = 8f; //movement speed for crouching
    public float SprintSpeed = 4f; //movement speed for sprinting
    public float jumpHeight = 3f; // Jump height
    public float turnSmoothTime = 0.1f; //speed the character turns to desired angle of movement
    float turnSmoothVelocity; //holds rotation smooth velocity

    [Header("Ground Checks")]
    public Transform groundCheck;
    public float groundDistance; //radius of sphere we use to check
    public LayerMask groundMask;
    bool isGrounded;

    [Header("Gravity")]
    public float gravity = -9.81f; //the gravity enacted on the player

    Vector3 velocity;  //stores the velocity of the player

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;
    public KeyCode grappleKey = KeyCode.F;
    public KeyCode dashKey = KeyCode.LeftControl;

    [Header("Grappling")] //variables making up the grapple function
    public Grapple grapple;

    [Header("Sprinting")] //variables making up the spritning function
    public float accelerateTime; //float for how long it takes to reach max acceleration for a sprint
    public float decelerateTime; //float for how long it takes to decelerate from a sprint
    public float sprintMultiplier; //float for how long it takes to speed up to sprint
    public float decelerationMultiplier; //float for how long it takes to slow down 

    [Header("Dash")] //variables making up the dash function
    public bool readyToDash = false; //boolean if the player is able to dash
    public int Maxcharges = 3; //maximum charges for dash
    public int dashCharges = 3; //the amount of charages the player has to be able to dash
    public float dashChargeRate = 5f; //the rate dash charges refresh
    public float dashChargeTime = 5f;
    private bool hasDashedInAir = false;
    public float dashCooldown = 1f;
    private float timeDashed = 0f; //private float used to see when the last time we dashed was
    public float dashTime = 0.5f;
    public float dashSpeed = 10;








    private void Awake()
    {
        instance = this;

        grapple = this.GetComponent<Grapple>();

        //cooldown reset

        dashCharges = Maxcharges;

    }
    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        playerAnimator.SetBool("Grounded", isGrounded);


        //moevment. Might need to update with new input system if we switch to that

        horizontalInput = Input.GetAxisRaw("Horizontal");

        verticalInput = Input.GetAxisRaw("Vertical");







        playerStateChange(); //change player state 

        //COOLDOWNS

        DashLogic();

        //  Debug.Log(timeDashed);


    }

    private void FixedUpdate()
    {



        MovePlayer();


    }

    void MovePlayer()
    {


        if (isGrounded) //makes  hasDashedInAir false if the player is grounded
        {

            hasDashedInAir = false;

            if (velocity.y < 0) //resets the y velocity to -2 if gorunded, makes it so player doesnt keep gaining downward momentum
            {
                velocity.y = -2;
            }
        }
        //normalized so we dont get faster diagonal movement
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        //the player can only move if CanMove is true, gravity still affects the player.
        if (direction.magnitude >= 0.1f && canMove == true)
        {
            //move player to desired angle over turnSmoothTime amount of time
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            //finds move direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;



            //moves player. Time.deltaTime makes this frame-rate independent


            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);


            // rb.velocity = moveDir * moveSpeeed; RB

        }

        if (canMove == true)
        {
            velocity.y += gravity * Time.deltaTime; //add gravity

            //use move again for gravity

            controller.Move(velocity * Time.deltaTime);
            //rb.velocity = velocity * Time.deltaTime; RB
        }



    }

    void playerStateChange()
    {
        switch (currentState)
        {
            case PlayerState.idle: //idle means no input is given to the player and they are grounded (this does not include attacking)

                playerAnimator.SetBool("IsRunning", false);
                playerAnimator.SetBool("IsWalking", false);

                if ((verticalInput != 0 || horizontalInput != 0) && isGrounded) //idle to walking
                {

                    currentState = PlayerState.walking;

                }
                if ((verticalInput != 0 || horizontalInput != 0) && isGrounded && Input.GetKeyDown(sprintKey)) //if becuase sprinting neds to be checked after
                {

                    currentState = PlayerState.sprinting;
                }
                else if (Input.GetKeyDown(crouchKey) && isGrounded) //idle to crouch/crouchwalk
                {
                    if ((verticalInput != 0 || horizontalInput != 0)) //if player is moving when the crouch button is pressed
                    {
                        currentState = PlayerState.crouchwalk;

                    }
                    else
                    { //if player is not moving when the crouch button is pressed

                        currentState = PlayerState.crouching;

                    }


                    Crouch();
                }
                else if (Input.GetKeyDown(jumpKey) && isGrounded) //idle to jumping
                {
                    currentState = PlayerState.jumping;

                    Jump();
                }
                else if (Input.GetKeyDown(grappleKey) && grapple.closest != null) //Idle to Grappling
                {
                    BeginGrapple();
                    currentState = PlayerState.grappling;
                }
                else if (Input.GetKeyDown(dashKey) && readyToDash) //Idle to dashing
                {
                    StartCoroutine(DashCoroutine());
                    currentState = PlayerState.dashing;
                }
                else if (!isGrounded) //idle to falling (jumping)
                {
                    currentState = PlayerState.jumping;

                    playerAnimator.SetTrigger("Fall");

                  playerAnimator.SetTrigger("Fall");

                }
                break;

            case PlayerState.walking:

                playerAnimator.SetBool("IsWalking", true);
                playerAnimator.SetBool("IsRunning", false);

                if ((verticalInput == 0 && horizontalInput == 0) && isGrounded) //walking to idle
                {
                    currentState = PlayerState.idle;
                }
                if (Input.GetKeyDown(sprintKey)) //walking to sprinting, if becuase sprinting neds to be checked after
                {
                    currentState = PlayerState.sprinting;
                }
                else if (Input.GetKeyDown(crouchKey) && (verticalInput == 0 && horizontalInput == 0)) //walking to crouching (this will likely never happen but just covering the bases)
                {
                    currentState = PlayerState.crouching;

                    Crouch();
                }
                else if (Input.GetKeyDown(crouchKey) && (verticalInput != 0 || horizontalInput != 0)) //walking to crouchwalk
                {
                    currentState = PlayerState.crouchwalk;

                    Crouch();
                }
                else if (Input.GetKeyDown(jumpKey) && isGrounded) //walking to jumping
                {
                    currentState = PlayerState.jumping;

                    Jump();
                }
                else if (Input.GetKeyDown(grappleKey) && grapple.closest != null) //walking to Grappling
                {
                    BeginGrapple();
                    currentState = PlayerState.grappling;
                }
                else if (Input.GetKeyDown(dashKey) && readyToDash) //walking to dashing
                {
                    StartCoroutine(DashCoroutine());
                    currentState = PlayerState.dashing;
                }
                else if (!isGrounded) //walking to falling (jumping)
                {
                    currentState = PlayerState.jumping;
                    playerAnimator.SetTrigger("Fall");
                }

                break;

            case PlayerState.sprinting:

                playerAnimator.SetBool("IsRunning", true);
                playerAnimator.SetBool("IsWalking", false);


                if (Input.GetKey(sprintKey)) //if sprint key is held down, movement speed is increased until sprintspeed is reached
                {
                    if (moveSpeed < SprintSpeed)
                    {
                        moveSpeed += sprintMultiplier * Time.deltaTime; // Cap at some max value too
                    }
                    else
                    {
                        moveSpeed = SprintSpeed;
                    }


                }
                else //if sprint key is not held, then start decelerating the player, if they reach walkspeed they start walking
                {

                    if (moveSpeed > walkspeed)
                    {
                        moveSpeed -= decelerationMultiplier * Time.deltaTime;
                    }
                    else
                    {
                        moveSpeed = walkspeed;
                        currentState = PlayerState.walking;
                    }

                }


                //temp changes for jumping, polish later
                if (Input.GetKeyDown(jumpKey) && isGrounded) //sprinting to jumping
                {
                    currentState = PlayerState.jumping;
                    moveSpeed = walkspeed;
                    Jump();
                }

                if (!isGrounded) //sprintingto falling (jumping)
                {
                    moveSpeed = walkspeed;
                    currentState = PlayerState.jumping;
                    playerAnimator.SetTrigger("Fall");
                }


                /*
                float timestarted = Time.time;


                float t = 0;
                moveSpeed = Mathf.Lerp(walkspeed, SprintSpeed, t);

                 t += Time.deltaTime + accelerateTime; 
               
                /*
                //if sprint is not held anymore and acceleration time is reached


                /*
                if ((verticalInput == 0 && horizontalInput == 0) && isGrounded) //walking to idle
                {
                    currentState = PlayerState.idle;
                }
                else if (Input.GetKeyDown(crouchKey) && (verticalInput == 0 && horizontalInput == 0)) //walking to crouching (this will likely never happen but just covering the bases)
                {
                    currentState = PlayerState.crouching;

                    Crouch();
                }
                else if (Input.GetKeyDown(crouchKey) && (verticalInput != 0 || horizontalInput != 0)) //walking to crouchwalk
                {
                    currentState = PlayerState.crouchwalk;

                    Crouch();
                }
                else if (Input.GetKeyDown(jumpKey) && isGrounded) //walking to jumping
                {
                    currentState = PlayerState.jumping;

                    Jump();
                }
                else if (Input.GetKeyDown(grappleKey) && grapple.closest != null) //walking to Grappling
                {
                    BeginGrapple();
                    currentState = PlayerState.grappling;
                }
                else if (Input.GetKeyDown(dashKey)) //walking to dashing
                {
                    StartCoroutine(DashCoroutine());
                    currentState = PlayerState.dashing;
                }
                else if (!isGrounded) //walking to falling (jumping)
                {
                    currentState = PlayerState.jumping;
                }
                */
                break;

            case PlayerState.crouching:

                if (Input.GetKeyDown(crouchKey) && (verticalInput == 0 && horizontalInput == 0)) //crouch to idle
                {
                    currentState = PlayerState.idle;

                    EndCrouch(); //end the crouch state
                }
                else if (Input.GetKeyDown(crouchKey) && (verticalInput != 0 || horizontalInput != 0)) //crouch to walking
                {
                    currentState = PlayerState.walking;

                    EndCrouch(); //end the crouch state
                }
                else if ((verticalInput != 0 || horizontalInput != 0) && isGrounded) //crouch to crouchwalk
                {
                    currentState = PlayerState.crouchwalk;

                }
                else if (Input.GetKeyDown(jumpKey) && isGrounded) //crouch to jump
                {
                    currentState = PlayerState.jumping;

                    EndCrouch(); //end the crouch state

                    Jump(); //cause the player to jump
                }
                else if (Input.GetKeyDown(grappleKey) && grapple.closest != null) //crouch to Grappling
                {
                    BeginGrapple();
                    currentState = PlayerState.grappling;
                }
                else if (Input.GetKeyDown(dashKey) && readyToDash) //crouch to dashing
                {
                    StartCoroutine(DashCoroutine());
                    currentState = PlayerState.dashing;
                }
                else if (!isGrounded) //crouch to falling (jumping)
                {
                    currentState = PlayerState.jumping;
                    playerAnimator.SetTrigger("Fall");
                    EndCrouch(); //end the crouch state
                }
                break;

            case PlayerState.crouchwalk:

                if (Input.GetKeyDown(crouchKey) && (verticalInput == 0 && horizontalInput == 0)) //crouchwalk to idle (this will likely never happen)
                {
                    currentState = PlayerState.idle;

                    EndCrouch(); //end the crouch state
                }
                else if (Input.GetKeyDown(crouchKey) && (verticalInput != 0 || horizontalInput != 0)) //crouchwalk to walking
                {
                    currentState = PlayerState.walking;

                    EndCrouch(); //end the crouch state
                }
                else if ((verticalInput == 0 && horizontalInput == 0) && isGrounded) // crouchwalk to crouch
                {
                    currentState = PlayerState.crouching;
                }
                else if (Input.GetKeyDown(jumpKey) && isGrounded) //crouchwalk to jump
                {
                    currentState = PlayerState.jumping;

                    EndCrouch(); //end the crouch state

                    Jump(); //cause the player to jump
                }
                else if (Input.GetKeyDown(grappleKey) && grapple.closest != null) //crouchwalk to Grappling
                {
                    BeginGrapple();
                    currentState = PlayerState.grappling;
                }
                else if (Input.GetKeyDown(dashKey) && readyToDash) //crouchwalk to dashing
                {
                    StartCoroutine(DashCoroutine());
                    currentState = PlayerState.dashing;
                }
                else if (!isGrounded) //crouchwalk to falling (jumping)
                {
                    currentState = PlayerState.jumping;
                    playerAnimator.SetTrigger("Fall");
                    EndCrouch(); //end the crouch state
                }

                break;

            case PlayerState.jumping:

                if ((verticalInput == 0 && horizontalInput == 0) && isGrounded) //jumping to idle
                {
                    currentState = PlayerState.idle;
                }
                else if ((verticalInput != 0 || horizontalInput != 0) && isGrounded) //jumping to walking
                {
                    currentState = PlayerState.walking;
                }
                else if (Input.GetKey(crouchKey) && (verticalInput == 0 && horizontalInput == 0) && isGrounded)//jumping to crouching
                {
                    currentState = PlayerState.crouching;

                    Crouch();
                }
                else if (Input.GetKeyDown(grappleKey) && grapple.closest != null) //jumping to Grappling
                {
                    BeginGrapple();
                    currentState = PlayerState.grappling;
                }
                else if (Input.GetKeyDown(dashKey) && readyToDash) //jumping to dashing
                {
                    hasDashedInAir = true;
                    StartCoroutine(DashCoroutine());
                    currentState = PlayerState.dashing;
                }
                else if (Input.GetKey(crouchKey) && (verticalInput != 0 || horizontalInput != 0) && isGrounded) //jumping to crouchwalk
                {
                    currentState = PlayerState.crouchwalk;

                    Crouch();
                }

                break;

            case PlayerState.grappling:

                //in grappling, we lead back to idle when the grappling is finished

                if (grapple.grappling == false)
                {
                    currentState = PlayerState.idle;
                }


                break;

            case PlayerState.dashing:





                break;

        }

    }


    //movement functions

    public void Jump() //cuases the players y velocity to be influenced by the jump height
    {
        playerAnimator.SetTrigger("Jump");
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void Crouch() // the crouch state
    {

        moveSpeed = crouchSpeed; //makes the player movement speed the crouch speed

        //lower the hitbox of the player

        //the player will make less noise?

    }


    public void EndCrouch() // the crouch state
    {
        moveSpeed = walkspeed;

        //raise the hitbox of the player

        //the player will make normal noise
    }

    public void Sprint() //the sprint state
    {
        moveSpeed = SprintSpeed;



        //the player makes more noise


    }

    public void EndSprint() //the sprint state
    {
        moveSpeed = walkspeed;



        //the player makes more noise


    }
    //called to set up the player to grapple
    public void BeginGrapple()
    {

        //set y velocity to 0

        velocity.y = 0;

        //stop player movement

        stopMovement();

        //put the player in the grappling state


        //make grapple start doing it thing

        grapple.grappling = true;

        //everything from here is done on grapples end
    }

    private IEnumerator DashCoroutine()
    {
        //dash charges put down

        dashCharges--;

        //set dashTime to dashcooldown

        timeDashed = dashCooldown;
        //set y velocity to 0

        velocity.y = 0;

        //stop player movement

        stopMovement();



        //stores direction of the dash  

        //if they arent pressing anything, make the dodge direction backwards, like a backflip.

        //DASH DIRECTION WILL BE CHANGED TO PLAYER INPUT AND NOT THE TRANSFORM??? MAYBE? player turns slow so amyeb i can find a solution that isnt that

        Vector3 dashDirection;
        if ((verticalInput == 0 && horizontalInput == 0))
        {
            dashDirection = -transform.forward;
        }
        else
        {
            dashDirection = transform.forward;
        }




        float dashStartTime = Time.time; // need to remember this to know how long to dash
        while (Time.time < dashStartTime + dashTime)
        {
            //transform.Translate(transform.forward * dashSpeed * Time.deltaTime);
            // or controller.Move(...), dunno about that script
            controller.Move(dashDirection.normalized * dashSpeed * Time.deltaTime);
            yield return null; // this will make Unity stop here and continue next frame
        }


        startMovement();
        //sent to jumping state, might make mroe direct connections in the future


        currentState = PlayerState.jumping;
    }




    //functions that start movement of the player or stop them

    //stops the player from moving for a certain period of time, does not stop gravity or camera movement
    IEnumerator stopMovingTime(float seconds)
    {

        canMove = false;
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }


    //stops the players from moving, primarily used in other fucntions
    public void stopMovement()
    {
        canMove = false;
    }

    //lets the player move, primarily used in other fucntions
    public void startMovement()
    {
        canMove = true;
    }

    //Cooldown functions


    public void DashLogic() //function to control when the player can dash, if they can, and managing cooldowns associated.
    {

        //First part, cooldown logic for dash and charge refresh

        //if dashTime is greater than 0, decrease it 

        if (timeDashed > 0)
        {
            timeDashed -= 1f * Time.deltaTime;
        }


        //charge refresh

        if (dashCharges < Maxcharges)
        {
            //if oplayer is not in dashing state, recharge
            if (currentState != PlayerState.dashing)
            {
                dashChargeTime -= 1f * Time.deltaTime;
            }



            if (dashChargeTime <= 0) // if you recharge all the way, you get a new charge
            {
                dashCharges++;

                dashChargeTime = dashChargeRate;
            }


        }
        else
        {
            dashChargeTime = dashChargeRate;
        }






        //if hasdashedinair is false
        //if cooldown is 0?
        //if they have charges





        //second part, see if the player can actually dash

        if ((hasDashedInAir == false) && (timeDashed <= 0) && (dashCharges > 0))
        {

            readyToDash = true;
        }
        else
        {
            readyToDash = false;
        }
    }


    public void checkSpeed()
    {

        //decrease movement speed if the player is in sprintspeed but not in that state

        //if player is not sprinting and has sprint speed
        if (moveSpeed > walkspeed && currentState != PlayerState.sprinting && currentState != PlayerState.crouching)
        {
            moveSpeed -= decelerationMultiplier * Time.deltaTime;
        }
        else
        {
            moveSpeed = walkspeed;

        }


        //check to make sure sprint speed goes down if it needs to 
        if (moveSpeed > walkspeed && currentState != PlayerState.sprinting)
        {
            moveSpeed -= decelerationMultiplier * Time.deltaTime;
        }

    }

}
