
public class Firefly : SkillBase
{
    public override void Shot()
    {
        switch (skillDir)
        {
            case SkillDir.Forward:
                // _rigidBody.velocity = skillDir * skillTable.speed; TODO : check skillTable.speed and add 

                break;
            default:
                break;
        }
    }
    private void Update()
    {
        // 바라보는 방향으로 날아가기 
        // Rigidbody velocity? 
    }
}
