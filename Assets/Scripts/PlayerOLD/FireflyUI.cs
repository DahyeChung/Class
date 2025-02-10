using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyUI : MonoBehaviour
{
    public PlayerInfo playerInfo;

    public GameObject thisOne;
    public GameObject thisTwo;
    public GameObject thisThree;
    // Update is called once per frame
    void Update()
    {
        if (playerInfo.luciferin >= 3)
        {

            thisOne.SetActive(true);
        }
        else
        {
            thisOne.SetActive(false);
        }

        if (playerInfo.luciferin >= 2)
        {

            thisTwo.SetActive(true);
        }
        else
        {
            thisTwo.SetActive(false);
        }

        if (playerInfo.luciferin >= 1)
        {

            thisThree.SetActive(true);
        }
        else
        {
            thisThree.SetActive(false);
        }

    }
}
