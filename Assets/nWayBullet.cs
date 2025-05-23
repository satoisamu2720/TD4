using UnityEngine;

public class nWayBullet : MonoBehaviour
{
    public GameObject Bullet;
    public float _Velocity_0 = 5f;      // 弾の速度
    public float Degree = 60f;          // 扇状角度
    public int Angle_Sprite = 5;        // 弾の本数
    public Transform player;            // プレイヤーのTransform（Inspectorで指定）
    public float fireCooldown = 1f;     // クールタイム（秒）

    private float fireTimer = 0f;

    void Update()
    {
        // 自機を左右に動かすテスト用
        Vector2 pos = transform.position;
        pos.x += 0.1f * Input.GetAxisRaw("Horizontal");
        transform.position = pos;

        // クールタイム更新
        fireTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && fireTimer <= 0f)
        {
            FireNWays();
            fireTimer = fireCooldown; // クールタイムリセット
        }
    }

    void FireNWays()
    {
        // プレイヤーの方向ベクトルを取得
        Vector2 direction = (player.position - transform.position).normalized;

        // 角度に変換
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 左端の弾の発射角を計算
        float startAngle = baseAngle - Degree / 2f;

        for (int i = 0; i < Angle_Sprite; i++)
        {
            float angle = startAngle + Degree * i / (Angle_Sprite - 1);
            float rad = angle * Mathf.Deg2Rad;

            GameObject bulletObj = Instantiate(Bullet, transform.position, Quaternion.identity);
            BulletCs bulletCs = bulletObj.GetComponent<BulletCs>();
            bulletCs.theta = rad;
            bulletCs.Velocity_0 = _Velocity_0;

            Destroy(bulletObj, 5f);
        }
    }
}
