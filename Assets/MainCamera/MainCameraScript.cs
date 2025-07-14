using UnityEngine;

public class MainCameraScript: MonoBehaviour
{

    [SerializeField]
    private Transform target;  // 追いかける対象（プレイヤーなど）

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, -15f); // カメラの位置のずれ

    [SerializeField, Range(0.01f, 1f)]
    private float smoothSpeed = 0.125f; // 遅延のスピード（小さいとゆっくり追従）

    private static MainCameraScript instance;

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
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