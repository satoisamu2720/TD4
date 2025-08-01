using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nWayBullet : MonoBehaviour
{
    public static nWayBullet Instance { get; private set; }

    public GameObject Bullet;
    public float _Velocity_0 = 5f;
    public float Degree = 60f;
    public int Angle_Sprite = 5;
    public string playerTag = "Player";
    private Transform player;
    public float fireCooldown = 1f;
    public float reloadTime = 3f;
    public int maxShotsBeforeReload = 3;
    public float followDistance = 5f;
    public float moveSpeed = 2f;
    public GameObject itemPrefab;
    public float delayBeforeFire = 1f; // 追加：スポーンしてから撃ち始めるまでの待機時間

    public int currentHP;
    private float fireTimer = 0f;
    private int shotCount = 0;
    private bool isReloading = false;
    private float reloadTimer = 0f;
    private bool canFire = false; // 弾を撃ち始めてよいかどうか
    
    // 無敵時間の長さ
    [SerializeField]
    private float invincibilityDuration = 2f;
    private bool isInvincible = false;
    // 無敵時間の残り時間
    private float invincibilityTimer = 0f;

    private SpriteRenderer spriteRenderer;

    //　元のカラー
    private Color originColor;

    void Start()
    {
        currentHP = StetusScript.Instance.TutorialBossEnemyHp;
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
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
        if (GameManagement.Instance != null && GameManagement.Instance.isPause == false)
        {
            Invincible();
            if (canFire && player != null)
            {

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
        }

    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartInvincibility();

        }
           currentHP -= damage;
        if (currentHP < 0)
        {
            Die();
        }


    }

    public void Die()
    {      

        GameManagement.Instance.OnEnemyKilled(this); // ← この敵が倒れたことを通知
        //GameManagement.Instance.stage1Boss = true;
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

           // Destroy(bulletObj, 5f);
        }
    }
    



    private void StartInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;

    }
   
    private void Invincible()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            //Debug.Log("敵：点滅中 " + invincibilityTimer);

            float alpha = Mathf.PingPong(Time.time * 10f, 1f);
            spriteRenderer.color = new Color(1f, 0f, 0f, alpha); // 赤点滅

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                spriteRenderer.color = originColor;
            }
        }
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
