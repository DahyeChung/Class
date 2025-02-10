using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.LobbyScene;


        // Create UI prefab and add it under the @Root.
        // Managers.UI.ShowSceneUI<UI_LobbyScene>();
        Screen.sleepTimeout = SleepTimeout.SystemSetting;

        // Managers.Sound.Play(Define.Sound.Bgm, "Bgm_Lobby");
    }

    public override void Clear()
    {

    }
}
