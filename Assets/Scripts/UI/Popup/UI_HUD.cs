using UnityEngine;

public class UI_HUD : MonoBehaviour
{
    private PlayerInfo playerInfo;
    // private List<GameObject> luciferinList = new List<GameObject>();


    public GameObject luciferinLight1, luciferinLight2, luciferinLight3;

    enum GameObjects
    {
        ContentObject,

        // Gadget 
        IconMainGadget,
        IconScouting,
        IconAbsorption,
        IconFlashbang,

        // Luciferin
        Luciferin1,
        Luciferin2,
        Luciferin3,

    }

    private void Start()
    {
        Debug.Log("현재 Luciferin : " + PlayerInfo.instance.luciferin);

    }
    private void Update()
    {
    }




    public void SetInfo()
    {
        // Use DataTable for skill info
    }

    public void GetGadget()
    {

        // If the player get the gadget, the icon will be activated

        // set active IconScouting true
    }

    public void EnableGadget()
    {
        // If the player get the gadget, the icon will be activated
    }

    public void SetMainGadget()
    {
        // If the player press the key, the icon will be activated
        Debug.Log("1,2,3 Key pressed");

    }

    public void UseGadget()
    {
        // If the player press the key, the skill will be used


        Debug.Log("Main skill used");
    }

    public void EnableLuciferin()
    {
        // If the player get the luciferin, the light will be activated
    }

    public void UseLuciferin(int luciferinCount)
    {
        Debug.Log("현재 Luciferin : " + luciferinCount);
        //// UI 루시페린 갱신
        //for (int i = 0; i < luciferinList.Count; i++)
        //{
        //    luciferinList[i].SetActive(false);
        //}
        //var cnt = PlayerInfo.instance.luciferin;

        //for (int i = 0; i < cnt; i++)
        //{
        //    luciferinList[i].SetActive(true);
        //}
    }


}

// 키를 누란다.(어디서든지)) -> 플레이어 인포가 호출된다.-> 스킬 컨트롤러로 스킬 사용한다.. -> 유아이 허드의 루시페린을 변경시킨다.

// 플레이어 인포에서 키 누른다 -> 스킬 컨트롤러로 스킬 사용한다 -> 유아이 허드의 루시페린을 변경시킨다.