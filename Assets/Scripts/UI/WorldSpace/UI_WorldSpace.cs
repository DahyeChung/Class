using UnityEngine;

public class UI_WorldSpace : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main ? Camera.main.transform : null;

        if (cameraTransform == null)
        {
            Debug.LogWarning("Main Camera missing. Set the target camera");
        }
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 lookDirection = cameraTransform.forward;
        transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
}
