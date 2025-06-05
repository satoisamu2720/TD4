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
        if (TextBoxController.IsTalking) return; // 会話中は入力無効
        if (target == null || cam == null) return;

        Vector3 worldTarget = target.position + offsetAboveTarget;

        //// 距離でUI表示制御
        //float worldDistance = Vector3.Distance(transform.position, target.position);
        //if (uiObject != null)
        //    uiObject.SetActive(worldDistance > arriveDistance);

        // 目的地が画面内か
        //float worldDistance = Vector3.Distance(transform.position, target.position);
        //if (uiObject != null)
        //    uiObject.SetActive(worldDistance > arriveDistance);

        // 目的地が画面内か
        //float worldDistance = Vector3.Distance(transform.position, target.position);
        //if (uiObject != null)
                // 到達していない、または新しいターゲットに切り替えたばかりの時は移動

        // 目的地が画面内か
        //float worldDistance = Vector3.Distance(transform.position, target.position);
        //if (uiObject != null)
        //    uiObject.SetActive(worldDistance > arriveDistance);

        // 目的地が画面内か
        Vector3 viewportPos = cam.WorldToViewportPoint(target.position);

        bool targetIsOnScreen = viewportPos.x >= 0 && viewportPos.x <= 1 &&
                                viewportPos.y >= 0 && viewportPos.y <= 1 &&
                                viewportPos.z >= 0;

        if (targetIsOnScreen)
               
                isSnapped = true;
                // 到達後バウンド処理

            if (dist > snapDistance || !isSnapped)
            {
                // 「少し上」へ移動（スムーズに近づく）
                transform.position = Vector3.MoveTowards(transform.position, worldTarget, speed * Time.deltaTime);
           
            transform.rotation = Quaternion.Euler(0, 0, -90f);
                if (Vector3.Distance(transform.position, worldTarget) <= snapDistance)
                {
                    isSnapped = true;
                }
                else
           
                    isSnapped = false;
                }
            }
            else
           
            Vector2 moveDirection = transform.right;
            Vector3 moveDelta = (Vector3)(moveDirection * speed * Time.deltaTime);
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
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
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
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
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