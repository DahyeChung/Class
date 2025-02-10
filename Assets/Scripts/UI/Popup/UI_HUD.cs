public class UI_HUD : MonoBehaviour
{
    private PlayerInfo playerInfo;
    //private List<GameObject> luciferinList = new List<GameObject>();


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
        luciferinStack.Push(luciferinLight1);
        luciferinStack.Push(luciferinLight2);
        luciferinStack.Push(luciferinLight3); // first to be used
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // PlayerInfo.instance.TestSkill1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerInfo.instance.TestSkill2();
            GainLuciferin();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerInfo.instance.TestSkill3();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // UseGadget();
            UseLuciferin();
        }
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

    public void UseLuciferin()
    {
        if (luciferinStack.Count <= 0)
        {
            Debug.Log("No luciferin left");
            return;
        }

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
