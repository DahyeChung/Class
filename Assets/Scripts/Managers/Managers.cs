using UnityEngine;

/// <summary>
/// Manage all Managers
/// </summary>
public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }


    #region Managers List
    public static GameManager Game { get { return Instance?._game; } }
    public static SceneManagerEx Scene { get { return Instance?._scene; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static SoundManager Sound { get { return Instance?._sound; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static DataManager Data { get { return Instance?._data; } }



    DataManager _data = new DataManager();
    GameManager _game = new GameManager();
    SceneManagerEx _scene = new SceneManagerEx();
    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    #endregion



    public static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._sound.Init();
            s_instance._data.Init();
        }
    }

    public static void Clear()
    {
        // TODO : [Dahye] Add later

        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        //Object.Clear();
    }


}
