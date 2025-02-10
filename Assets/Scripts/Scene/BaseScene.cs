using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Define scene type & create EventSystem in the scene.
/// </summary>
public abstract class BaseScene : MonoBehaviour
{
    // Must define scene type in the derived class. 
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));


        if (obj == null)
        {
            GameObject eventSystem = new GameObject("@EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();

            // Simplified
            // var eventSystem = new GameObject("@EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

    }


    /// <summary>
    /// Clear all the unnecessary managers in the scene.
    /// </summary>
    /// // Must be implemented in the derived class.
    public abstract void Clear();
}
