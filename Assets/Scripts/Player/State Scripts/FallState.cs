using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Fall")]
public class FallState : State<PlayerController>
{
    [Header("Keycodes")]
    public KeyCode jumpKey = KeyCode.Space;


    [Header("Fall state fields")]
    private dahye.GroundCheck groundCheck;
    private Rigidbody _rigidbody;
    [SerializeField] private bool _leftTheGround; //bool to see if the player left the ground
    private float timeLeftGround; //time the player left the ground
    private bool _jump;

    [SerializeField]
    private float cyoteTime;

    [Header("Movement")]
    //FIELDS EVERY STATE NEEDS
    private Vector3 moveDirection;
    [SerializeField] private float _speed = 5f;


    public override void Init(PlayerController parent)
    {
        base.Init(parent);
        if (groundCheck == null) groundCheck = parent.GetComponentInChildren<dahye.GroundCheck>();
        if (_rigidbody == null) _rigidbody = parent.GetComponent<Rigidbody>();
        _leftTheGround = true;


        timeLeftGround = Time.time;
    }

    public override void CaptureInput()
    {
        _jump = Input.GetKeyDown(jumpKey);
        moveDirection = calcualteMoveDirection(); //get movedirection here, every frame
    }

    public override void Update() //speed control, animations and seeing if the player left the ground
    {
        if (!groundCheck.Check())
            _leftTheGround = true;


        Debug.Log("left ground:" + _leftTheGround);
        SpeedControl(_speed);



    }

    public override void FixedUpdate() //actually move player
    {
        MovePlayer(moveDirection, _speed, 0.4f, 0.4f);

    }

    public override void ChangeState()
    {
        if (_leftTheGround && groundCheck.Check()) //was in the air and is touching the ground
        {

            _runner.SetState(typeof(WalkState));
        }

        if (_jump && Time.time <= timeLeftGround + cyoteTime)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _runner.SetState(typeof(JumpState));
        }


    }




    public override void Exit() { }

}