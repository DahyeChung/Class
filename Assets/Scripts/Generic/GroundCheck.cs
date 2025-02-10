using UnityEngine;
namespace dahye
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField]
        private float _groundCheckRadius = 0.05f;
        [SerializeField]
        private LayerMask _collisionMask;



        //checks a sphere generated to see if object is grounded
        public bool Check()
        {
            Vector3 newVec = new Vector3(transform.position.x, transform.position.y, transform.position.z); //new vector3 that makes groundcheck ahppen higher up \\
                                                                                                            //NOT ACTUALLY USED ^


            return Physics.CheckSphere(newVec, _groundCheckRadius, _collisionMask);

        }
    }
}

