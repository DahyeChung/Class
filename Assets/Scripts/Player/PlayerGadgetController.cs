using UnityEngine;

public class PlayerGadgetController : MonoBehaviour
{
    private PlayerInfo player;

    void Start()
    {
        player = GetComponent<PlayerInfo>();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Scouting");
            player.Scouting1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Absorption");
            player.LumiAbsorption2();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Flashbang");
            player.FLashBang3();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
        }
    }

}
