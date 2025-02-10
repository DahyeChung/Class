using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Jump")]

public class JumpState : State<PlayerController>
{
    //keycodes
    [Header("Keycodes")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;
    public KeyCode grappleKey = KeyCode.F;
    public KeyCode dashKey = KeyCode.LeftControl;

    [Header("Jump state fields")]
    private dahye.GroundCheck groundCheck;
    private Rigidbody _rigidbody;
    private bool _leftTheGround; //when the player was standing on the ground then became in the air

    [SerializeField]
    private float _jumpVelocity; //initial jump velocity
    private float jumpTime;


    //FIELDS EVERY STATE NEEDS
    [Header("Movement")]
    private Vector3 moveDirection;
    [SerializeField] private float _speed = 5f;


    public override void Init(PlayerController parent)
    {
        base.Init(parent);
        if (groundCheck == null) groundCheck = parent.GetComponentInChildren<dahye.GroundCheck>();
        if (_rigidbody == null) _rigidbody = parent.GetComponent<Rigidbody>();
        _leftTheGround = false;

        //do actual jump logic
        //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpVelocity, _rigidbody.velocity.z);
        rb.AddForce(parent.transform.up * _jumpVelocity, ForceMode.Impulse);
        Debug.Log("left: ");
        jumpTime = Time.time;
    }

    public override void CaptureInput()
    {
        moveDirection = calcualteMoveDirection(); //get movedirection here, every frame
    }

    public override void Update() //speed control, animations and seeing if the player left the ground
    {
        if (!groundCheck.Check())
            _leftTheGround = true;

        SpeedControl(_speed);



    }

    public override void FixedUpdate() //actually move player
    {
        MovePlayer(moveDirection, _speed, 0.4f, 0.4f);

    }

    public override void ChangeState()
    {
        if ((_leftTheGround || Time.time > jumpTime + 0.5f) && groundCheck.Check()) //was in the air and is touching the ground
        {
            _runner.SetState(typeof(WalkState));
        }
    }


    public override void Exit() { }
}
