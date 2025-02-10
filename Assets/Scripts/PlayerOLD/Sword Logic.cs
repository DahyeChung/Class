using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLogic : MonoBehaviour
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

        //if is an enemy, and have chaarge, deal damage and take awawy a charge
       
        if (other.CompareTag("Enemy")&& PlayerInfo.instance.luciferin > 0)
        {
            Debug.Log("hit");

            PlayerInfo.instance.luciferin--; //decrease luciferin charge

            other.GetComponent<Enemy>().takeDamage();

        }
        
    }
}
