using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJunk : MonoBehaviour
{
    //place toi store codde for reverts
    /*
    public class EnemyDetection : MonoBehaviour
    {
        [Header("Detection State")]
        public bool seeingPlayer = false;
        public float timeSeen = 0f;
        public float DetectionTime = 15f;

        [Header("Radius")] //player state machine
        public float radius = 10f;
        public float instaDetectRadius = 5f;

        [Header("Navmesh")] //player state machine
        private NavMeshAgent agent;

        [Header("Player")] //player reference
        public bool lostPlayer = false;
        Vector3 lastKnownLocation;
        // Start is called before the first frame update

        [Header("Look Around")]//variables related to the "looking around" state
        public float RotationSpeed = 1f;

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

            //fucntion to raise suspicion (actually put in suspicion state





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
                    timeSeen += 1 * Time.deltaTime;



                }
                else //the enemy now does not see the player
                {
                    //send to last position the player was at, have them look around, then go back to thier patrol route

                    if (timeSeen > 0)
                    {
                        timeSeen -= 0.3f * Time.deltaTime;
                    }
                }


                //move logic to enemy.cs
                if (timeSeen >= DetectionTime)
                {

                    FollowPlayer();
                }
                else
                {
                    StopCoroutine(LookAround());
                }

            }
        }

        private void FixedUpdate()
        {

        }

        void EnemyDetectionStateChange()
        {
            switch (currentState)
            {
                case DetectionState.unaware: //if the enemy sees the player while they are unaware, they will become suspicious


                    //the enemy will instantly become aware if the player is in instant detection range




                    if (seeingPlayer == true)
                    {
                        currentState = DetectionState.suspicious;
                    }



                    break;

                case DetectionState.suspicious: //if the enemy sees the player while they are suspicious, thier suspicion will grow until they are aware of the player
                                                // suspicion will be broken if the player stays out of line of sight for long enough


                    break;


                case DetectionState.aware:  //the enemy is now aware of the player, and will become suspicious if line of sight is blocked for a certain amount of time



                    break;
            }





        }




        public bool checkFOV() //checks to see if the player is in the vision cone of the enemy
        {

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toOther = Vector3.Normalize(ThirdPersonMovement.instance.transform.position - transform.position);


            if (Vector3.Dot(forward, toOther) > .707) //this means the player is in the enemies FOV
            {
                print("in front!");

                RaycastHit hit;

                Vector3 raycastDir = ThirdPersonMovement.instance.transform.position - transform.position;

                if (Physics.Raycast(transform.position, raycastDir, out hit))
                {
                    if (hit.collider.tag == "Player")
                    {
                        Debug.DrawRay(transform.position, raycastDir, Color.green);

                        if (hit.distance < 5f)//ADD: find how long it took the raycast to hit, if in the instadetect radius make it 
                        {
                            timeSeen = 15f;
                        }

                        return true;
                    }
                }
            }
            return false;
        }

        void FollowPlayer()
        {

            if (seeingPlayer == true) //the enemy sees the player
            {
                lostPlayer = false;
                agent.destination = ThirdPersonMovement.instance.transform.position;
            }
            else //the enemy no longer sees the player
            {
                if (lostPlayer == false)
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



    */

}
