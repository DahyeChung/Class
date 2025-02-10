using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



//this is the main script tat deals with enemies
public class Enemy : MonoBehaviour
{
    [Header("Enemy Info")]
    public int enemyHealth = 3;
    public EnemyState currentState;
    public Animator enemyAnimator;
    [Header("Enemy Detection")] //references detection state the enemy is in
    public EnemyDetection enemyDetection;

    [Header("Navmesh")] //navigation mesh the enemy follows
    public NavMeshAgent agent;

    [Header("Enemy Patrol")] //patrol points the enemy follows
    public Transform[] patrolPoints; //enemy patrol points
    private int destPoint = 0; //destination point
    public bool readyToPatrol; //bool to see fit eh enemy is ready to move to the next point

    [Header("Enemy Varaibles")] //patrol points the enemy follows
    public float walkSpeed; //not used yet, change movement speed when the enemy is chasing player
    public float runSpeed; //not used yet, change movement speed when the enemy is chasing player
    public bool canBeAssassinated; //bool to see if the enemy can be assassinated
    public bool hasLOS; //sees if the enemy has Line Of sight on the player
    [Header("Investigating")] //used for the investigating state
    Vector3 lastKnownLocation; //vector that stores the players 

    [Header("EnemyAttack")] //enemy attack variables
    public bool enemyAttacking; //bool to see if the enemy is attacking, if true the enemy will only move acording to that action.
   

    public enum EnemyState
    {
        relaxed,
        scanning, //needs to be added
        investigating,
        alert,
        attacking,
        jumping

    }
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; //makes it so enemy doesnt slow down when reaching a point, can enable if we want
    }

    // Update is called once per frame
    void Update()
    {
        EnemyStateChange();
       // Debug.Log(agent.remainingDistance);
        // Choose the next destination point when the agent gets
        // close to the current one and is ready to patrol again
        //as well as not currently chasing or inspecting the player


        //check if enemy as assassinateable
        canBeAssassinated = assassinateable();
        //check if enemy has LOS

        hasLOS = checkLineOfSight();

        if (agent.remainingDistance < 0.1f && currentState != EnemyState.attacking)
        {
            enemyAnimator.SetBool("Walking", false);

        }
        else if (agent.remainingDistance > 0.1f && currentState != EnemyState.attacking) 
        {
            enemyAnimator.SetBool("Walking", true);

        }
    }




    public void EnemyStateChange()
    {

        switch (currentState)
        {

            case EnemyState.relaxed:

                agent.speed = walkSpeed;
                //patrol paths
                if (!agent.pathPending && agent.remainingDistance < 0.5f && readyToPatrol  && enemyDetection.currentState == EnemyDetection.DetectionState.unaware)
                {
                    GotoNextPoint();
                }


                //investigate if suspicion is over 50%
                if (enemyDetection.suspicion > enemyDetection.maxSuspicion /2)
                {
                    
                    currentState = EnemyState.investigating;
                    lastKnownLocation = ThirdPersonMovement.instance.transform.position;
                    return;
                }


                //stop if they see the player
                if (enemyDetection.currentState == EnemyDetection.DetectionState.suspicious)
                {
                    
                    agent.speed = 0;
                   
                    
                }
                else
                {
                    agent.speed = walkSpeed;
                }
                


                //chase if enemy becomes aware
                if (enemyDetection.currentState == EnemyDetection.DetectionState.aware)
                {
                    currentState = EnemyState.alert;

                }

                break;

            case EnemyState.investigating:

                agent.speed = walkSpeed;

                agent.destination = lastKnownLocation;

                //enemy investigates last known player location


                //if the enemy sees the player, they update thier last known position
                if(enemyDetection.seeingPlayer == true)
                {
                    lastKnownLocation = ThirdPersonMovement.instance.transform.position;
                }


                //if the enemy cant find them, they go back to thier patrol
                if(!agent.pathPending && agent.remainingDistance < 0.5f && enemyDetection.suspicion <= 0)
                {
                    currentState = EnemyState.relaxed;
                }

                //if enemy becomes aware, start chasing the player
                if (enemyDetection.currentState == EnemyDetection.DetectionState.aware)
                {
                    currentState = EnemyState.alert;
                }

                break;

            case EnemyState.alert:


                agent.speed = runSpeed;


                //if the enemy sees the player, they update thier last known position
                if (enemyDetection.seeingPlayer == true)
                {
                    lastKnownLocation = ThirdPersonMovement.instance.transform.position;
                }



                //chase the player if not attacking
                //similar to investigating

                if (!enemyAttacking) //only do all of this if the enemy is not attacking
                {
                    //chase the player if not attacking
                    //similar to investigating
                    agent.destination = lastKnownLocation;


                    //if enemy becomes suspicious, go back into investigating, only way to get
                    if (enemyDetection.currentState == EnemyDetection.DetectionState.suspicious)
                    {
                        currentState = EnemyState.investigating;
                    }

                    //have this just to make sure
                    if (enemyDetection.currentState == EnemyDetection.DetectionState.unaware)
                    {
                        currentState = EnemyState.investigating;
                    }
                }
                
               

                break;

            case EnemyState.attacking:

                




                break;
        }

    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (patrolPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = patrolPoints[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }


    //alert functions
    public bool isPlayerInRadius()
    {
        bool inRadius = false;

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, enemyDetection.radius); //every collider in detection radius
        foreach (var hitCollider in hitColliders) //goes through each collider and sees if it is the player, optimize later with layermask
        {
            if (hitCollider.CompareTag("Player")) //this will let us know if the player is being seen
            {

                inRadius = checkRadius();
            }

        }


        return inRadius;
    }

    public bool checkRadius() //checks to see if the player is in the radius of the enemy and they can cast a ray to them
    {

        


        
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
        
        return false;
    }

    public bool assassinateable()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = Vector3.Normalize(ThirdPersonMovement.instance.transform.position - transform.position);


        


        if (currentState != EnemyState.alert && Vector3.Dot(forward, toOther) < 0 && checkLineOfSight()) //check if enemy is not alert, player is behind them, and has line of sight
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }


    public void takeDamage()
    {
        enemyHealth--;

        if (enemyHealth <= 0)
        {
            Object.Destroy(this.gameObject);
        }

    }

    public bool checkLineOfSight()
    {
        

        RaycastHit hit;

        Vector3 raycastDir = ThirdPersonMovement.instance.transform.position - transform.position;

        if (Physics.Raycast(transform.position, raycastDir, out hit)) //raycast check to see if player has line of sight on enemy
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }

        }

        return false;
    }
}
