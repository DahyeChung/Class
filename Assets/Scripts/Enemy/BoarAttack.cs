using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAttack : MonoBehaviour
{
    [Header("Enemy Reference")] //referencesthe enemy script attatched to the enemy
    public Enemy enemy;
    public GameObject eyes;
    public Animator EnemyAnimator;
    [Header("Attack")] //references detection state the enemy is in
    public bool charging;
    public bool hitWall;
    public float chargeSpeed; //not used yet, change movement speed when the enemy is chasing player
    [Header("Charge")]
    public GameObject hitbox; //hitbox for the charge attack
    public float windupTime; //the time it takes for the boar to charge up
    GameObject Hitbox; //enemy attack hitbox, if this hits the player they take damage


    // Start is called before the first frame update
    void Start()
    {
        charging = false;
        hitWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //if the boar is alert and sees the player, attempt to charge at them, if not already


        if(enemy.currentState ==  Enemy.EnemyState.alert && enemy.checkLineOfSight() && charging == false)
        {
            enemy.currentState = Enemy.EnemyState.attacking; //set enemy state to attacking
            enemy.agent.destination = enemy.transform.position;//
            enemy.agent.ResetPath(); //reset the enemy pathing
            //the boar will now charge
            charging = true;
            hitWall = false;
            StartCoroutine(chargeCorutine());

        }
    }


    public IEnumerator chargeCorutine()
    {
        //windup and rotate to the player
        enemy.agent.speed = chargeSpeed;
        EnemyAnimator.SetTrigger("playerSpotted");
        float WindupStartTime = Time.time; // time we started winding up
        while (Time.time < WindupStartTime + windupTime)
        {

            Vector3 targetPostition = new Vector3(ThirdPersonMovement.instance.transform.position.x, this.transform.position.y, ThirdPersonMovement.instance.transform.position.z); //rotate the enemy only on the Y axis
            this.transform.LookAt(targetPostition);


           
            yield return null; // this will make Unity stop here and continue next frame
        }

        Debug.Log("charge");
        //activate hitbox

        hitbox.SetActive(true);
        //until hitbox hits a surface or time elapses we continue charging

        

        float chargeStartTime = Time.time; // time we started charging
        while (!hitWall && Time.time < chargeStartTime + 5f)
        {

            enemy.agent.destination = eyes.transform.position;
            yield return null; // this will make Unity stop here and continue next frame
        }
        hitbox.SetActive(false);

        EnemyAnimator.SetTrigger("EndCharge");
        charging = false;
        enemy.currentState = Enemy.EnemyState.relaxed; //set enemy state to idle
    }
}
