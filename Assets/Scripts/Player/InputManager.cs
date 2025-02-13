using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Gadgets
    public static event Action OnKey1Pressed;
    public static event Action OnKey2Pressed;
    public static event Action OnKey3Pressed;
    public static event Action OnKeyEPressed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) OnKey1Pressed?.Invoke();
        if (Input.GetKeyDown(KeyCode.Alpha2)) OnKey2Pressed?.Invoke();
        if (Input.GetKeyDown(KeyCode.Alpha3)) OnKey3Pressed?.Invoke();
        if (Input.GetKeyDown(KeyCode.E)) OnKeyEPressed?.Invoke();
    }
    #endregion
}
