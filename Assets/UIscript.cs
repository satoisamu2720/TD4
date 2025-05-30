using UnityEngine;

public class UIscript : MonoBehaviour
{
    public Transform target;           // 敵
    public RectTransform arrowUI;      // 矢印Image（UI）
    public Camera mainCam;
    public RectTransform canvasRect;   // キャンバスのRectTransform

    void Update()
    {
        if (target == null || arrowUI == null || mainCam == null || canvasRect == null) return;

        Vector3 viewportPos = mainCam.WorldToViewportPoint(target.position);

        // 敵が画面外か判定
        bool isOffScreen = viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1 || viewportPos.z < 0;

        arrowUI.gameObject.SetActive(isOffScreen);

        if (isOffScreen)
        {
            // 0~1の範囲に制限（クランプ）
            float clampedX = Mathf.Clamp01(viewportPos.x);
            float clampedY = Mathf.Clamp01(viewportPos.y);

            float canvasWidth = canvasRect.rect.width;
            float canvasHeight = canvasRect.rect.height;

            // 左下(0,0)基準のViewportを、キャンバス中心(0,0)基準の座標に変換
            Vector2 pointerPos = new Vector2(
                (clampedX - 0.5f) * canvasWidth,
                (clampedY - 0.5f) * canvasHeight
            );

            // 画面端から50px内側に矢印を表示
            float maxX = canvasWidth / 2f - 50f;
            float maxY = canvasHeight / 2f - 50f;

            pointerPos.x = Mathf.Clamp(pointerPos.x, -maxX, maxX);
            pointerPos.y = Mathf.Clamp(pointerPos.y, -maxY, maxY);

            arrowUI.anchoredPosition = pointerPos;

            // 矢印の向きは敵の方向へ
            Vector3 dir = (target.position - mainCam.transform.position).normalized;
            dir.z = 0;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }
}
