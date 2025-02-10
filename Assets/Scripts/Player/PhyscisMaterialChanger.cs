using UnityEngine;

public class PhyscisMaterialChanger : MonoBehaviour
{
    [SerializeField]
    private dahye.GroundCheck groundcheck;

    [SerializeField]
    private PhysicMaterial ground;
    [SerializeField]
    private PhysicMaterial air;
    [SerializeField]
    private PhysicMaterial standing;
    [SerializeField]
    private CapsuleCollider capsule;


    private bool wasInAir;
    private float timeInAir;
    private float timeToChangeMaterial = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        wasInAir = false;
        timeInAir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");







        if (groundcheck.Check() && (Time.time >= timeInAir + timeToChangeMaterial || (horizontalInput == 0 && verticalInput == 0)))
        {



            if (horizontalInput == 0 && verticalInput == 0) //if there no input use standing material
            {
                capsule.material = standing;
            }
            else //if there is input, use ground material
            {
                capsule.material = ground;
            }

            wasInAir = false;
        }
        else //if not grounded, use air material
        {
            capsule.material = air;
            wasInAir = true;
            if (!groundcheck.Check())
                timeInAir = Time.time;
        }
    }
}
