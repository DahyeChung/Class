using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDetection : MonoBehaviour
{
    [Header("Detection State")]
    public bool seeingPlayer = false;
    public float suspicion = 0f; //suspicion value of the enemy
    public float maxSuspicion = 15f; //maximum suspicion value
    public float suspicionDelay = 3f; //the time is takes for the suspision guage to deplete once the player is not seen
    private float suspicionTime = 0f; //the time the enemy has been suspicious, used with suspicionDelay

    [Header("AwareState")]
    public float minAwareTime = 5f; //the minimum time an enemy will be aware of the player once the "Aware" reaching that state
    private float awareTime = 0f; //the time the enemy became aware of the player


    [Header("Radius")] //player state machine
    public float radius = 10f;
    public float instaDetectRadius = 5f;

    public float enemyFOV = 0f;
    public float normalEnemyFov = .707f;
    public float alertFov = 0f;


    [Header("Navmesh")] //player state machine
    private NavMeshAgent agent;

    [Header("Player")] //player reference
    public bool lostPlayer = false;
    Vector3 lastKnownLocation;
    // Start is called before the first frame update

   // [Header("Look Around")]//variables related to the "looking around" state
   // public float RotationSpeed = 1f;

    public DetectionState currentState;
    public enum DetectionState
    {
        unaware,
        suspicious,
        aware


    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        //new update


        //fucntion to see if enemy sees the player
        lookForPlayer();
        //fucntion to raise suspicion (actually put in suspicion state

        EnemyDetectionStateChange();






        /*

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (var hitCollider in hitColliders) //goes through each collider and sees if it is the player, optimize later with layermask
        {
            if (hitCollider.CompareTag("Player")) //this will let us know if the player is being seen
            {

                seeingPlayer = checkFOV();
            }

            if (seeingPlayer == true) //if the player is being seen, then the enemy suspision raises
            {
                StopCoroutine(LookAround()); //stops the enemy looking around if they see the player
                suspicion += 1 * Time.deltaTime;

                 

            }
            else //the enemy now does not see the player
            {
                //send to last position the player was at, have them look around, then go back to thier patrol route

                if(suspicion > 0)
                {
                    suspicion -= 0.3f * Time.deltaTime;
                }
            }


            //move logic to enemy.cs
            if(suspicion >= maxSuspicion)
            {
                
                FollowPlayer();
            }
            else
            {
                StopCoroutine(LookAround());
            }

         
        }*/

    }

    private void FixedUpdate()
    {
       
    }

    void EnemyDetectionStateChange()
    {
        switch (currentState)
        {
            case DetectionState.unaware: //if the enemy sees the player while they are unaware, they will become suspicious

                enemyFOV = normalEnemyFov; //set FOV
                //the enemy will instantly become aware if the player is in instant detection range
                if (seeingPlayer == true && Vector3.Distance(ThirdPersonMovement.instance.transform.position, this.transform.position) < instaDetectRadius)  //DOESNT WORK, CHECK?
                {
                    currentState = DetectionState.aware;
                    awareTime = Time.time;
                }



                if (seeingPlayer == true)
                {
                    currentState = DetectionState.suspicious;
                }

                

            break;

            case DetectionState.suspicious: //if the enemy sees the player while they are suspicious, thier suspicion will grow until they are aware of the player

                enemyFOV = normalEnemyFov; //set FOV

                // suspicion will be broken if the player stays out of line of sight for long enough

                //the enemy will instantly become aware if the player is in instant detection range
                if (seeingPlayer == true && Vector3.Distance(ThirdPersonMovement.instance.transform.position, this.transform.position) < instaDetectRadius)  //DOESNT WORK, CHECK?
                {
                    currentState = DetectionState.aware;
                    awareTime = Time.time;
                }

                SuspicionLogic();

                if (suspicion >= maxSuspicion) //if suspicion is at maximum, the enemy becomes aware of the player
                {
                    currentState = DetectionState.aware;
                    awareTime = Time.time;
                }

                

                if(suspicion < 0)
                {
                    currentState = DetectionState.unaware;
                }
                

                break;


            case DetectionState.aware:  //the enemy is now aware of the player, and will become suspicious if line of sight is blocked for a certain amount of time

                enemyFOV = alertFov; //set FOV

                // if player is seen, aware time is then
                if (seeingPlayer)
                {
                    awareTime = Time.time;
                }


                //SuspicionLogic(); //suspicion logic used 

                if (Time.time > awareTime + minAwareTime && !seeingPlayer) //if its been longer than the minimum aware time and the enemy does not see the player go back to suspicious
                {
                    //set suspicion at half
                    suspicion = maxSuspicion / 2;
                    currentState = DetectionState.suspicious;
                }
                


                break;
        }

            



    }

    public void lookForPlayer() //fucntion used to search for the player, and see if the player is visible
    {

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius); //every collider in detection radius
        foreach (var hitCollider in hitColliders) //goes through each collider and sees if it is the player, optimize later with layermask
        {
            if (hitCollider.CompareTag("Player")) //this will let us know if the player is being seen
            {

                seeingPlayer = checkFOV();
            }

        }
    }



    public bool checkFOV() //checks to see if the player is in the vision cone of the enemy
    {
        
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = Vector3.Normalize(ThirdPersonMovement.instance.transform.position - transform.position);


            if(Vector3.Dot(forward, toOther) > enemyFOV) //this means the player is in the enemies FOV
            {
                //print("in front!");

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

    public void SuspicionLogic() //this function rasises suspicion if the player is seen
    {
        if (seeingPlayer == true) //if the player is being seen, then the enemy suspision raises
        {
            suspicionTime =  Time.time; //saves the time that suspicion is happening at, used for the suspicion delay

            if(suspicion < maxSuspicion) //suspicion is not raised beyond maxSuspicion
            {
                suspicion += 1f * Time.deltaTime;
            }
            else //if suspicion is over maximum already, then just set it to maxSuspicion to make it more consistant
            {
                suspicion = maxSuspicion;
            }
          

        }
        else //if the player is not seen, suspicion goes down, after the delay
        {
           
            //if the current time goes over the time sightline was lost + the suspicion delay then suspicion starts getting subtracted
            //I.E if sight is lost at 10 seconds (game time), at 10 seconds + supicion delay (3 seconds) = 13 seconds, suspicion starts to deplete
            if (suspicion > 0  && (Time.time > suspicionTime  + suspicionDelay))
            {
                suspicion -= 1f * Time.deltaTime;
            }
        }
    }


   


    void FollowPlayer()
    {

        if(seeingPlayer == true) //the enemy sees the player
        {
            lostPlayer = false;
            agent.destination = ThirdPersonMovement.instance.transform.position;
        }
        else //the enemy no longer sees the player
        {
            if(lostPlayer == false)
            {
                lostPlayer = true;
                lastKnownLocation = ThirdPersonMovement.instance.transform.position;
            }

            agent.destination = lastKnownLocation;


           // StartCoroutine(LookAround());
            //ADD: start coruntine to have enemy look around
        }

        
        


    }

    private IEnumerator LookAround() //the corutine that will make the enemy look around 
    {

        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float t = 0.0f;
        float duration = 10f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation,
            transform.eulerAngles.z);

            yield return null;
        }

    }
}
