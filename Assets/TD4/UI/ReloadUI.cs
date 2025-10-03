using UnityEngine;
using UnityEngine.UI;

public class ReloadUI : MonoBehaviour
{
    public Image reloadCircleImage;
    public Image reloadGageImage;

    private float reloadDuration;
    private float timer;
    private bool isReloading;

    private Color visibleColor;
    public Transform playerTransform;         
    public Vector3 worldOffset = new Vector3(1.5f, 1.5f, 0);
    private RectTransform uiRect;
    private RectTransform GageRect;

    void Start()
    {
        uiRect = reloadCircleImage.GetComponent<RectTransform>();
        GageRect = reloadGageImage.GetComponent<RectTransform>();
        reloadCircleImage.gameObject.SetActive(false);
        reloadGageImage.gameObject.SetActive(false);
    }
    public void StartReload(float duration)
    {
        reloadDuration = duration;
        timer = 0f;
        isReloading = true;

      
        visibleColor = reloadCircleImage.color;
        visibleColor.a = 1f;

        reloadCircleImage.fillAmount = 0f;
        reloadCircleImage.color = visibleColor;
        reloadCircleImage.gameObject.SetActive(true);
        reloadGageImage.gameObject.SetActive(true);
        
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(playerTransform.position + worldOffset);
            uiRect.position = screenPos;
            GageRect.position = screenPos;
            uiRect.rotation = Quaternion.identity;         
        }
        if (isReloading)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / reloadDuration);

            
            reloadCircleImage.fillAmount = progress;

            if (progress >= 1f)
            {
                isReloading = false;
                reloadCircleImage.gameObject.SetActive(false);
                reloadGageImage.gameObject.SetActive(false); 
            }
        }
    }
}
