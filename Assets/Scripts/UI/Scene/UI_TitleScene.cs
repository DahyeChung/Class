using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class UI_TitleScene : UI_Scene
{
    #region Enum 
    // Enum {Button, text, object} should have the same name as the object in the hierarchy.
    // Will bind with the objects, buttons, and texts to be used in the inspector and connect them with actions.
    //enum GameObjects
    //{
    //}

    enum Buttons
    {
        StartButton,
        OptionButton,
        ControlButton,
    }

    enum Texts
    {
        StartText,
        TitleText,

    }
    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        // BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        // TODO : [Dahye] Which scene to transfer from the title scene? 

        // Move to white box 1
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(() =>
        {
            Debug.Log("Start Button Clicked");
            // Managers.Scene.LoadScene(Define.Scene.WB1, transform);
        });
        GetButton((int)Buttons.StartButton).gameObject.SetActive(false);


        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);
        GetButton((int)Buttons.OptionButton).gameObject.BindEvent(OnClickOptionButton);
        GetButton((int)Buttons.OptionButton).GetOrAddComponent<UI_ButtonAnimation>();
        GetButton((int)Buttons.ControlButton).gameObject.BindEvent(OnClickControlButton);
        GetButton((int)Buttons.ControlButton).GetOrAddComponent<UI_ButtonAnimation>();

        return true;
    }

    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        GetButton((int)Buttons.StartButton).gameObject.SetActive(false);
        GetButton((int)Buttons.StartButton).gameObject.SetActive(true);

        //        Managers.Game.Init();
        //        Managers.Time.Init();
        StartButtonAnimation();

    }

    // Text fade animation
    void StartButtonAnimation()
    {
        GetText((int)Texts.StartText).DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic).Play();
    }

    void OnClickStartButton()
    {
        //Managers.UI.ShowPopupUI<UI_GameLoadPopup>();
    }
    void OnClickOptionButton()
    {
        //GetText((int)Texts.TitleText).gameObject.SetActive(false);
        //GetButton((int)Buttons.StartButton).gameObject.SetActive(false);
        Managers.UI.ShowPopupUI<UI_OptionPopup>();
    }

    void OnClickControlButton()
    {
        //GetText((int)Texts.TitleText).gameObject.SetActive(false);
        //GetButton((int)Buttons.StartButton).gameObject.SetActive(false);
        Managers.UI.ShowPopupUI<UI_ControlPopup>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Option Button Clicked");
            Managers.UI.ShowPopupUI<UI_OptionPopup>();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("Control Button Clicked");
            Managers.UI.ShowPopupUI<UI_ControlPopup>();
        }
    }
#endif
}
