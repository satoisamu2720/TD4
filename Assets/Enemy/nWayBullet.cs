using UnityEngine;

public class nWayBullet : MonoBehaviour
{
    public GameObject Bullet;
    public float _Velocity_0 = 5f;
    public float Degree = 60f;
    public int Angle_Sprite = 5;
    public Transform player;
    public float fireCooldown = 1f;
    public float reloadTime = 3f;
    public int maxShotsBeforeReload = 3;
    public float followDistance = 5f;
    public float moveSpeed = 2f;
    public int maxHP = 2;
    public GameObject itemPrefab;
    public float delayBeforeFire = 1f; // 追加：スポーンしてから撃ち始めるまでの待機時間

    private int currentHP;
    private float fireTimer = 0f;
    private int shotCount = 0;
    private bool isReloading = false;
    private float reloadTimer = 0f;
    private bool canFire = false; // 弾を撃ち始めてよいかどうか

    void Start()
    {
        currentHP = maxHP;
    }

    void OnEnable()
    {
        // 撃ち始めを遅らせる
        fireTimer = fireCooldown;
        canFire = false;
        Invoke(nameof(EnableFire), delayBeforeFire);
    }

    void EnableFire()
    {
        canFire = true;
    }

    void Update()
    {
        if (!canFire || player == null) return;

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
            return;
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

    void TakeDamage(int damage)
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
