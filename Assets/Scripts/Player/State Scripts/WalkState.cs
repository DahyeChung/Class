using UnityEngine;

[CreateAssetMenu(menuName = "States/Character/Walk")]


public class WalkState : State<PlayerController> //makes this state only available to PlayerController
{
    //keycodes
    [Header("Keycodes")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.C;
    public KeyCode grappleKey = KeyCode.F;
    public KeyCode dashKey = KeyCode.LeftControl;


    //walkstate fields
    [Header("Walk state fields")]
    private Rigidbody _rigidbody;
    private dahye.GroundCheck groundCheck;
    private CharacterAnimation _animation;

    //possible keys
    private bool _jump;
    private bool _crouch;

    //FIELDS EVERY STATE NEEDS
    [Header("Movement")]
    private Vector3 moveDirection;
    [SerializeField] private float _speed = 5f;

    public override void Init(PlayerController parent)
    {
        base.Init(parent); //set parent variable

        //check if variables havent been assigned, if not, get components
        if (groundCheck == null) groundCheck = parent.GetComponentInChildren<dahye.GroundCheck>();
        if (_rigidbody == null) _rigidbody = parent.GetComponent<Rigidbody>();
        if (_animation == null) _animation = parent.CharacterAnimation;

        //_rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        // playerTransform = parent.transform;
        // Debug.Log("walk ");
    }

    public override void CaptureInput() //grab input for the player
    {

        _jump = Input.GetKeyDown(jumpKey);
        _crouch = Input.GetKeyDown(crouchKey);

        moveDirection = calcualteMoveDirection(); //get movedirection here, every frame
    }

    public override void Update() //update aniamtion and control speed
    {

        _animation.WalkingAnimation(groundCheck.Check());

        SpeedControl(_speed);


    }

    public override void FixedUpdate() //actually move player
    {

        MovePlayer(moveDirection, _speed, 0.4f, 1f);
        Debug.Log(groundCheck.Check());
    }

    public override void ChangeState()
    {
        if (_groundCheck.Check() && _jump) //if character is grounded and presses jump 
        {
            _runner.SetState(typeof(JumpState));
        }

        if (_groundCheck.Check() && _crouch) //if player presses crouch they jump
        {
            _runner.SetState(typeof(CrouchState));
        }

        if (!_groundCheck.Check())
        {
            Debug.Log("here");
            _runner.SetState(typeof(FallState)); ;
        }


    }


    public override void Exit() { } //dont do anything, dont have to.


}
