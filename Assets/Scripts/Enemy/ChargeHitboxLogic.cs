using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeHitboxLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            PlayerInfo.instance.playerTakeDamage(1);
        }

        transform.parent.GetComponent<BoarAttack>().hitWall = true;

    }
}
