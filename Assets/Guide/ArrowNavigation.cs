using UnityEngine;

public class ArrowNavigation : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotationSpeed = 200f;
    public Vector2 screenMargin = new Vector2(0.5f, 0.5f);
    public float arriveDistance = 1f;
    public GameObject uiObject;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (target == null || cam == null)
            return;

        // 距離でUIの表示/非表示を切り替え
        float distance = Vector3.Distance(transform.position, target.position);
        if (uiObject != null)
            uiObject.SetActive(distance > arriveDistance);

        // 「目的地が画面内にあるか」を判定
        Vector3 viewportPos = cam.WorldToViewportPoint(target.position);
        bool targetIsOnScreen = viewportPos.x >= 0 && viewportPos.x <= 1 &&
                                viewportPos.y >= 0 && viewportPos.y <= 1 &&
                                viewportPos.z >= 0;

        if (targetIsOnScreen)
        {
            // 目的地が画面内 → 矢印を目的地の位置に固定
            transform.position = target.position;
        }
        else
        {
            // 回転処理（目的地方向に向ける）
            Vector2 direction = (target.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float currentAngle = transform.eulerAngles.z;
            float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // 移動処理
            Vector2 moveDirection = transform.right;
            Vector3 moveDelta = (Vector3)(moveDirection * speed * Time.deltaTime);
            Vector3 nextPosition = transform.position + moveDelta;

            // 画面外にいても安全に制限をかける（オプション）
            Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            bottomLeft += new Vector3(screenMargin.x, screenMargin.y, 0);
            topRight -= new Vector3(screenMargin.x, screenMargin.y, 0);

            float clampedX = Mathf.Clamp(nextPosition.x, bottomLeft.x, topRight.x);
            float clampedY = Mathf.Clamp(nextPosition.y, bottomLeft.y, topRight.y);
            Vector3 clampedPosition = new Vector3(clampedX, clampedY, nextPosition.z);

            transform.position = clampedPosition;
        }
    }
}
