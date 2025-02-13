using UnityEngine;

// SkillLumiRec 클래스는 특정 방향으로 스킬을 발사하고, Lumi 아이템과의 충돌을 감지하는 기능을 담당한다.
public class SkillLumiRec : SkillBase
{
    public override void Init()
    {
        base.Init();
        SkillType = Define.SkillType.LuminescentAbsorption;
    }

    // 스킬을 실행하는 메서드 (오버라이드된 Shot 메서드)
    public override void Shot()
    {
        switch (skillDir) // skillDir에 따라 다른 동작 수행
        {
            case SkillDir.Normal:
                // 현재 오브젝트에 ParticleSystem이 있다면, 멈춘 후 다시 실행
                if (GetComponent<ParticleSystem>())
                {
                    GetComponent<ParticleSystem>().Stop();
                    GetComponent<ParticleSystem>().Play();
                }
                break;

            case SkillDir.Forward:
                // Rigidbody를 사용하여 특정 방향(dir)으로 스킬을 이동시키는 코드 (현재 주석 처리됨)
                // rigidbody.velocity = dir * skillTable.speed; // 데이터 테이블의 스피드 값으로 가져옴
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Define.Item.Lumi.ToString()))
        {
            // 디버그 메시지를 출력하여 충돌한 오브젝트의 태그 확인
            Debug.Log("other Name tag: " + other.gameObject.tag);

            // Lumi 아이템의 GetItem() 메서드를 실행하여 해당 아이템을 획득 처리
            other.gameObject.GetComponent<ItemLumi>().GetItem();
        }
    }

    private void Update()
    {
    }
}
