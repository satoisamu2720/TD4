using UnityEngine;

public class GuideScript:MonoBehaviour
{
    [SerializeField]
    public GameObject target; // 対象のGameObjectへの参照
    public GameObject Camera; // 稼働領域
    public float speed = 1f;            // 突っ込みスピード
    public int maxHP = 2;               // 最大HP
    private int currentHP;              // 現在のHP

    private Transform destination;           // プレイヤーのTransform



    void Start()
    {
        destination = GameObject.FindWithTag("Destination")?.transform;
        currentHP = maxHP;
    }

    void Update()
    {
        RotateTowardsTarget();
        if (destination != null)
        {
            transform.position = Vector3.MoveTowards(
           transform.position,
           target.transform.position,
           speed * Time.deltaTime);

            transform.position = new Vector2(
            //移動範囲を制限する
            Mathf.Clamp(transform.position.x, Camera.transform.position.x - 8.3f, Camera.transform.position.x + 8.3f),
            Mathf.Clamp(transform.position.y, Camera.transform.position.y - 4.4f, Camera.transform.position.y + 4.4f)
            );
        }
    }

  

    private void RotateTowardsTarget()
    {
        // ターゲットが設定されていなければ、処理をスキップ
        if (target == null) return;

        // ターゲットへの方向を算出
        Vector3 targetDirection = CalculateDirectionTowardsTarget();

        // ターゲットの方向へ回転させる
        AlignRotationToTarget(targetDirection);
    }

    private Vector3 CalculateDirectionTowardsTarget()
    {
        return target.transform.position - transform.position;
    }

    private void AlignRotationToTarget(Vector3 direction)
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }
}
