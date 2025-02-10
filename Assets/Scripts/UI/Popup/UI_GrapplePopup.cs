public class UI_GrapplePopup : UI_Popup
{
    enum GameObjects
    {
        GrappleImage,
    }

    private void Awake()
    {
        Init();
        // Create Grapple Prefab UI 
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        #region Object Bind
        BindObject(typeof(GameObjects));
        #endregion
        return true;
    }

}
