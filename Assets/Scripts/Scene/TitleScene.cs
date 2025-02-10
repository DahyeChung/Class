using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.TitleScene;
        Managers.Sound.Play(Define.Sound.Bgm, "BGM/Sample_TitleBGM_Josh");
        Debug.Log("TitleScene Init");
        //TitleUI
    }

    public override void Clear()
    {

    }
}
