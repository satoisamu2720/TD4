using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public static ShootEnemy Instance { get; private set; }

    public float speed = 2f;
    public int maxHP = 3;
    public int currentHP;
    public GameObject bulletPrefab;
    public float shootInterval = 2f;
    private float shootTimer;

    public GameObject itemPrefab;
    private Transform player;

    [SerializeField]
    private float invincibilityDuration = 2f;
    private bool isInvincible = false;
    private float invincibilityTimer = 0f;

    private SpriteRenderer spriteRenderer;
    private Color originColor;
    private Animator animator;

    public float minDistanceFromPlayer = 3f; // 👈 プレイヤーからの最小距離

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        currentHP = StetusScript.Instance.TutorialBossEnemyHp;
        shootTimer = shootInterval;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManagement.Instance != null && !GameManagement.Instance.isPause)
        {
            if (player != null)
            {
                shootTimer -= Time.deltaTime;
                if (shootTimer <= 0f)
                {
                    Shoot();
                    shootTimer = shootInterval;
                }

                MoveAwayFromPlayer();  
                UpdateAnimation();     
            }

            Invincible();
        }
    }

    [System.Obsolete]
    void Shoot()
    {
        if (bulletPrefab != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 direction = (player.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 12f;
            Destroy(bullet, 5f);
        }
    }

    void MoveAwayFromPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < minDistanceFromPlayer)
        {
            // プレイヤーと逆方向に逃げる
            Vector2 direction = (transform.position - player.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    void UpdateAnimation()
    {
        if (animator == null || player == null) return;

        float x = player.position.x - transform.position.x;

        if (x > 0.1f)
        {
            animator.Play("enemy_Right");
        }
        else if (x < -0.1f)
        {
            animator.Play("enemy_Left");
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartInvincibility();
        }
        currentHP -= damage;
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

            float alpha = Mathf.PingPong(Time.time * 10f, 1f);
            spriteRenderer.color = new Color(1f, 0f, 0f, alpha); // 赤点滅

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                spriteRenderer.color = originColor;
            }
        }
    }

    public void Die()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
