public class UI_SettingPopup : UI_Popup
{

    //#region Enum
    //enum GameObjects
    //{
    //    ContentObject,
    //    //LanguageObject
    //}
    //enum Buttons
    //{
    //    BackButton,
    //    SoundEffectOffButton,
    //    SoundEffectOnButton,
    //    BackgroundSoundOffButton,
    //    BackgroundSoundOnButton,
    //}

    //enum Texts
    //{
    //    SettingTlileText,
    //    SoundEffectText,
    //    BackgroundSoundText,
    //}
    //#endregion
    //private void Awake()
    //{
    //    Init();
    //}
    //private void OnEnable()
    //{
    //    PopupOpenAnimation(GetObject((int)GameObjects.ContentObject));
    //}

    //public override bool Init()
    //{
    //    if (base.Init() == false)
    //        return false;
    //    #region Object Bind
    //    BindObject(typeof(GameObjects));
    //    BindButton(typeof(Buttons));
    //    BindText(typeof(Texts));

    //    GetButton((int)Buttons.BackButton).gameObject.BindEvent(OnClickBackgroundButton);

    //    GetButton((int)Buttons.SoundEffectOffButton).gameObject.BindEvent(EffectSoundOn);
    //    GetButton((int)Buttons.SoundEffectOnButton).gameObject.BindEvent(EffectSoundOff);

    //    GetButton((int)Buttons.BackgroundSoundOffButton).gameObject.BindEvent(BackgroundSoundOn);
    //    GetButton((int)Buttons.BackgroundSoundOnButton).gameObject.BindEvent(BackgroundSoundOff);


    //    //if (Managers.Game.BGMOn == false)
    //    //{
    //    //    BackgroundSoundOff();
    //    //}
    //    //else
    //    //{
    //    //    BackgroundSoundOn();
    //    //}

    //    //if (Managers.Game.EffectSoundOn == false)
    //    //{
    //    //    EffectSoundOff();
    //    //}
    //    //else
    //    //{
    //    //    EffectSoundOn();
    //    //}


    //    #endregion

    //    Refresh();
    //    return true;
    //}

    //public void SetInfo()
    //{

    //    Refresh();
    //}

    //void Refresh()
    //{



    //}

    //void EffectSoundOn()
    //{
    //    // Managers.Sound.PlayButtonClick();
    //    // Managers.Game.EffectSoundOn = true;
    //    GetButton((int)Buttons.SoundEffectOnButton).gameObject.SetActive(true);
    //    GetButton((int)Buttons.SoundEffectOffButton).gameObject.SetActive(false);
    //}

    //void EffectSoundOff()
    //{
    //    // Managers.Sound.PlayButtonClick();
    //    // Managers.Game.EffectSoundOn = false;
    //    GetButton((int)Buttons.SoundEffectOnButton).gameObject.SetActive(false);
    //    GetButton((int)Buttons.SoundEffectOffButton).gameObject.SetActive(true);
    //}

    //void BackgroundSoundOn()
    //{
    //    //Managers.Sound.PlayButtonClick();
    //    //Managers.Game.BGMOn = true;
    //    GetButton((int)Buttons.BackgroundSoundOnButton).gameObject.SetActive(true);
    //    GetButton((int)Buttons.BackgroundSoundOffButton).gameObject.SetActive(false);
    //}

    //void BackgroundSoundOff()
    //{
    //    //Managers.Sound.PlayButtonClick();
    //    //Managers.Game.BGMOn = false;
    //    GetButton((int)Buttons.BackgroundSoundOnButton).gameObject.SetActive(false);
    //    GetButton((int)Buttons.BackgroundSoundOffButton).gameObject.SetActive(true);
    //}


    //void OnClickBackgroundButton()
    //{
    //    Managers.UI.ClosePopupUI(this);
    //}

}
