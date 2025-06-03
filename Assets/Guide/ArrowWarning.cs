using UnityEngine;

public class ArrowWarning : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotationSpeed = 200f;
    public Vector2 screenMargin = new Vector2(0.5f, 0.5f);
    public float arriveDistance = 1f;
    public GameObject uiObject;
    public Vector3 offsetAboveTarget = new Vector3(0, 1f, 0);
    public float bounceAmplitude = 0.3f;
    public float bounceSpeed = 3f;
    public float snapDistance = 0.05f;

    private Camera cam;
    private bool isSnapped = false;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (target == null || cam == null || uiObject == null)
            return;

        // ターゲットのワールド位置
        Vector3 worldTarget = target.position + offsetAboveTarget;

        // ビューポート座標でターゲットが画面内かチェック
        Vector3 viewportPos = cam.WorldToViewportPoint(target.position);
        bool targetIsOnScreen = viewportPos.x >= 0 && viewportPos.x <= 1 &&
                                viewportPos.y >= 0 && viewportPos.y <= 1 &&
                                viewportPos.z > 0;

        // UI 表示切り替え
        uiObject.SetActive(!targetIsOnScreen);

        if (targetIsOnScreen)
        {
            // ターゲットが画面内 → 非表示 & 位置変更なし
            return;
        }

        // --- ターゲットが画面外のときのみ以下の処理を実行 ---

        // 方向と回転をターゲットに向ける
        Vector2 direction = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;
        //float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 90f);

        // 前進
        Vector2 moveDirection = transform.right;
        Vector3 moveDelta = (Vector3)(moveDirection * speed * Time.deltaTime);
        Vector3 nextPosition = transform.position + moveDelta;

        // 画面端制限
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, Mathf.Abs(cam.transform.position.z)));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, Mathf.Abs(cam.transform.position.z)));
        bottomLeft += new Vector3(screenMargin.x, screenMargin.y, 0);
        topRight -= new Vector3(screenMargin.x, screenMargin.y, 0);

        float clampedX = Mathf.Clamp(nextPosition.x, bottomLeft.x, topRight.x);
        float clampedY = Mathf.Clamp(nextPosition.y, bottomLeft.y, topRight.y);
        transform.position = new Vector3(clampedX, clampedY, nextPosition.z);
    }
}