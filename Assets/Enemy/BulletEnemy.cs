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
        player = GameObject.FindWithTag("Player")?.transform;
        currentHP = StetusScript.Instance.TutorialBossEnemyHp;
        shootTimer = shootInterval;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originColor = spriteRenderer.color;
    }

    void Update()
    {
        if (TextBoxController.IsTalking) return; // 会話中は入力無効
        if (Player.IsNotMove) return; // 会話中は入力無効
        if (player != null)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootInterval; 
            }
        }
        Invincible();
    }

    void Shoot()
    {
        if (bulletPrefab != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 direction = (player.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 12f;

            Destroy(bullet, 5f);
        }
    }


   

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            StartInvincibility();

            currentHP -= damage;
        }
        currentHP -= damage;
    }

    public void Die()
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
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
