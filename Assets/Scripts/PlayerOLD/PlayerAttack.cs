using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [Header("Keybinds")]

    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode assassinateKey = KeyCode.Mouse1;

    [Header("Attack")]
    public GameObject hitbox;
    public Animator playerAnimator;
    [Header("assassinate")]
    public float radius = 5f; //the radius the payer can perform an assassination
    public GameObject closest; //the closest enemy that is assassinateable

    public PlayerAttackState currentState;
    public enum PlayerAttackState
    {
        idle,
        attacking,
        assassinating

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        attackStateChange();
        //  assassinateCheck();
    }


    public void attackStateChange()
    {
        switch (currentState)
        {

            case PlayerAttackState.idle:

                if (ThirdPersonMovement.instance.currentState != ThirdPersonMovement.PlayerState.jumping) //THE PLAYER CANNOT ATTACK or assassinate while jumping
                {


                    if (Input.GetKeyDown(attackKey))
                    {
                        StartCoroutine(AttackCoroutine());
                        currentState = PlayerAttackState.attacking;
                    }


                    if (Input.GetKeyDown(assassinateKey) && assassinateCheck())
                    {
                        //destroy closest enemy right now


                        StartCoroutine(AssassinateCoroutine());

                        //   currentState = PlayerAttackState.assassinating;
                    }
                }

                break;

            case PlayerAttackState.attacking:

                //make player stop moving
                ThirdPersonMovement.instance.canMove = false;

                break;


            case PlayerAttackState.assassinating:

                //find closest enemy in assassinate range

                //check if that enemy is assassinateable

                break;
        }
    }



    public bool assassinateCheck()
    {



        float minDist = Mathf.Infinity;

        closest = null;

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius); //every collider in assassinate radius
        foreach (var hitCollider in hitColliders) //goes through each collider and sees if it is an enemy, optimize later with layermask
        {



            if (hitCollider.CompareTag("Enemy")) //this will let us know if there is an enemy
            {
                Debug.Log("yup enemy here");
                float dist = Vector3.Distance(hitCollider.transform.position, ThirdPersonMovement.instance.transform.position); //records distance of the enemy

                if (hitCollider.GetComponent<Enemy>().canBeAssassinated && (dist < minDist)) //check if enemy is assassinateable and is the closest enemy
                {
                    closest = hitCollider.gameObject;
                    minDist = dist;




                }

            }

        }


        if (closest)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    private IEnumerator AttackCoroutine()
    {
        playerAnimator.SetTrigger("Attack");
        ThirdPersonMovement.instance.canMove = false;
        hitbox.SetActive(true);

        yield return new WaitForSeconds(1f);

        ThirdPersonMovement.instance.canMove = true;
        hitbox.SetActive(false);
        currentState = PlayerAttackState.idle;
    }

    private IEnumerator AssassinateCoroutine()
    {
        playerAnimator.SetTrigger("Assassinate");
        ThirdPersonMovement.instance.canMove = false;
        Object.Destroy(closest);
        yield return new WaitForSeconds(2f);
        ThirdPersonMovement.instance.canMove = true;
        currentState = PlayerAttackState.idle;
    }
}




