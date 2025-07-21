using UnityEngine;

public class UIFollowWorldObject : MonoBehaviour
{
    public Transform target;
    public RectTransform uiElement;
    public Camera mainCamera;

    public bool shouldShowUI = false;


    public static UIFollowWorldObject Instance { get; private set; }
    void Update()
    {
        if (target == null || mainCamera == null || uiElement == null)
            return;

        uiElement.gameObject.SetActive(shouldShowUI);

        if (!shouldShowUI)
            return;
        // UIà íuÇÉèÅ[ÉãÉhÇ…í«è]
        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        uiElement.position = screenPos;

        // å¸Ç´Ç‡ìØä˙
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

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

}