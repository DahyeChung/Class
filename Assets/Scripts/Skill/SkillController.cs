using System;
using Unity.Mathematics;
using UnityEngine;

public class SkillController
{
    // 특정 스킬을 발동하는 함수
    public void SkillShot(int skillID, Vector3 pos, Quaternion rot, Action callback = null)
    {
        // 데이터베이스에서 skillID에 해당하는 스킬 정보를 가져옴
        var skillData = DB.DataTables.GetSkill(skillID);

        // 만약 해당 skillID가 존재하지 않는다면 에러 메시지를 출력하고 함수 종료
        if (skillData == null)
        {
            Debug.LogError("Skill not found ID: " + skillID);
            return;
        }

        // 플레이어의 루시페린(Luciferin)이 스킬을 사용하기에 충분한지 확인
        if (PlayerInfo.instance.luciferin < math.abs(skillData.Luciferin))
        {
            Debug.Log("Not enough Luciferin"); // 부족하면 메시지를 출력하고 함수 종료
            return;
        }

        // 스킬 사용 시 필요한 루시페린을 차감
        PlayerInfo.instance.luciferin -= skillData.Luciferin;

        // 오브젝트 풀에서 해당 스킬 오브젝트를 가져와 생성 (위치 및 회전 적용)
        var skillOB = PoolMananger.instance.GetSpawn(skillData.Name, pos, rot);

        // 만약 스킬 오브젝트가 존재하고, SkillBase 컴포넌트를 가지고 있다면 스킬 정보 설정
        if (skillOB && skillOB.GetComponent<SkillBase>())
        {
            // SkillBase 스크립트에 스킬 정보를 전달하고, 콜백 함수 설정
            skillOB.GetComponent<SkillBase>().SetInfo(skillData, callback);
        }
    }
}