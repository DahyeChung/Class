
public class GameScene : BaseScene
{
    GameManager _game;

    // TODO : [Dahye] Every UI happening in the GameScene.
    // UI_GameScene _ui;

    /// <summary>
    /// Execute very first
    /// </summary>
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.GameScene;
        //TitleUI

        _game = Managers.Game;

        // Managers.UI.ShowSceneUI<UI_Joystick>();
    }

    public override void Clear()
    {

    }
}
