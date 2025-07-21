using UnityEngine;

public class ArrowNavigation : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float arriveDistance = 1f;
    public float bounceAmplitude = 0.3f;
    public float bounceSpeed = 3f;
    public float snapDistance = 0.05f;
    public Vector3 offsetAboveTarget = new Vector3(0, 1f, 0);
    public Vector2 screenMargin = new Vector2(0.5f, 0.5f);

    private Camera cam;
    private bool isSnapped = false;

    private static ArrowNavigation instance;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (GameManagement.Instance != null && GameManagement.Instance.isPause == false)
        {
            if (target == null || cam == null) return;

            Vector3 worldTarget = target.position + offsetAboveTarget;
            Vector3 viewportPos = cam.WorldToViewportPoint(target.position);

            bool targetIsOnScreen = viewportPos.x >= 0 && viewportPos.x <= 1 &&
                                    viewportPos.y >= 0 && viewportPos.y <= 1 &&
                                    viewportPos.z >= 0;

            if (targetIsOnScreen)
            {
                float dist = Vector3.Distance(transform.position, worldTarget);

                if (dist > snapDistance || !isSnapped)
                {
                    // 到達していない、または新しいターゲットに切り替えたばかりの時は移動
                    transform.position = Vector3.MoveTowards(transform.position, worldTarget, speed * Time.deltaTime);

                    // 到達チェック（次のフレームからバウンド可）
                    if (Vector3.Distance(transform.position, worldTarget) <= snapDistance)
                    {
                        isSnapped = true;
                    }
                    else
                    {
                        isSnapped = false;
                    }
                }
                else
                {
                    // 到達後バウンド処理
                    float bounceY = Mathf.Sin(Time.time * bounceSpeed) * bounceAmplitude;
                    transform.position = worldTarget + new Vector3(0, bounceY, 0);
                    transform.rotation = Quaternion.Euler(0, 0, -90f);
                }

            }
            else
            {
                isSnapped = false;

                // プレイヤーの位置を中心に、一定距離だけ矢印を表示
                Transform player = GameObject.FindWithTag("Player")?.transform;
                if (player != null)
                {
                    Vector2 direction = (target.position - player.position).normalized;

                    // プレイヤーの周囲に矢印を配置
                    float radius = 4f;
                    Vector3 offset = new Vector3(direction.x, direction.y, 0) * radius;
                    transform.position = player.position + offset;

                    // 向きをターゲットの方向に合わせる
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                }
            }
        }
    }

    // ターゲットを外部から切り替える用
    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null)
        {
            target = null;
            return;
        }

        target = newTarget;
        isSnapped = false;

        // 強制的にworldTargetから少し離す（即バウンドを防ぐ）
        Vector3 worldTarget = target.position + offsetAboveTarget;
        if (Vector3.Distance(transform.position, worldTarget) <= snapDistance)
        {
            // 真上に強制移動させて距離を稼ぐ（方向はお好みで）
            transform.position = worldTarget + new Vector3(0.5f, 1f, 0);
        }
    }



    // 到達判定（チュートリアル制御用）
    public bool HasArrived()
    {
        if (target == null) return false;

        // バウンド中以外のときのみ到達とみなす
        return !isSnapped && Vector3.Distance(transform.position, target.position + offsetAboveTarget) <= snapDistance;
    }

}