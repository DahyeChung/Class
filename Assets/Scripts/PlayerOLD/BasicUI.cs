using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerInfo playerInfo;

    public GameObject thisOne;
    public GameObject thisTwo;
    public GameObject thisThree;
    // Update is called once per frame
    void Update()
    {
        if (playerInfo.health >= 3)
        {

            thisOne.SetActive(true);
        }
        else
        {
            thisOne.SetActive(false);
        }

        if (playerInfo.health >= 2)
        {

            thisTwo.SetActive(true);
        }
        else
        {
            thisTwo.SetActive(false);
        }

        if (playerInfo.health >= 1)
        {

            thisThree.SetActive(true);
        }
        else
        {
            thisThree.SetActive(false);
        }

    }
}
