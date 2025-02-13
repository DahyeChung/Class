using System;
using UnityEngine;

public class PlayerGadgetController : MonoBehaviour
{
    private PlayerInfo player;
    public event Action OnSkillUse;
    public event Action<Sprite> OnSkillChanged;

    void Start()
    {
        player = GetComponent<PlayerInfo>();
        OnSkillUse = null; // no skill registered


        InputManager.OnKey1Pressed += () => RegisterSkill(player.Scouting1, "Scouting");
        InputManager.OnKey2Pressed += () => RegisterSkill(player.LumiAbsorption2, "Absorption");
        InputManager.OnKey3Pressed += () => RegisterSkill(player.FLashBang3, "Flashbang");
        InputManager.OnKeyEPressed += UseSkill;

        //FindObjectOfType<UI_HUD>().UpdateSkillIcon += (newIcon) => hud.UpdateSkillIcon(newIcon);

        Debug.Log("스킬 구독 완료");

    }
    private void RegisterSkill(Action skill, string skillName)
    {
        Debug.Log($"{skillName} 등록됨");

        OnSkillUse = skill;
        // 스킬 아이콘 변경 이벤트 호출
        //OnSkillChanged?.Invoke(skillIcon);
    }

    // 등록된 스킬 사용
    private void UseSkill()
    {
        if (OnSkillUse != null)
        {
            Debug.Log("스킬 사용!");
            OnSkillUse.Invoke();
        }
        else
        {
            Debug.Log("등록된 스킬이 없습니다!");
        }
    }
}
