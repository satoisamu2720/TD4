using UnityEngine;

public class UIFollowWorldObject : MonoBehaviour
{
    public Transform target;
    public RectTransform uiElement;
    public Camera mainCamera;


    void Update()
    {
        if (target == null || mainCamera == null || uiElement == null)
            return;

        // UIˆÊ’u‚ğƒ[ƒ‹ƒh‚É’Ç]
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        uiElement.position = screenPos;

        // Œü‚«‚à“¯Šú
        transform.rotation = target.rotation;
    }
}
