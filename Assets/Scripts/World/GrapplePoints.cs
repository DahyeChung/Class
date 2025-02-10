using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoints : MonoBehaviour
{

    public Renderer m_Renderer;

    [Header("GrapplePointInfo")]
    public bool readyToGrapple = false;
    public bool pointInCamera = false;
    public bool inFrontOfPlayer = false;
    public bool inRange = false;
    public float MaxDistance = 20f;
    [SerializeField] private float distance;
    public GameObject UI;
    public bool testclosest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        pointInCamera = checkIfPointInCamera(); //checks if the grapple point is in camera view
        inFrontOfPlayer = checkIfInFront(); //checks if the player is facing the grapple point
        inRange = checkIfInDIstance(); //checks if the player is close enough to the grapple point

        if(pointInCamera && inFrontOfPlayer && inRange)
        {
            readyToGrapple = true;
        }
        else
        {
            readyToGrapple = false; 
        }

        //enable UI if closest

        if (Grapple.instance.closest)
        {
            if (this.name == Grapple.instance.closest.name && readyToGrapple == true)
            {
                testclosest = true;
                UI.SetActive(true);
            }
            else
            {
                testclosest = false;
                UI.SetActive(false);
            }
        }
       
    }

    public bool checkIfPointInCamera()
    {



        if (m_Renderer.isVisible)
        {
            return true;
        }
        else
        {
            return false;
        }


    }


    //checks to see if the grapple point is in front of the player
    public bool checkIfInFront()
    {
        Vector3 forward = ThirdPersonMovement.instance.transform.TransformDirection(Vector3.forward);
        Vector3 toOther = Vector3.Normalize(transform.position - ThirdPersonMovement.instance.transform.position);


        if (Vector3.Dot(forward, toOther) > .707) //this means the player is in the enemies FOV
        {
            //print(Vector3.Dot(forward, toOther));

            RaycastHit hit;

            Vector3 raycastDir = ThirdPersonMovement.instance.transform.position - transform.position;

            if (Physics.Raycast(transform.position, raycastDir, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawRay(transform.position, raycastDir, Color.green);



                    return true;
                }
            }
        }
        return false;
    }

    public bool checkIfInDIstance()
    {



         distance = Vector3.Distance(ThirdPersonMovement.instance.transform.position, transform.position);


        if(distance <= MaxDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
        

    }

}
