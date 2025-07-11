using UnityEngine;

public class UIFollowWorldObject : MonoBehaviour
{
    public Transform target;
    public RectTransform uiElement;
    public Camera mainCamera;

    public bool shouldShowUI = false;


    private static UIFollowWorldObject instance;
    void Update()
    {
        if (target == null || mainCamera == null || uiElement == null)
            return;

        uiElement.gameObject.SetActive(shouldShowUI);

        if (!shouldShowUI)
            return;
        // UI位置をワールドに追従
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        uiElement.position = screenPos;

        // 向きも同期
        transform.rotation = target.rotation;



    }
    public void ShowUI(bool show)
    {
        shouldShowUI = show;

        if (uiElement != null)
        {
            uiElement.gameObject.SetActive(show);
        }
    }

    
    public bool IsUIVisible()
    {
        return shouldShowUI;
    }


    public static UIFollowWorldObject GetInstance()
    {
        return instance;
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