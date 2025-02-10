using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public float detectionRange = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        
        //detects if player is in range

        //ADD CHECK IF PLAYER IS IN LINE OF SIGHT
        if (Vector3.Distance(transform.position, ThirdPersonMovement.instance.transform.position) < detectionRange)
        {

            //check if the player is in line of sight before firing

            RaycastHit hit;

            Vector3 raycastDir = ThirdPersonMovement.instance.transform.position - transform.position;

            if (Physics.Raycast(transform.position, raycastDir, out hit))
            {
                if (hit.collider.tag == "Player")
                {
                    Debug.DrawRay(transform.position, raycastDir, Color.green);


                }
                else
                {
                    Debug.DrawRay(transform.position, raycastDir, Color.yellow);
                }


            }


        }

    }
}
