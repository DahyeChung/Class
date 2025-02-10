using UnityEngine;

public class UI_Grapple : UI_Base
{
    enum GameObjects
    {
        GrappleImage,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        Bind<GameObject>(typeof(GameObjects));
        return true;
    }

    private void Update()
    {
        Transform parent = transform.parent;
        //transform.position = Camera.main.WorldToScreenPoint(parent.position - Vector3.up * 1.2f);
        transform.rotation = Camera.main.transform.rotation;

    }
}
