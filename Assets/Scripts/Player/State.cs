using UnityEngine;

//State Abstract class for the states the player uses


public abstract class State<T> : ScriptableObject where T : MonoBehaviour
{
    protected T _runner;
    private RaycastHit slopeHit;

    //references needed
    [Header("References")]
    public Transform playerTransform;
    public Rigidbody rb;
    public dahye.GroundCheck _groundCheck;
    public Transform cam;

    //movement
    [Header("Movement")]
    public float turnSmoothTime = 0.1f; //speed the character turns to desired angle of movement
    float turnSmoothVelocity; //holds rotation smooth velocity
    private float horizontalInput;
    private float verticalInput;

    public virtual void Init(T parent) //anything rleated to initializing the state
    {
        _runner = parent;
        playerTransform = parent.GetComponent<Rigidbody>().transform;
        rb = parent.GetComponent<Rigidbody>();
        _groundCheck = parent.GetComponent<dahye.GroundCheck>();
        _groundCheck = parent.GetComponent<dahye.GroundCheck>();
        cam = parent.GetComponentInChildren<Camera>().transform;
    }

    public abstract void CaptureInput(); //get the player input

    public abstract void Update(); //called during the gameObjects update

    public abstract void FixedUpdate(); //called during the gameObjects Fixedupdate

    public abstract void ChangeState(); //method where all code related to changing states from one to another

    public abstract void Exit(); //action when state is finishing. I.e: water splash when exiting swimming

    //functions the states can use

    public Vector3 calcualteMoveDirection()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        //the player can only move if CanMove is true, gravity still affects the player.
        if (direction.magnitude >= 0.1f)
        {
            //move player to desired angle over turnSmoothTime amount of time
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(playerTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            playerTransform.rotation = Quaternion.Euler(0f, angle, 0f);


            //finds move direction
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            return new Vector3(0f, 0f, 0f);
        }


    }

    public void MovePlayer(Vector3 moveDirection, float moveSpeed, float airMultiplier = 0.4f, float groundMultiplier = 1f)
    {
        // on slope
        if (OnSlope(playerTransform) && _groundCheck.Check()) //has !exitingslope, change if this doesnt work
        {

            rb.AddForce(getSlopeMoveDirection(moveDirection) * moveSpeed * 20, ForceMode.Force);
            Debug.Log("slope movement");
            //maybe check if player is moving
            if (rb.velocity.y > 0 || calcualteMoveDirection().magnitude == 0)
            {

                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }


        }
        else if (_groundCheck.Check()) //on ground
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * groundMultiplier, ForceMode.Force);
            Debug.Log("ground movement");
        }
        else
        { //in air

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            Debug.Log("air movement");
        }

    }

    public bool OnSlope(Transform playerTransform)
    {

        float maxSlopeAngle = 60; //maxslope angle, change here


        //raycast testing
        Vector3 test = playerTransform.TransformDirection(Vector3.down) * 0.1f;
        Debug.DrawRay(playerTransform.position, test, Color.green, 0.1f);



        if (Physics.Raycast(playerTransform.position, Vector3.down, out slopeHit, 0.1f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            // Debug.Log("On Slope?: " + angle + " " + (angle < maxSlopeAngle && angle != 0));

            return angle < maxSlopeAngle && angle != 0;
        }

        return false;


    }

    public Vector3 getSlopeMoveDirection(Vector3 direction)
    {


        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }


    public void SpeedControl(float maxSpeed)
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


        //limits the velocity if needed
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

        }

        //for slope
        if (OnSlope(playerTransform))
        {

            //  if (rb.velocity.magnitude > maxSpeed)

            //     rb.velocity = rb.velocity.normalized * 8;



        }
    }


}
