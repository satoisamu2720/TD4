using UnityEngine;

public class UIFollowWorldObject : MonoBehaviour
{
    public Transform target;
    public RectTransform uiElement;
    public Camera mainCamera;

    private static UIFollowWorldObject instance;
    void Update()
    {
        if (target == null || mainCamera == null || uiElement == null)
            return;

        // UI位置をワールドに追従
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        uiElement.position = screenPos;

        // 向きも同期
        transform.rotation = target.rotation;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // これでシーンをまたいでも残る
        }
        else
        {
            Destroy(gameObject); // 2個目のカメラができたら破棄する
        }
    }
}