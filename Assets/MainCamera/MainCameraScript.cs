using UnityEngine;
using System.Collections;
public class MainCameraScript: MonoBehaviour
{
    public static MainCameraScript Instance { get; private set; }
    //[SerializeField]
    private Transform target;  // 追いかける対象（プレイヤーなど）

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, -15f); // カメラの位置のずれ

    [SerializeField, Range(0.01f, 1f)]
    private float smoothSpeed = 0.125f; // 遅延のスピード（小さいとゆっくり追従）

    private bool isFocusing = false;
    private float focusDuration = 3f;
    private float focusSpeed = 3f;

    private Transform defaultTarget; // 通常追従するプレイヤーなどのターゲット

    public void Start()
    {
        // プレイヤーのTransformを取得
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SetDefaultTarget(player.transform);
        }
    }
    void FixedUpdate()
    {
        if (target == null) 
        { 
            return; 
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    public void SetDefaultTarget(Transform newTarget)
    {
        defaultTarget = newTarget;
        target = newTarget;
    }

    public void FocusOn(Transform focusTarget, float duration = 3f)
    {
        if (!isFocusing)
        {
            focusDuration = duration;
            StartCoroutine(FocusCoroutine(focusTarget));
        }
    }

    private IEnumerator FocusCoroutine(Transform focusTarget)
    {
        isFocusing = true;
        GameManagement.Instance.isPause = true;
        target = focusTarget;

        yield return new WaitForSeconds(focusDuration);

        // 元のターゲットに戻す
        if (defaultTarget != null)
        {
            target = defaultTarget;
        }
        GameManagement.Instance.isPause = false;
        isFocusing = false;
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