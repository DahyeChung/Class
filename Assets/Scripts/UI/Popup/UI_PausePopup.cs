using Unity.VisualScripting;
using UnityEngine;

public class UI_PausePopup : UI_Popup
{


    #region Enum

    enum GameObjects
    {
        ContentObject,
    }
    enum Buttons
    {
        ResumeButton,
        RestartButton,
        QuitButton,
    }


    enum Texts
    {
        ResumeText,
        RestartText,
        QuitText,
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
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.ResumeButton).gameObject.BindEvent(OnClickResumeButton);
        GetButton((int)Buttons.ResumeButton).GetOrAddComponent<UI_ButtonAnimation>();
        GetButton((int)Buttons.RestartButton).gameObject.BindEvent(OnClickRestartButton);
        GetButton((int)Buttons.RestartButton).GetOrAddComponent<UI_ButtonAnimation>();
        GetButton((int)Buttons.QuitButton).gameObject.BindEvent(OnClickQuitButton);
        GetButton((int)Buttons.QuitButton).GetOrAddComponent<UI_ButtonAnimation>();
        #endregion
        return true;
    }



    void OnClickResumeButton()
    {
        Debug.Log("Resume Button Clicked");
        Managers.UI.ClosePopupUI(this);
    }

    void OnClickRestartButton()
    {
        // Play sound 
        // TODO : [Dahye] Back to Lobby button if there's any lobby

        Debug.Log("Restart Button Clicked");
    }

    void OnClickQuitButton()
    {
        // Play sound 
        // TODO : [Dahye] ShowPopupUI() check again @UIManager
        Debug.Log("Quit Button Clicked");
    }

}
