using UnityEngine;

public class ArrowNavigation : MonoBehaviour
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
    public float snapDistance = 0.05f; // バウンド開始と判断する距離

    private Camera cam;
    private bool isSnapped = false; 

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (target == null || cam == null)
            return;

        Vector3 worldTarget = target.position + offsetAboveTarget;

        //// 距離でUI表示制御
        //float worldDistance = Vector3.Distance(transform.position, target.position);
        //if (uiObject != null)
        //    uiObject.SetActive(worldDistance > arriveDistance);

        // 目的地が画面内か
        Vector3 viewportPos = cam.WorldToViewportPoint(target.position);
        bool targetIsOnScreen = viewportPos.x >= 0 && viewportPos.x <= 1 &&
                                viewportPos.y >= 0 && viewportPos.y <= 1 &&
                                viewportPos.z >= 0;

        if (targetIsOnScreen)
        {
            float dist = Vector3.Distance(transform.position, worldTarget);
            if (dist > snapDistance)
            {
                // 「少し上」へ移動（スムーズに近づく）
                transform.position = Vector3.MoveTowards(transform.position, worldTarget, speed * Time.deltaTime);
                isSnapped = false;
            }
            else
            {
                // 到達後に上下にバウンド
                isSnapped = true;
                float bounceY = Mathf.Sin(Time.time * bounceSpeed) * bounceAmplitude;
                transform.position = worldTarget + new Vector3(0, bounceY, 0);
            }

            // 向きは下向きに固定
            transform.rotation = Quaternion.Euler(0, 0, -90f);
        }
        else
        {
            isSnapped = false;

            // 方向ベクトルと回転
            Vector2 direction = (target.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float currentAngle = transform.eulerAngles.z;
            float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // 移動処理
            Vector2 moveDirection = transform.right;
            Vector3 moveDelta = (Vector3)(moveDirection * speed * Time.deltaTime);
            Vector3 nextPosition = transform.position + moveDelta;

            // 画面端制限
            Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
            bottomLeft += new Vector3(screenMargin.x, screenMargin.y, 0);
            topRight -= new Vector3(screenMargin.x, screenMargin.y, 0);

            float clampedX = Mathf.Clamp(nextPosition.x, bottomLeft.x, topRight.x);
            float clampedY = Mathf.Clamp(nextPosition.y, bottomLeft.y, topRight.y);
            transform.position = new Vector3(clampedX, clampedY, nextPosition.z);
        }
    }
}
