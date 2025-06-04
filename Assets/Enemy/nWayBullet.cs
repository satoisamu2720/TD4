using UnityEngine;

public class nWayBullet : MonoBehaviour
{
    public GameObject Bullet;
    public float _Velocity_0 = 5f;        // 弾の速度
    public float Degree = 60f;            // 扇状角度
    public int Angle_Sprite = 5;          // 弾の本数
    public Transform player;              // プレイヤーのTransform
    public float fireCooldown = 1f;       // 発射間隔
    public float reloadTime = 3f;         // リロード時間
    public int maxShotsBeforeReload = 3;  // 何回撃ったらリロードするか
    public float followDistance = 5f;     // プレイヤーとの距離を保つ
    public float moveSpeed = 2f;          // 敵の移動速度
    public int maxHP = 2;
    public GameObject itemPrefab;


    private int currentHP;
    private float fireTimer = 0f;
    private int shotCount = 0;
    private bool isReloading = false;
    private float reloadTimer = 0f;
     
    void Update()
    {
        if (player == null) return;

        // 距離を保ちながらプレイヤーに近づく・離れる
        Vector2 directionToPlayer = player.position - transform.position;
        float distance = directionToPlayer.magnitude;

        if (Mathf.Abs(distance - followDistance) > 0.1f)
        {
            Vector2 moveDir = directionToPlayer.normalized;
            float moveStep = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position - (Vector3)(moveDir * followDistance), moveStep);
        }

        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0f)
            {
                isReloading = false;
                shotCount = 0;
            }
            return; // リロード中は発射できない
        }

        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            FireNWays();
            fireTimer = fireCooldown;
            shotCount++;

            if (shotCount >= maxShotsBeforeReload)
            {
                isReloading = true;
                reloadTimer = reloadTime;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void FireNWays()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
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
