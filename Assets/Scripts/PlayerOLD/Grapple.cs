using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public static Grapple instance;

    [Header("Grapple state")]
    public bool grappling;

    [Header("Camera")] ///the camera and associated variables
    public Camera cam; //refernces camera. 
    public Vector3 viewPos;

    [Header("GrapplePoints")]
    public GameObject grapplePoints1;
    public GameObject closest;
    public int grappleSpeed;

    [Header("References")]
    public ThirdPersonMovement thirdPersonMovement;
    public CharacterController cc;

    [Header("Character Controller and animator")]
    public Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        thirdPersonMovement = this.GetComponent<ThirdPersonMovement>();
        cc = GetComponent<CharacterController>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        //get list of all grapple points that are visible

        //we do NOT do this while the player is grappling

        if(grappling == false)
        {

            //find all grapple points with the tag
            var grapplePoints = GameObject.FindGameObjectsWithTag("GrapplePoint");

            //the minimum distance is infinity as a base
            float minDist = Mathf.Infinity;

            closest = null;

            //we find the nearest grapple point from all in the scene
            foreach (var obj in grapplePoints)
            {
                float dist = Vector3.Distance(obj.transform.position, ThirdPersonMovement.instance.transform.position);

                if (obj.GetComponent<GrapplePoints>().readyToGrapple == true && (dist < minDist))
                {
                    closest = obj;
                    minDist = dist;

                    

                    
                }


            }


            

        }
        else
        {


            //if grappling is true, then we actually grapple to a point

            // Move our position a step closer to the target.
            playerAnimator.SetBool("isGrappling", true);
            var step = grappleSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, step);

            // Check if the position of the player and grapple point is equal
            Debug.Log(Vector3.Distance(transform.position, closest.transform.position));
            if (Vector3.Distance(transform.position, closest.transform.position) < 0.001f)
            {
             

                grappling = false;
                playerAnimator.SetBool("isGrappling", false);
                Debug.Log("We did it");
                thirdPersonMovement.startMovement();
            }
            




        }
        





    }


    public void GrappleToPointdumb(Vector3 target)
    {

        //stop player movement

        // ThirdPersonMovement.instance.GetComponent<ThirdPersonMovement>().StopMovement;

        //ThirdPersonMovement.instance.transform.position
        thirdPersonMovement.stopMovement();

        var cc = GetComponent<CharacterController>();
            var offset = target - transform.position;
            //Get the difference.
            if (offset.magnitude > .1f)
            {
                //If we're further away than .1 unit, move towards the target.
                //The minimum allowable tolerance varies with the speed of the object and the framerate. 
                // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
                offset = offset.normalized * grappleSpeed;
                //normalize it and account for movement speed.
                cc.Move(offset * Time.deltaTime);
                //actually move the character.
            }



        
    }
    /*
   public IEnumerator GrappleToPoint(CharacterController cc, Vector3 endpoint)
    {
        /*
        //stop player movement
        // thirdPersonMovement.StopMovement();

        var offset = endpoint - cc.transform.position;
        //var cc = GetComponent<CharacterController>();

        // canMove = false;
        //yield return new WaitForSeconds(1);
       // Debug.Log("Got into method");
       
        while (offset.magnitude > .1f)
        {
            //If we're further away than .1 unit, move towards the target.
            offset = offset.normalized * grappleSpeed;
            //normalize it and account for movement speed.
            cc.Move(offset * Time.deltaTime);
        }
        /*
                var offset2 = closest.transform.position - cc.transform.position;

                Debug.Log(offset2.magnitude);
        
        if (offset.magnitude < .1f)
        {
            thirdPersonMovement.startMovement();
            yield return null;
        }

        
        
    }
    */

}
