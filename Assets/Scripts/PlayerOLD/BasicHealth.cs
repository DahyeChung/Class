using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealth : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public GameObject thisShield;
    public GameObject thisShieldTwo;
    public GameObject thisTwo;
    public GameObject thisThree;
    // Update is called once per frame
    void Update()
    {
        

        if (playerInfo.health >= 2)
        {

            thisTwo.SetActive(true);

            if (playerInfo.shieldActive)
            {
                thisShield.SetActive(false);
                thisShieldTwo.SetActive(true);
                return;
            }
            else
            {

                thisShieldTwo.SetActive(false);
            }
        }
        else
        {
            thisTwo.SetActive(false);
        }

        if (playerInfo.health >= 1)
        {

            thisThree.SetActive(true);

            if (playerInfo.shieldActive)
            {
                thisShieldTwo.SetActive(false);
                thisShield.SetActive(true);
                return;
            }
            else
            {

                thisShield.SetActive(false);
            }

        }
        else
        {
            thisThree.SetActive(false);
        }

    }
}
