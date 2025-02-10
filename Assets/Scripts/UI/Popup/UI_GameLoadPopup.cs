using Unity.VisualScripting;
using UnityEngine;

public class UI_GameLoadPopup : UI_Popup
{
    enum GameObjects
    {
        ContentObject,
    }
    enum Buttons
    {
        NewButton,
        LoadButton,
        ExitButton,
    }

    private void Awake()
    {
        Init();
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        #region Object Bind
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.NewButton).gameObject.BindEvent(OnClickNewButton);
        GetButton((int)Buttons.NewButton).GetOrAddComponent<UI_ButtonAnimation>();
        GetButton((int)Buttons.LoadButton).gameObject.BindEvent(OnClickLoadButton);
        GetButton((int)Buttons.LoadButton).GetOrAddComponent<UI_ButtonAnimation>();
        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(OnClickExitButton);
        GetButton((int)Buttons.ExitButton).GetOrAddComponent<UI_ButtonAnimation>();


        #endregion
        return true;
    }

    void OnClickNewButton()
    {   // Check saved  data
        // Managers.Scene.LoadScene(Define.Scene.GameScene);
    }

    void OnClickLoadButton()
    {
        // Load Data
    }
    void OnClickExitButton()
    {
        Application.Quit();
    }
}
