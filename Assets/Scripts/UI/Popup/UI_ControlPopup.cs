using Unity.VisualScripting;

public class UI_ControlPopup : UI_Popup
{


    #region Enum

    enum GameObjects
    {
        ContentObject,
    }
    enum Buttons
    {
        BackButton,
    }

    #endregion

    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        PopupOpenAnimation(GetObject((int)GameObjects.ContentObject));
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        #region Object Bind
        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.BackButton).gameObject.BindEvent(OnClickBackButton);
        GetButton((int)Buttons.BackButton).GetOrAddComponent<UI_ButtonAnimation>();
        #endregion
        return true;
    }



    void OnClickBackButton()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
