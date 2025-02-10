using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    /// <summary>
    /// Load scene in a particular condition. 
    /// ex) Managers.Scene.LoadScene(Define.Scene.GameScene, transform);
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parents"></param>
    public void LoadScene(Define.Scene type, Transform parents = null)
    {
        switch (CurrentScene.SceneType)
        {
            case Define.Scene.TitleScene:
                Managers.Clear();
                SceneManager.LoadScene(GetSceneName(type));
                break;
            case Define.Scene.GameScene:
                // TODO : [Dahye] Scene Transfer Animation? 

                //Time.timeScale = 1;

                // TODO : [Dahye] If animation is finished, then do below.
                //    Managers.Resource.Destroy(Managers.UI.SceneUI.gameObject);
                //    Managers.Clear();
                //    SceneManager.LoadScene(GetSceneName(type));
                break;
            case Define.Scene.LobbyScene:
                // TODO : [Dahye] Scene Transfer Animation? 

                //Time.timeScale = 1;

                // TODO : [Dahye] If animation is finished, then do below.
                //    Managers.Resource.Destroy(Managers.UI.SceneUI.gameObject);
                //    Managers.Clear();
                //    SceneManager.LoadScene(GetSceneName(type));
                break;
        }

    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
