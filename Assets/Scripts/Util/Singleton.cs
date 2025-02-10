using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }

            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<T>();
                singleton.name = "(Singleton) " + typeof(T).ToString();
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = (T)this;
    }

}
