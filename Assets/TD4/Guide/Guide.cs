using UnityEngine;

public class Guide : MonoBehaviour
{
    public Transform target;             // 目的地
    public Camera mainCamera;            // メインカメラ
    public RectTransform arrowUI;        // 矢印UI
    public RectTransform canvasRect;     // CanvasのRectTransform

    public float hideDistance = 1.0f;    // 目的地に近づいたら非表示

    private bool isArrowVisible = true;  // 矢印の表示状態を管理

    void Update()
    {
        if (target == null) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 fromCenter = new Vector2(screenPos.x, screenPos.y) - screenCenter;
        Vector2 dir = fromCenter.normalized;

        // 矢印と目的地の距離をスクリーン座標で測る
        Vector2 arrowScreenPos = arrowUI.anchoredPosition + screenCenter;
        float distanceScreen = Vector2.Distance(arrowScreenPos, new Vector2(screenPos.x, screenPos.y));

        // 非表示にするかどうか判定
        bool shouldBeVisible = distanceScreen > hideDistance * 100;

        // 表示状態が変わったらSetActiveを呼ぶ
        if (shouldBeVisible != isArrowVisible)
        {
            arrowUI.gameObject.SetActive(shouldBeVisible);
            isArrowVisible = shouldBeVisible;
        }

        // 非表示なら以降の処理をスキップ（位置・回転の更新しない）
        if (!isArrowVisible) return;

        // 画面端に矢印を表示（四辺の座標に制限）
        float halfWidth = canvasRect.rect.width / 2f;
        float halfHeight = canvasRect.rect.height / 2f;
        float m = dir.y / dir.x;
        Vector2 pos;

        if (dir.x > 0)
        {
            pos.x = halfWidth;
            pos.y = halfWidth * m;

            if (pos.y > halfHeight)
            {
                pos.y = halfHeight;
                pos.x = halfHeight / m;
            }
            else if (pos.y < -halfHeight)
            {
                pos.y = -halfHeight;
                pos.x = -halfHeight / m;
            }
        }
        else
        {
            pos.x = -halfWidth;
            pos.y = -halfWidth * m;

            if (pos.y > halfHeight)
            {
                pos.y = halfHeight;
                pos.x = halfHeight / m;
            }
            else if (pos.y < -halfHeight)
            {
                pos.y = -halfHeight;
                pos.x = -halfHeight / m;
            }
        }

        arrowUI.anchoredPosition = pos;

        // 矢印の向きを目的地方向に合わせる
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        arrowUI.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}
