using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HUD : MonoBehaviour
{
    public List<GameObject> luciferinList = new List<GameObject>();

    //// 4개의 스킬 아이콘을 저장할 Image와 Sprite
    //public List<Image> skillIconImages;

    //// 사용된 스킬이 Define.SkillType에 따라 어떤 스킬인지 판별
    //public Define.SkillType skillType;

    //// current Image
    //public Image currentSkillIconImage;

    //public SkillBase skillBase;

    [SerializeField] private Image skillIconImage;

    public void UpdateSkillIcon(Sprite newIcon)
    {
        skillIconImage.sprite = newIcon;
    }
    private void Start()
    {

    }
    private void Update()
    {
        // SkillManager 에서 UseLuficerin 함수를 호출하면 UI_HUD에서 Luciferin 갱신 하려고했는데
        UseLuciferin(PlayerInfo.instance.luciferin);
    }

    // OnEnable에 PlayerGadgetController.OnSkillChanged += SetSkillIcon; // 이벤트 구독?


    public void EnableLuciferin()
    {
        // If the player get the luciferin, the light will be activated
    }

    public void UseLuciferin(int luciferinCount)
    {
        // UI 루시페린 갱신
        for (int i = 0; i < luciferinList.Count; i++)
        {
            luciferinList[i].SetActive(false);
        }
        var cnt = PlayerInfo.instance.luciferin;

        for (int i = 0; i < cnt; i++)
        {
            luciferinList[i].SetActive(true);
            // TODO : update animation
        }
    }

    #region Gadget




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
    #endregion
}

// 키를 누란다.(어디서든지)) -> 플레이어 인포가 호출된다.-> 스킬 컨트롤러로 스킬 사용한다.. -> 유아이 허드의 루시페린을 변경시킨다.

// 플레이어 인포에서 키 누른다 -> 스킬 컨트롤러로 스킬 사용한다 -> 유아이 허드의 루시페린을 변경시킨다.