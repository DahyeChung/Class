using UnityEngine;

// ItemLumi 클래스는 ItemBase를 상속받아 특정 아이템이 획득될 때 발생하는 효과를 처리하는 기능을 담당한다.
public class ItemLumi : ItemBase
{
    // 아이템을 획득할 때 발생하는 효과의 이름을 저장하는 변수
    public string getEffect;

    // 아이템 획득 후 일정 시간 후에 발생하는 다음 효과의 이름을 저장하는 변수
    public string nextEffect;

    // 다음 효과가 발생하기까지의 지연 시간을 설정하는 변수 (기본값: 1초)
    public float nextEffectDelayTime = 1f;

    // 아이템이 획득될 때 호출되는 메서드 (부모 클래스의 GetItem()을 오버라이드)
    public override void GetItem()
    {
        // 아이템 오브젝트를 비활성화하여 씬에서 보이지 않게 처리
        this.gameObject.SetActive(false);

        // getEffect에 값이 존재하면 해당 효과를 생성
        if (!string.IsNullOrEmpty(getEffect))
            PoolMananger.instance.GetSpawn(getEffect, transform.position, Quaternion.identity);

        // nextEffect에 값이 존재하면 설정된 딜레이 후에 DelayLumiSpace 메서드를 호출
        if (!string.IsNullOrEmpty(nextEffect))
            Invoke("DelayLumiSpace", nextEffectDelayTime);
    }

    // 설정된 딜레이 이후에 호출되어 nextEffect를 생성하는 메서드
    public void DelayLumiSpace()
    {
        // nextEffect를 해당 위치에서 생성
        var ob = PoolMananger.instance.GetSpawn(nextEffect, transform.position, Quaternion.identity);
    }
}
