using System.Collections;
using UnityEngine;

public class SkillLumiSpace : MonoBehaviour
{
    // 스킬의 지속 시간을 설정하는 변수
    public float duration;

    // 오브젝트가 활성화될 때 실행되는 메서드
    private void OnEnable()
    {
        // duration 값이 0보다 크다면 일정 시간이 지난 후 자동으로 비활성화되는 코루틴을 실행
        if (duration > 0f)
            StartCoroutine(Duration());
    }

    // 지정된 시간이 지난 후 오브젝트를 비활성화하는 코루틴
    public virtual IEnumerator Duration()
    {
        yield return new WaitForSeconds(duration);

        this.gameObject.SetActive(false);
    }

    // 콜라이더에 다른 오브젝트가 들어왔을 때 실행되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 확인
        if (other.gameObject.tag.Equals(Define.UnitType.Player.ToString()))
        {
            // 플레이어 정보 컴포넌트가 있다면 'isHide' 상태를 true로 설정하여 숨김 처리
            if (other.gameObject.GetComponent<PlayerInfo>())
                other.gameObject.GetComponent<PlayerInfo>().isHide = true;

            // 디버그 메시지를 출력하여 충돌한 오브젝트의 이름을 확인
            Debug.Log("other Name : " + other.gameObject.name);
        }
    }

    // 콜라이더에서 다른 오브젝트가 나갔을 때 실행되는 메서드
    private void OnTriggerExit(Collider other)
    {
        // 충돌했던 오브젝트가 플레이어인지 확인
        if (other.gameObject.tag.Equals(Define.UnitType.Player.ToString()))
        {
            // 플레이어 정보 컴포넌트가 있다면 'isHide' 상태를 false로 설정하여 숨김 해제
            if (other.gameObject.GetComponent<PlayerInfo>())
                other.gameObject.GetComponent<PlayerInfo>().isHide = false;

            // 디버그 메시지를 출력하여 충돌이 끝난 오브젝트의 이름을 확인
            Debug.Log("other Name : " + other.gameObject.name);
        }
    }
}
